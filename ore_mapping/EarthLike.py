# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

def centeroidnp(arr):
    length = arr.shape[0]
    sum_x = np.sum(arr[:, 0])
    sum_y = np.sum(arr[:, 1])
    return sum_x/length, sum_y/length


iLackSurface = 0

def convertArraryToGPSString(arrayOfThree):
    global iLackSurface
    iLackSurface += 1
    # GPS: eaDesert: 58189.34:-7111: -24526.78:  # FF75C9F1:
    tmpGpsString = "GPS: LackN" + str(iLackSurface) + ":" + str(arrayOfThree[0]) + ":" + \
                   str(arrayOfThree[1]) + ":" + str(arrayOfThree[2]) + ":#F175DC:"
    return tmpGpsString

import array as arr
planet_radius = 62000 #in meters
# planet_radius = 60895 #in meters
center_of_planet = arr.array('d', [0, 0, 0])

from skimage import measure
for i in range(6):
    # print(i)
    folder_planetsfiles = 'planets_files/EarthLike/'
    if(i==0):
        # continue
        filename = os.path.join(folder_planetsfiles,'back_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [0, 0, planet_radius])
        vector3DNormalToFaceScanned = np.subtract(center_of_planet, centerFacePosition)
    if(i==1):
        # continue
        filename = os.path.join(folder_planetsfiles,'down_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [0, -planet_radius, 0])
    if(i==2):
        # continue
        filename = os.path.join(folder_planetsfiles,'front_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [0, 0, -planet_radius])
        vector3DNormalToFaceScanned = np.subtract(center_of_planet, centerFacePosition)
    if(i==3):
        # continue
        filename = os.path.join(folder_planetsfiles,'left_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [planet_radius, 0, 0])
    if(i==4):
        # continue
        filename = os.path.join(folder_planetsfiles,'right_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [-planet_radius, 0, 0])
    if(i==5):
        # continue
        filename = os.path.join(folder_planetsfiles,'up_mat.png')
        #verifie
        centerFacePosition = arr.array('d', [0, planet_radius, 0])
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
                    # print("regionSize:",regionSize)
                    tmp_region_size = regionSize

                centroid_surface_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
                centroid_surface_lack_array = arr.array('d', [centroid_surface_lack[0], centroid_surface_lack[1]])
                # print("centroid_surface_lack:",centroid_surface_lack)
                # print("centroid_surface_lack_array:",centroid_surface_lack_array)


                try:
                    pass
                    # print("vector3DNormalToFaceScanned:",vector3DNormalToFaceScanned)
                except NameError:
                    print("vector3DNormalToFaceScanned is not defined")

                try:
                    tmpPointOnTheCubeFace = arr.array('d', [0, 0, 0])

                    # we can test:
                    #case1: if unit north (0,-1,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case2: if unit ? (-1,0,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case3: if unit ? (0,0,-1).(normalToFaceCenter) = 1 or -1 or if 0 throw an error

                    # caseX got their custom formulas to generate the points

                    centroid_surface_lack_planetSized = arr.array('d', [2*planet_radius* (centroid_surface_lack_array[0]/2048),2*planet_radius* (centroid_surface_lack_array[1]/2048)])
                    # print("centroid_surface_lack_planetSized:",centroid_surface_lack_planetSized)

                    if(i==0):
                        #generate a point of a the cube that it is based on
                        #parameters are: planet_radius, image_width, centroid_surface_lack
                        intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,planet_radius])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # if(centroid_surface_lack_array[1]>1100):
                        #     if(centroid_surface_lack_array[0]>1100):
                        #         print("lower right:")

                        print(GPSString)


                    if(i==1):
                        intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [intX,-planet_radius, intZ,])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # if(centroid_surface_lack_array[1]>900):
                        #     if(centroid_surface_lack_array[0]>900):
                        #         print("lower right:")
                        # print("i:",1)
                        print(GPSString)


                    if(i==2):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,-planet_radius])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # print(GPSString)

                    if(i==3):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [planet_radius,intY, intZ,])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # if(centroid_surface_lack_array[1]<900):
                        #     if(centroid_surface_lack_array[0]<900):
                        #         print("lower right:")
                        # print("i:",3)
                        print(GPSString)

                    if(i==4):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [-planet_radius,intY, intZ,])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # if(centroid_surface_lack_array[1]>900):
                        #     if(centroid_surface_lack_array[0]>900):
                                # print("lower right:")
                        # print("i:",4)
                        print(GPSString)

                    if(i==5):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # print("intX:",intX,"|intY:",intY)
                        generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,])
                        generated_gps_point_on_planet = planet_radius * (generated_gps_point_on_cube  / np.linalg.norm(generated_gps_point_on_cube))
                        # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                        # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                        GPSString = convertArraryToGPSString(generated_gps_point_on_planet)

                        # if(centroid_surface_lack_array[1]>900):
                        #     if(centroid_surface_lack_array[0]>900):
                        #         print("lower right:")
                        # print("i:",5)
                        print(GPSString)



                except NameError:
                    print("tmpPointOnTheCubeFace computation failed")



    #find the centroid on lack spot with constant_surface_lack

    #find the centroid on lack spot with constant_surface_lack




    # # display the array of pixels as an image
    # pyplot.imshow(data)
    # pyplot.show()
