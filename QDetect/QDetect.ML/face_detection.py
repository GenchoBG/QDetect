from PIL import Image
from numpy import asarray
from mtcnn.mtcnn import MTCNN

detector = MTCNN()

def open_image(filepath):
    image = Image.open(filepath)
    image = image.convert('RGB')
    pixels = asarray(image)

    return pixels

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
