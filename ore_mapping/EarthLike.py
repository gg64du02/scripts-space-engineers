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

def convertArraryToGPSString(prefix,arrayOfThree):
    global iLackSurface
    iLackSurface += 1
    # GPS: eaDesert: 58189.34:-7111: -24526.78:  # FF75C9F1:
    tmpGpsString = "GPS: "+str(prefix)+"N" + str(iLackSurface) + ":" + str(round(arrayOfThree[0],1)) + ":" + \
                   str(round(arrayOfThree[1],1)) + ":" + str(round(arrayOfThree[2],1)) + ":#F175DC:"
    return tmpGpsString

import array as arr
# planet_radius = 62000 #in meters
# planet_radius = 60895 #in meters
# planet_radius = 61000 #in meters
planet_radius = 60000 #in meters
center_of_planet = arr.array('d', [0, 0, 0])

# TODO: add the offset introduced by center of the planet
# TODO: re factor points/coords generation for each faces
# TODO: account for the heightmap of the planet
# TODO. check PlanetGeneratorDefinitions.sbc to guess ore spots
# TODO: find out the min and max altitude
# lack on heigthmap : 3d3d3d 62.05km
# mountain top : e3e3e3 5.17km 65.17km? GPS: OreN1:-85.8:43921.7:-40876.4:#F175DC: [1978-1,1027-1]

# actual might be actually: 66.35km

# lack - mountains = a6a6a6 = in dec 166 : 5170 / 166
# lack - mountains = a6a6a6 = in dec 166 : 6635 / 166 between 32 - 39


constant_hm_lacks = 3*16+13  # 3d

constant_hm_mountains = 14*16+3  # e3

constant_hm_alt_adj = constant_hm_mountains - constant_hm_lacks
print("constant_hm_alt_adj:",constant_hm_alt_adj)

# import module
import traceback

