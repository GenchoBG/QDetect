from picamera import PiCamera
from time import sleep
import requests
import os
import cloudinary
import cloudinary.uploader
import cloudinary.api
import uuid

cloudinary.config(
  cloud_name = 'dd9pexjam',  
  api_key = '918125125257855',  
  api_secret = '76rlNYHk0okgqUp7yX7n_nbe950'  
)

camera = PiCamera()

#camera.start_preview()
sleep(2)

pythonServerUrl = "http://94.156.180.190:80/getembeddings"
aspNetServerUrl = "http://qdetect.azurewebsites.net/Report/Create"

while True:
#for i in range(5):
    imagePath = f'./image.jpg'
    
    #camera.capture(imagePath)
    
    data = {}
    files = {
        'face' : open(imagePath, 'rb')    
    }
    
    result = requests.post(pythonServerUrl, data=data, files=files)
    
    embeddings = result.json()
    
    if len(embeddings) > 0:
        
        guid = str(uuid.uuid4())
        number = cloudinary.uploader.upload(imagePath, public_id = guid, folder="ad_images")
        link = cloudinary.utils.cloudinary_url(guid+".jpg");
        
        data = {
            'embeddings' : embeddings,
            'link' : link
        }
        response = requests.post(aspNetServerUrl, data, files)
        
        print(response.text)
        break
    
    os.remove(imagePath)
    
    sleep(1)
    
#camera.stop_preview()
