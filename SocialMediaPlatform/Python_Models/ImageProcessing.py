import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
os.environ['TF_ENABLE_ONEDNN_OPTS'] = '0'

import tensorflow as tf
import logging
import sys
import pickle
import numpy as np
from PIL import Image

# Suppress TensorFlow logs
tf.get_logger().setLevel('ERROR')
logging.getLogger('tensorflow').setLevel(logging.ERROR)

model_file_path = os.path.join(os.path.dirname(__file__), 'model.pkl')

if os.path.isfile(model_file_path):
    with open(model_file_path, 'rb') as model_file:
        age_model = pickle.load(model_file)

def predict_age(image_path):
    try:
        img = Image.open(image_path).convert('L')
        img = img.resize((128, 128), Image.Resampling.LANCZOS)
        img = np.expand_dims(img, axis=0) 
        img = np.array(img)
            
        img = img.reshape(1, 128, 128, 1)
            
        img = img / 255.0
        age_prediction = age_model.predict(img,verbose=0)
        return age_prediction[1][0][0]
    except Exception as e:
        sys.stdout.buffer.write(f"Error: {e}\n".encode('utf-8'))  # Use buffer write for encoding
        return None  # Ensure it returns None on error

# Get the image path from the argument
image_path = sys.argv[1]

# Run prediction
age = predict_age(image_path)

# Ensure only the age is printed (as an integer)
if age is None:
    sys.stdout.buffer.write(b"not found\n")  # Use buffer write for encoding
else:
    sys.stdout.buffer.write(f"{int(round(age))}\n".encode('utf-8')) 