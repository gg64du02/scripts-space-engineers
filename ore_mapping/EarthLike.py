# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot
import os
for i in range(6):
    print(i)
    folder_planetsfiles = 'planets_files/EarthLike/'
    if(i==0):
        filename = os.path.join(folder_planetsfiles,'back_mat.png')
    if(i==1):
        filename = os.path.join(folder_planetsfiles,'down_mat.png')
    if(i==2):
        filename = os.path.join(folder_planetsfiles,'front_mat.png')
    if(i==3):
        filename = os.path.join(folder_planetsfiles,'left_mat.png')
    if(i==4):
        filename = os.path.join(folder_planetsfiles,'right_mat.png')
    if(i==5):
        filename = os.path.join(folder_planetsfiles,'up_mat.png')

    # load image as pixel array
    data = image.imread(filename)

    data_lack_layer = data[:,:,0]

    pyplot.imshow(data_lack_layer)
    pyplot.show()

    #testing the red layer
    #52
    constant_surface_lack = 16*5+2
    #2b
    constant_hidden_lack  = 16*2+11

    planet_diameter = 62000 #in meters

    center_of_planet = [0,0,0]




    # summarize shape of the pixel array
    print(data.dtype)
    print(data.shape)
    # # display the array of pixels as an image
    # pyplot.imshow(data)
    # pyplot.show()
