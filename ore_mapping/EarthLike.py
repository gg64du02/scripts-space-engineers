# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

from skimage import measure
for i in range(6):
    # print(i)
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
    print("filename:",filename)

    # load image as pixel array
    data = image.imread(filename)

    data_lack_layer = 255*data[:,:,0]


    # # summarize shape of the pixel array
    # print(data_lack_layer.dtype)
    # print(1*data[0,0,:])
    # print(255*data[0,0,:])
    # change the dtype to 'float64'
    data_lack_layer = data_lack_layer.astype('int8')
    # print(data_lack_layer.dtype)
    # print(data.dtype)
    # print(data.shape)

    # pyplot.imshow(data_lack_layer)
    # pyplot.show()

    #testing the red layer
    #52
    constant_surface_lack = 16*5+2
    #2b
    constant_hidden_lack  = 16*2+11

    planet_diameter = 62000 #in meters

    center_of_planet = [0,0,0]

    converted_to_bool_surface_array  = np.zeros_like(data_lack_layer)
    for j in range(2048):
        for k in range(2048):
            if(data_lack_layer[j,k] == (constant_surface_lack)):
                converted_to_bool_surface_array[j,k] = 1

    # print(np.any((converted_to_bool_surface_array)==1))

    labeled = measure.label(data_lack_layer, background=False, connectivity=1)
    # print("labeled.shape:",labeled.shape)
    # on the bottom line
    tmp_region_size = 0
    for j in range(2048):
        for k in range(2048):
            if(converted_to_bool_surface_array[j,k]==1):

                label = labeled[j, k]  # known pixel location
                # print("label:",label)

                rp = measure.regionprops(labeled)
                # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
                props = rp[label - 1]  # background is labeled 0, not in rp
                # props.bbox  # (min_row, min_col, max_row, max_col)
                # props.image  # array matching the bbox sub-image
                # print(len(props.coords))  # list of (row,col) pixel indices
                regionSize = len(props.coords)
                pointsOfCurrentDetectedLackArray = props.coords
                for iPoint in range(len(pointsOfCurrentDetectedLackArray)):
                    converted_to_bool_surface_array[pointsOfCurrentDetectedLackArray[iPoint,0], pointsOfCurrentDetectedLackArray[iPoint,1]] = 0
                if( tmp_region_size != regionSize):
                    print("regionSize:",regionSize)
                    tmp_region_size = regionSize

    #find the centroid on lack spot with constant_surface_lack

    #find the centroid on lack spot with constant_surface_lack




    # # display the array of pixels as an image
    # pyplot.imshow(data)
    # pyplot.show()
