import numpy as np
from tensorflow.keras.models import load_model
import argparse
from face_detection import open_image, extract_faces

model = load_model(r'./models/facenet_keras.h5')

def get_embedding(face_pixels):
    face_pixels = face_pixels.astype('float32')

    mean, std = face_pixels.mean(), face_pixels.std()
    face_pixels = (face_pixels - mean) / std

    samples = np.expand_dims(face_pixels, axis=0)
    yhat = model.predict(samples)

    return yhat[0]

def distance(embedding1, embedding2):
    return np.sqrt(np.sum((embedding1 - embedding2) ** 2, axis=0))
if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('--image', metavar='image', type=str, default=None)

    args = parser.parse_args()

    image = args.image

    if image == None:
        print('Specify image path')
        exit(1)

    faces = extract_faces(open_image(image))

    embeddings = [get_embedding(face) for face in faces]

    print(embeddings)
