# This script needs those files:
# files from C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers\Content\Data\PlanetDataFiles
# folder_planetsfiles = 'planets_files/EarthLike/'
# file from https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc
# file PlanetGeneratorDefinitions.sbc

# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

import array as arr

# ======================================
# Set the script starting here
# ======================================

# files from C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers\Content\Data\PlanetDataFiles
# folder_planetsfiles = 'planets_files/Alien/'
# folder_planetsfiles = 'planets_files/EarthLike/'
folder_planetsfiles = 'planets_files/Europa/'
# folder_planetsfiles = 'planets_files/Mars/'
# folder_planetsfiles = 'planets_files/Moon/'
# folder_planetsfiles = 'planets_files/Pertam/'
# folder_planetsfiles = 'planets_files/Triton/'
# folder_planetsfiles = 'planets_files/Gea/'

# Alien
# planet_radius = 60000 #in meters
# center_of_planet = np.asarray([131072.5,131072.5,5731072.5])
# testThisGPSnpArray = np.asarray([153575.803557999,165209.154024688,5776500.24429754])

# EarthLike
# planet_radius = 58200 #in meters
# center_of_planet = np.asarray([0, 0, 0])
# testThisGPSnpArray = np.asarray([29963,1248,-52526])

# Europa
planet_radius = 8500 #in meters
center_of_planet = np.asarray([916384.50, 16384.50, 1616384.50])
testThisGPSnpArray = np.asarray([923894,22238,1615125])

# Mars
# planet_radius = 58000 #in meters
# center_of_planet = np.asarray([1031072.5,131072.5,1631072.5])
# testThisGPSnpArray = np.asarray([1068558.11061507,161713.019643011,1669167.1047199])

# Moon
# planet_radius = 8500 #in meters
# center_of_planet = np.asarray([16384.5,136384.5,-113615.5])
# testThisGPSnpArray = np.asarray([13006.69678417,145040.712265219,-115439.960197295])

# Pertam stock ?
# planet_radius = 30000 #in meters
# center_of_planet = np.asarray([-3967231.5,-32231.5,-767231.5])
# testThisGPSnpArray = np.asarray([-3937194.48,-31541.38,-764329.95])

# Triton
# planet_radius = 38000 #in meters
# center_of_planet = np.asarray([-284463.5,-2434463.5,365536.5])
# testThisGPSnpArray = np.asarray([-283368.449761682,-2400056.0174512,348238.53512947])



# Gea
# planet_radius = 58200 #in meters
# center_of_planet = np.asarray([2489556.79290313, -937.726758119417, -4199.53763362007])

# Gea test creative
# planet_radius = 58200 #in meters
# center_of_planet = np.asarray([-1202.79290313, 54987.726758119417, -44064.53763362007])
#
# testThisGPSnpArray = np.asarray([2457313.69, 19265.36, 28541.93])

displayOnlyNearTestThisGPSnpArrayBool = False

# adjusted for EarthLike
constant_hm_lacks = 3*16+13  # 3d
constant_hm_mountains = 14*16+3  # e3
constant_hm_alt_adj = constant_hm_mountains - constant_hm_lacks
print("constant_hm_alt_adj:",constant_hm_alt_adj)

enableGPSOreAltAdj = True

# DONE: add the offset introduced by center of the planet
# TODO: re factor points/coords generation for each faces
# DONE to be checked: account for the heightmap of the planet
# DONE to be checked. check PlanetGeneratorDefinitions.sbc to guess ore spots
# TODO: find out the min and max altitude

# (EarthLike)
# lack on heigthmap : 3d3d3d 62.05km
# mountain top: e3e3e3 5.17km 65.17km? GPS: OreN1:-85.8:43921.7:-40876.4:#F175DC: [1978-1,1027-1]
# actual might be actually: 66.35km
# lack - mountains = a6a6a6 = in dec 166 : 5170 / 166
# lack - mountains = a6a6a6 = in dec 166 : 6635 / 166 between 32 - 39

display_surface_lacks_bool = False
display_ore_bool = True

# DONE: check for the distance and do a mock up run to figure out which face is closer to the tested GPS
display_face_centers_positions_only = False

# ======================================
# Set the script ending here
# ======================================