from skimage import measure
for i in range(6):
    # print(i)
    folder_planetsfiles = 'planets_files/EarthLike/'
    if(i==0):
        continue
        filename = os.path.join(folder_planetsfiles,'back_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'back.png')
        #verifie
        centerFacePosition = arr.array('d', [0, 0, planet_radius])
    if(i==1):
        continue
        filename = os.path.join(folder_planetsfiles,'down_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'down.png')
        #verifie
        centerFacePosition = arr.array('d', [0, -planet_radius, 0])
    if(i==2):
        continue
        filename = os.path.join(folder_planetsfiles,'front_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'front.png')
        #verifie
        centerFacePosition = arr.array('d', [0, 0, -planet_radius])
    if(i==3):
        continue
        filename = os.path.join(folder_planetsfiles,'left_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'left.png')
        #verifie
        centerFacePosition = arr.array('d', [planet_radius, 0, 0])
    if(i==4):
        continue
        filename = os.path.join(folder_planetsfiles,'right_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'right.png')
        #verifie
        centerFacePosition = arr.array('d', [-planet_radius, 0, 0])
    if(i==5):
        # continue
        filename = os.path.join(folder_planetsfiles,'up_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'up.png')
        #verifie
        centerFacePosition = arr.array('d', [0, planet_radius, 0])
    print("filename:",filename)

    # load image as pixel array
    data = image.imread(filename)
    dataHM = image.imread(filenameHeightmap)

    data_lack_layer = 255*data[:,:,0]
    data_ore_layer = 255*data[:,:,2]
    data_HM = 255*dataHM[:,:]


    # # summarize shape of the pixel array
    # print(data_lack_layer.dtype)
    # print(1*data[0,0,:])
    # print(255*data[0,0,:])
    # change the dtype to 'float64'
    data_lack_layer = data_lack_layer.astype('int8')
    data_ore_layer = data_ore_layer.astype('int8')
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
    constant_no_ore = 255


    converted_to_bool_surface_array  = np.zeros_like(data_lack_layer)
    converted_to_bool_underground_array  = np.zeros_like(data_lack_layer)
    for j in range(2048):
        for k in range(2048):
            if(data_lack_layer[j,k] == (constant_surface_lack)):
                converted_to_bool_surface_array[j,k] = 1
            # print("data_ore_layer[j,k]",data_ore_layer[j,k])
            if(data_ore_layer[j,k] == -1):
                converted_to_bool_underground_array[j, k] = 0
            else:
                converted_to_bool_underground_array[j, k] = 1
            # if(data_ore_layer[j,k] != (constant_no_ore)):
            #     # print("lol1")
            #     converted_to_bool_underground_array[j,k] = 1
            # else:
            #     print("nope")
    # break
    # print(np.any((converted_to_bool_surface_array)==1))

    labeled = measure.label(data_lack_layer, background=False, connectivity=1)
    labeledOre = measure.label(converted_to_bool_underground_array, background=False, connectivity=1)
    # print("labeled.shape:",labeled.shape)
    # on the bottom line
    tmp_region_size = 0
    for j in range(2048):
        for k in range(2048):
            # print("lol2")
            if(converted_to_bool_surface_array[j,k]==1):
                # pass
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
                    tmpPointOnTheCubeFace = arr.array('d', [0, 0, 0])

                    # we can test:
                    #case1: if unit north (0,-1,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case2: if unit ? (-1,0,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case3: if unit ? (0,0,-1).(normalToFaceCenter) = 1 or -1 or if 0 throw an error

                    # caseX got their custom formulas to generate the points

                    centroid_surface_lack_planetSized = arr.array('d', [2*planet_radius* (centroid_surface_lack_array[0]/2048),2*planet_radius* (centroid_surface_lack_array[1]/2048)])
                    # print("centroid_surface_lack_planetSized:",centroid_surface_lack_planetSized)

                    if(i==0):
                        intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,planet_radius])

                    if(i==1):
                        intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = arr.array('d', [intX,-planet_radius, intZ,])

                    if(i==2):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,-planet_radius])

                    if(i==3):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = arr.array('d', [planet_radius,intY, intZ,])

                    if(i==4):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = arr.array('d', [-planet_radius,intY, intZ,])

                    if(i==5):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,])

                    centroid_underground_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
                    centroid_underground_lack_array = arr.array('d', [centroid_underground_lack[0],
                                                                      centroid_underground_lack[1]])

                    print("centroid_underground_lack_array1:", centroid_underground_lack_array)
                    rounded_centroid_underground_lack_array = [int(round(centroid_underground_lack_array[0], 0)),
                                                                   int(round(centroid_underground_lack_array[1], 0))]
                    print("rounded_centroid_underground_lack_array1:", rounded_centroid_underground_lack_array)

                    print(
                        "data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]:",
                        data_HM[
                            rounded_centroid_underground_lack_array[0], rounded_centroid_underground_lack_array[1]])
                    alt_adj = (data_HM[
                        rounded_centroid_underground_lack_array[0], rounded_centroid_underground_lack_array[
                            1]]) * 22

                    computed_test_alt = planet_radius + alt_adj
                    print("computed_test_alt:",computed_test_alt)


                    generated_gps_point_on_planet = planet_radius * (
                                generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))
                    # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                    # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                    GPSString = convertArraryToGPSString("Lack", generated_gps_point_on_planet)

                    # if(centroid_surface_lack_array[1]>900):
                    #     if(centroid_surface_lack_array[0]>900):
                    #         print("lower right:")
                    # print("i:",1)
                    print(GPSString)

                except NameError:
                    print("tmpPointOnTheCubeFace computation failed")
                    # printing stack trace
                    traceback.print_exc()

            if(converted_to_bool_underground_array[j,k]==1):
                continue

                label = labeledOre[j, k]  # known pixel location
                # print("label:",label)

                rp = measure.regionprops(labeledOre)
                # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
                props = rp[label - 1]  # background is labeled 0, not in rp
                # props.bbox  # (min_row, min_col, max_row, max_col)
                # props.image  # array matching the bbox sub-image
                # print(len(props.coords))  # list of (row,col) pixel indices
                regionSize = len(props.coords)

                pointsOfCurrentDetectedLackArray = props.coords
                # for iPoint in range(len(pointsOfCurrentDetectedLackArray)):
                #     converted_to_bool_underground_array[pointsOfCurrentDetectedLackArray[iPoint,0], pointsOfCurrentDetectedLackArray[iPoint,1]] = 0
                # print("regionSize:",regionSize)
                if( tmp_region_size != regionSize):
                    print("regionSize:",regionSize)
                    tmp_region_size = regionSize

                centroid_underground_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
                centroid_underground_lack_array = arr.array('d', [centroid_underground_lack[0], centroid_underground_lack[1]])
                # print("centroid_surface_lack:",centroid_surface_lack)
                # print("centroid_surface_lack_array:",centroid_surface_lack_array)


                try:
                    tmpPointOnTheCubeFace = arr.array('d', [0, 0, 0])

                    # we can test:
                    #case1: if unit north (0,-1,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case2: if unit ? (-1,0,0).(normalToFaceCenter) = 1 or -1 or if 0 test:
                    #case3: if unit ? (0,0,-1).(normalToFaceCenter) = 1 or -1 or if 0 throw an error

                    # caseX got their custom formulas to generate the points

                    # dev debug: just to figure out what min max altitude are
                    # centroid_underground_lack_array = [1978-1,1027-1]

                    centroid_underground_lack_planetSized = arr.array('d', [2*planet_radius* (centroid_underground_lack_array[0]/2048),2*planet_radius* (centroid_underground_lack_array[1]/2048)])
                    # print("centroid_surface_lack_planetSized:",centroid_surface_lack_planetSized)

                    if(i==0):
                        intX = 1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,planet_radius])

                    if(i==1):
                        intX = 1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = arr.array('d', [intX,-planet_radius, intZ,])

                    if(i==2):
                        intX = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = arr.array('d', [intX, intY,-planet_radius])

                    if(i==3):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = arr.array('d', [planet_radius,intY, intZ,])

                    if(i==4):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        intZ = 1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = arr.array('d', [-planet_radius,intY, intZ,])

                    if(i==5):
                        intX = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,])

                    print("centroid_underground_lack_array:",centroid_underground_lack_array)
                    # rounded_centroid_underground_lack_array = [round(centroid_underground_lack_array[0],0),round(centroid_underground_lack_array[1],0)]
                    rounded_centroid_underground_lack_array = [int(round(centroid_underground_lack_array[0],0)),int(round(centroid_underground_lack_array[1],0))]
                    print("rounded_centroid_underground_lack_array:",rounded_centroid_underground_lack_array)

                    # alt_adj = (data_HM[j,k] - constant_hm_lacks) * 39
                    # todo: adj constant value change ? or axis changing ? according to the one in the gps coords computing ?
                    # print("data_HM[j,k]:",data_HM[j,k])
                    # alt_adj = (data_HM[j,k]) * 19
                    print("data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]:",
                          data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]])
                    # alt_adj = (data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]-constant_hm_lacks) * 39
                    alt_adj = (data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]) * 22
                    # alt_adj = (data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]) * 18 + 1084
                    # alt_adj = (data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]) * 39
                    print("alt_adj:",alt_adj)

                    generated_gps_point_on_planet = (planet_radius+alt_adj) * (
                                generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))
                    # generated_gps_point_on_planet = planet_radius * (
                    #             generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))
                    # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                    # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                    GPSString = convertArraryToGPSString("Ore", generated_gps_point_on_planet)

                    # if(centroid_surface_lack_array[1]>900):
                    #     if(centroid_surface_lack_array[0]>900):
                    #         print("lower right:")
                    # print("i:",1)
                    print(GPSString)

                except NameError:
                    print("tmpPointOnTheCubeFace computation failed")



    #find the centroid on lack spot with constant_surface_lack

    #find the centroid on lack spot with constant_surface_lack




    # # display the array of pixels as an image
    # pyplot.imshow(data)
    # pyplot.show()
