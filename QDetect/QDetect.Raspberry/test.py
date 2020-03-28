from picamera import PiCamera
from time import sleep

camera = PiCamera()

camera.start_preview()

sleep(2)

for i in range(10):
    imagePath = f'./image.jpg'    
    camera.capture(imagePath)
    
    sleep(1)
    
camera.stop_preview()