def centeroidnp(arr):
    length = arr.shape[0]
    sum_x = np.sum(arr[:, 0])
    sum_y = np.sum(arr[:, 1])
    return sum_x/length, sum_y/length

iLackSurface = 0


def convertArraryToGPSStringFace(prefix,arrayOfThree):
    global filename
    hintFaceName = filename[-13:-8]
    hintFaceName = hintFaceName.replace('/',' ')
    # print(lol)
    tmpGpsString = "GPS: "+str(prefix)+"N" +hintFaceName+":"+ str(round(arrayOfThree[0],1)) + ":" + \
                   str(round(arrayOfThree[1],1)) + ":" + str(round(arrayOfThree[2],1)) + ":#F175DC:"
    return tmpGpsString

def convertArraryToGPSString(prefix,arrayOfThree):
    global iLackSurface
    iLackSurface += 1
    # GPS: eaDesert: 58189.34:-7111: -24526.78:  # FF75C9F1:
    tmpGpsString = "GPS: "+str(prefix)+"N" + str(iLackSurface) + ":" + str(round(arrayOfThree[0],1)) + ":" + \
                   str(round(arrayOfThree[1],1)) + ":" + str(round(arrayOfThree[2],1)) + ":#F175DC:"
    return tmpGpsString

sanityCheckDistanceTestThisToPlanetCenter = np.subtract(testThisGPSnpArray,center_of_planet)
lenghsanityCheckDistanceTestThisToPlanetCenter =  np.linalg.norm(sanityCheckDistanceTestThisToPlanetCenter, ord=3)
print("lenghsanityCheckDistanceTestThisToPlanetCenter:",lenghsanityCheckDistanceTestThisToPlanetCenter)
if(displayOnlyNearTestThisGPSnpArrayBool==True):
    if(lenghsanityCheckDistanceTestThisToPlanetCenter> 70000):
        print("\n\nplease check either/both GPS:\ntestThisGPSnpArray\n or \ncenter_of_planet\n\n")
        exit()

# import module
import traceback

from skimage import measure

# =============================================
# =============================================
# =============================================

import xml.etree.ElementTree as ET

# file from https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc
mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
myroot = mytree.getroot()
# print(myroot)

i = 0
previousGPScoords = [0,0,0]

FeValuesList = []
NiValuesList = []
SiValuesList = []
CoValuesList = []
AgValuesList = []
MgValuesList = []
UrValuesList = []
AuValuesList = []

for x in myroot[0]:
    # print(x.tag, x.attrib)
    if(x.tag == "OreMappings"):
        pass
        for y in x:
            i += 1
            # print("i:",i)
            # print(y.tag, y.attrib)
            # print(y.attrib["Value"])
            oreValue = int(y.attrib["Value"])
            oreTypeStr = y.attrib["Type"]


            oreAbrStr = ""
            if("Iron" in oreTypeStr):
                oreAbrStr = "Fe"
                # print("yyy")
                if(oreValue not in FeValuesList):
                    FeValuesList.append(oreValue)
                # print("FeValuesList:",FeValuesList)
            if("Nickel" in oreTypeStr):
                oreAbrStr = "Ni"
                if(oreValue not in NiValuesList):
                    NiValuesList.append(oreValue)
            if("Silicon" in oreTypeStr):
                oreAbrStr = "Si"
                if(oreValue not in SiValuesList):
                    SiValuesList.append(oreValue)
            if("Cobalt" in oreTypeStr):
                oreAbrStr = "Co"
                if(oreValue not in CoValuesList):
                    CoValuesList.append(oreValue)
            if("Silver" in oreTypeStr):
                oreAbrStr = "Ag"
                if(oreValue not in AgValuesList):
                    AgValuesList.append(oreValue)
            if("Magnesium" in oreTypeStr):
                oreAbrStr = "Mg"
                if(oreValue not in MgValuesList):
                    MgValuesList.append(oreValue)
            if("Uraninite" in oreTypeStr):
                oreAbrStr = "Ur"
                if(oreValue not in UrValuesList):
                    UrValuesList.append(oreValue)
            if("Gold" in oreTypeStr):
                oreAbrStr = "Au"
                if(oreValue not in AuValuesList):
                    AuValuesList.append(oreValue)
            # print("oreAbrStr:",oreAbrStr)
            # else:
            #     pass
            #     # print("nnn")
        break


