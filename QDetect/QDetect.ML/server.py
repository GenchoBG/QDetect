from flask import Flask, request, Response
from flask_cors import CORS, cross_origin
from PIL import Image
import numpy as np
from numpy import asarray
from mtcnn.mtcnn import MTCNN
from tensorflow.keras.models import load_model
from tensorflow.keras.backend import set_session
import tensorflow as tf
import json

app = Flask(__name__)
CORS(app)
app.config['CORS_HEADERS'] = '*'

graph = tf.get_default_graph()
sess = tf.Session()

set_session(sess)

detector = MTCNN()
model = load_model(r'./models/facenet_keras.h5')

def extract_faces(pixels, required_size=(160, 160)):

    results = detector.detect_faces(pixels)
    faces = []

    for result in results:
        x1, y1, width, height = result['box']

        x1, y1 = abs(x1), abs(y1)
        x2, y2 = x1 + width, y1 + height

        face = pixels[y1:y2, x1:x2]

        image = Image.fromarray(face)
        if required_size:
            image = image.resize(required_size)
        face_array = asarray(image)
        faces.append(face_array)

    return faces

def get_embedding(face_pixels):
    face_pixels = face_pixels.astype('float32')

    mean, std = face_pixels.mean(), face_pixels.std()
    face_pixels = (face_pixels - mean) / std

    samples = np.expand_dims(face_pixels, axis=0)
    yhat = model.predict(samples)

    return yhat[0]

@app.route('/getembeddings', methods = ['POST'])
@cross_origin()
def faces_embeddings():
    global graph
    global sess
    with graph.as_default():
        set_session(sess)
        uploaded_image = request.files['face']
        uploaded_image = Image.open(uploaded_image).convert('RGB')
        detected_faces = extract_faces(asarray(uploaded_image))

        embeddings = [get_embedding(detected_face).tolist() for detected_face in detected_faces]

        return Response(json.dumps(embeddings), mimetype="application/json")

@app.after_request
def add_headers(response):
    response.headers.add('Access-Control-Allow-Origin', '*')
    response.headers.add('Access-Control-Allow-Headers', '*')
    return response

if __name__ == '__main__':
    app.run(host= '0.0.0.0', port=80, debug = False)
