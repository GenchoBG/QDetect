from picamera import PiCamera
from time import sleep
import requests
import os

camera = PiCamera()

#camera.start_preview()
sleep(2)

pythonServerUrl = "http://94.156.180.190:80/getembeddings"
aspNetServerUrl = "www.qdetect.azurewebsites.net/Report/Create"

while True:
#for i in range(5):
    imagePath = f'./image.jpg'
    
    camera.capture(imagePath)
    
    data = {}
    files = {
        'face' : open(imagePath, 'rb')    
    }
    
    result = requests.post(pythonServerUrl, data=data, files=files)
    
    embeddings = result.json()
    
    if len(embeddings) > 0:
        data = {
            'embeddings' : embeddings
        }
        files = {
             'image' : open(imagePath, 'rb')   
        }
        requests.post(aspNetServerUrl, data, files)
    
    os.remove(imagePath)
    
    sleep(1)
    
#camera.stop_preview()