def whatOreThatValueIs(valueInt):
    global FeValuesList
    global NiValuesList
    global SiValuesList
    global CoValuesList
    global AgValuesList
    global MgValuesList
    global UrValuesList
    global AuValuesList
    if(valueInt in FeValuesList):
        return "Fe"
    if(valueInt in NiValuesList):
        return "Ni"
    if(valueInt in SiValuesList):
        return "Si"
    if(valueInt in CoValuesList):
        return "Co"
    if(valueInt in AgValuesList):
        return "Ag"
    if(valueInt in MgValuesList):
        return "Mg"
    if(valueInt in UrValuesList):
        return "Ur"
    if(valueInt in AuValuesList):
        return "Au"
    return "$"
print("checking xml lists")
# =============================================
# =============================================
# =============================================

for i in range(6):
    # print(i)
    print("folder_planetsfiles:",folder_planetsfiles)
    if(i==0):
        # continue
        filename = os.path.join(folder_planetsfiles,'back_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'back.png')
        #verifie
        centerFacePosition = np.asarray([0, 0, planet_radius] )
    if(i==1):
        # continue
        filename = os.path.join(folder_planetsfiles,'down_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'down.png')
        #verifie
        centerFacePosition = np.asarray([0, -planet_radius, 0] )
    if(i==2):
        # continue
        filename = os.path.join(folder_planetsfiles,'front_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'front.png')
        #verifie
        centerFacePosition = np.asarray([0, 0, -planet_radius] )
    if(i==3):
        # continue
        filename = os.path.join(folder_planetsfiles,'left_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'left.png')
        #verifie
        centerFacePosition = np.asarray([planet_radius, 0, 0] )
    if(i==4):
        # continue
        filename = os.path.join(folder_planetsfiles,'right_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'right.png')
        #verifie
        centerFacePosition = np.asarray([-planet_radius, 0, 0] )
    if(i==5):
        # continue
        filename = os.path.join(folder_planetsfiles,'up_mat.png')
        filenameHeightmap = os.path.join(folder_planetsfiles,'up.png')
        #verifie
        centerFacePosition = np.asarray([0, planet_radius, 0] )
    centerFacePosition = np.asarray(center_of_planet+centerFacePosition)
    print("centerFacePosition",centerFacePosition)
    print("filename:",filename)

    if(display_face_centers_positions_only == True):
        sanityCheckDistanceTestThisToFaceCenter = np.subtract(testThisGPSnpArray, centerFacePosition)
        lenghsanityCheckDistanceTestThisToFaceCenter = round(np.linalg.norm(sanityCheckDistanceTestThisToFaceCenter,
                                                                        ord=3),0)
        print("lenghsanityCheckDistanceTestThisToFaceCenter:", lenghsanityCheckDistanceTestThisToFaceCenter)
        print(convertArraryToGPSStringFace("Face",centerFacePosition))
        continue

    print("Trying to load:",filename)
    # load image as pixel array
    data = image.imread(filename)
    print(filename,"loaded")
    print("Trying to load:",filenameHeightmap)
    dataHM = image.imread(filenameHeightmap)
    print(filenameHeightmap,"loaded")

    data_lack_layer = 255*data[:,:,0]
    data_ore_layer = 255*data[:,:,2]
    data_HM = 255*dataHM[:,:]


    # # summarize shape of the pixel array
    # print(data_lack_layer.dtype)
    # print(1*data[0,0,:])
    # print(255*data[0,0,:])
    # change the dtype to 'float64'
    # data_lack_layer = data_lack_layer.astype('int8')
    data_lack_layer = data_lack_layer.astype('int16')
    # data_ore_layer = data_ore_layer.astype('uint8')
    # data_ore_layer = data_ore_layer.astype('int8')
    data_ore_layer = data_ore_layer.astype('int16')
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
            # if(data_ore_layer[j,k] == -1):
            if(data_ore_layer[j,k] == 255):
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
            # print("j,k",j,k)

            if((converted_to_bool_surface_array[j,k]==1) and (display_surface_lacks_bool == True)):
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
                        generated_gps_point_on_cube = np.asarray([intX, intY,planet_radius] )

                    if(i==1):
                        intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = np.asarray([intX,-planet_radius, intZ,] )

                    if(i==2):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = np.asarray([intX, intY,-planet_radius] )

                    if(i==3):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = np.asarray([planet_radius,intY, intZ,] )

                    if(i==4):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = np.asarray([-planet_radius,intY, intZ,] )

                    if(i==5):
                        intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        # generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet)
                        generated_gps_point_on_cube = np.asarray([intX,planet_radius, intZ,])

                    centroid_underground_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
                    centroid_underground_lack_array = arr.array('d', [centroid_underground_lack[0],
                                                                      centroid_underground_lack[1]])

                    # print("centroid_underground_lack_array1:", centroid_underground_lack_array)
                    rounded_centroid_underground_lack_array = [int(round(centroid_underground_lack_array[0], 0)),
                                                                   int(round(centroid_underground_lack_array[1], 0))]
                    # print("rounded_centroid_underground_lack_array1:", rounded_centroid_underground_lack_array)

                    # print(
                    #     "data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]:",
                    #     data_HM[
                    #         rounded_centroid_underground_lack_array[0], rounded_centroid_underground_lack_array[1]])
                    alt_adj = (data_HM[
                        rounded_centroid_underground_lack_array[0], rounded_centroid_underground_lack_array[
                            1]]) * 23
                    # print("alt_adj:",alt_adj)

                    computed_test_alt = planet_radius + alt_adj
                    # print("computed_test_alt:",computed_test_alt)


                    generated_gps_point_on_planet = (planet_radius+alt_adj) * (
                                generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))+center_of_planet
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

            if((converted_to_bool_underground_array[j,k]==1)and (display_ore_bool == True)):
                # continue
                gpsNameOres = ""

                # if(j<1500):
                #     continue

                label = labeledOre[j, k]  # known pixel location
                # print("label:",label)

                rp = measure.regionprops(labeledOre)
                # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
                props = rp[label - 1]  # background is labeled 0, not in rp
                # props.bbox  # (min_row, min_col, max_row, max_col)
                # props.image  # array matching the bbox sub-image
                # print(len(props.coords))  # list of (row,col) pixel indices
                regionSize = len(props.coords)

                for pt in props.coords:
                    # print("data_ore_layer[pt[0],pt[1]]:",data_ore_layer[pt[0],pt[1]]+128)
                    currentPointValue = data_ore_layer[pt[0],pt[1]]+0
                    # currentPointValue = data_ore_layer[pt[0],pt[1]]+128
                    # print("currentPointValue:",currentPointValue)
                    ptOreValueIs = whatOreThatValueIs(currentPointValue)
                    # print("ptOreValueIs:",ptOreValueIs)
                    if(ptOreValueIs!="$"):
                        # print(gpsNameOres.find(ptOreValueIs))
                        if(gpsNameOres.find(ptOreValueIs)<0):
                            gpsNameOres += whatOreThatValueIs(currentPointValue)

                # print("gpsNameOres:",gpsNameOres)

                pointsOfCurrentDetectedLackArray = props.coords
                for iPoint in range(len(pointsOfCurrentDetectedLackArray)):
                    converted_to_bool_underground_array[pointsOfCurrentDetectedLackArray[iPoint,0], pointsOfCurrentDetectedLackArray[iPoint,1]] = 0
                # print("regionSize:",regionSize)
                if( tmp_region_size != regionSize):
                    # print("regionSize:",regionSize)
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
                        generated_gps_point_on_cube = np.asarray([intX, intY,planet_radius])

                    if(i==1):
                        intX = 1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = np.asarray([intX,-planet_radius, intZ,])

                    if(i==2):
                        intX = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        # intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius
                        generated_gps_point_on_cube = np.asarray([intX, intY,-planet_radius])

                    if(i==3):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = np.asarray([planet_radius,intY, intZ,])

                    if(i==4):
                        # intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1)
                        intY = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        intZ = 1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        generated_gps_point_on_cube = np.asarray([-planet_radius,intY, intZ,])

                    if(i==5):
                        intX = -1*(- planet_radius+centroid_underground_lack_planetSized[1]*1)
                        # intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1)
                        intZ = -1*(- planet_radius+centroid_underground_lack_planetSized[0]*1)
                        generated_gps_point_on_cube = np.asarray([intX,planet_radius, intZ,])

                    # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)

                    # print("centroid_underground_lack_array:",centroid_underground_lack_array)
                    # rounded_centroid_underground_lack_array = [round(centroid_underground_lack_array[0],0),round(centroid_underground_lack_array[1],0)]
                    rounded_centroid_underground_lack_array = [int(round(centroid_underground_lack_array[0],0)),int(round(centroid_underground_lack_array[1],0))]
                    # print("rounded_centroid_underground_lack_array:",rounded_centroid_underground_lack_array)

                    # alt_adj = (data_HM[j,k] - constant_hm_lacks) * 39
                    # todo: adj constant value change ? or axis changing ? according to the one in the gps coords computing ?
                    # print("data_HM[j,k]:",data_HM[j,k])
                    # alt_adj = (data_HM[j,k]) * 19
                    # print("data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]:",
                    #       data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]])
                    alt_adj = (data_HM[rounded_centroid_underground_lack_array[0],rounded_centroid_underground_lack_array[1]]) * 20
                    # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                    # print("alt_adj:",alt_adj)
                    # print("center_of_planet:",center_of_planet)
                    # print("planet_radius:",planet_radius)

                    if(enableGPSOreAltAdj== True):
                        radiusWithOrWithoutAltAdj = planet_radius+alt_adj
                    else:
                        radiusWithOrWithoutAltAdj = planet_radius

                    generated_gps_point_on_planet = (radiusWithOrWithoutAltAdj) * (
                                generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))+center_of_planet
                    # generated_gps_point_on_planet = planet_radius * (
                    #             generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))
                    # print("generated_gps_point_on_cube:",generated_gps_point_on_cube)
                    # print("generated_gps_point_on_planet:",generated_gps_point_on_planet)

                    # testThisGPSnpArray = np.asarray([33800.69,35345.36,36823.93])
                    maximum_test_distancenpArray = np.subtract(generated_gps_point_on_planet,testThisGPSnpArray)
                    distanceToTestThis = np.linalg.norm(maximum_test_distancenpArray, ord=3)
                    # print("maximum_test_distancenpArray:",maximum_test_distancenpArray)
                    # print("j,k",j,k)
                    # print("distanceToTestThis:",distanceToTestThis)


                    # GPSString = convertArraryToGPSString("Ore", generated_gps_point_on_planet)
                    GPSString = convertArraryToGPSString(gpsNameOres, generated_gps_point_on_planet)

                    # if(centroid_surface_lack_array[1]>900):
                    #     if(centroid_surface_lack_array[0]>900):
                    #         print("lower right:")
                    # print("i:",1)
                    # if(previousGPSString != GPSString):
                    #     previousGPSString = GPSString
                    previousGPScoordsNpArray = np.asarray([previousGPScoords[0],previousGPScoords[1],previousGPScoords[2]])

                    # if(previousGPScoordsNpArray.all()==generated_gps_point_on_planet.all()):
                    # if(np.array_equal(previousGPScoordsNpArray,generated_gps_point_on_planet)==True):
                    if(np.array_equal(previousGPScoordsNpArray,generated_gps_point_on_planet)==False):
                        if(displayOnlyNearTestThisGPSnpArrayBool == True):
                            if(distanceToTestThis<10000):
                                if(distanceToTestThis>-10000):
                                    # print("j,k",j,k)
                                    # print("distanceToTestThis:",distanceToTestThis)
                                    print(GPSString)
                        else:
                            # print(j,k)
                            print(GPSString)


                    previousGPScoords = generated_gps_point_on_planet

                except NameError:
                    print("tmpPointOnTheCubeFace computation failed")
                    # printing stack trace
                    traceback.print_exc()



    #find the centroid on lack spot with constant_surface_lack

    #find the centroid on lack spot with constant_surface_lack




    # # display the array of pixels as an image
    # pyplot.imshow(data)
    # pyplot.show()
