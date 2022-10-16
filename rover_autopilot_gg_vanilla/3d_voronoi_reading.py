import pickle

import numpy as np

from matplotlib import pyplot as plt

from sklearn.neighbors import KDTree


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "dVASpertam.pickle"

def numberOfStrFace(fileName):
    # // 0 is back
    # // 1 is down
    #
    # // 2 is front
    # // 3 is left
    #
    # // 4 is right
    # // 5 is up
    if(fileName == "back"):
        return 0
    if(fileName == "down"):
        return 1

    if(fileName == "front"):
        return 2
    if(fileName == "left"):
        return 3

    if(fileName == "right"):
        return 4
    if(fileName == "up"):
        return 5

    return -1

def conv2dTo3D(point, faceNumber):
    generated_gps_point_on_cube = []
    point = [point[0]*PR/1024,point[1]*PR/1024]
    if(faceNumber==0):
        intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        # //intZ = PR * (centroid_surface_lack[1]-2048/2) * PR
        generated_gps_point_on_cube = [intX, intY,PR]

    if(faceNumber==1):
        intX = 1*(- PR+point[1]*1)
        # //intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[0]*1)
        generated_gps_point_on_cube = [intX,-PR, intZ]

    if(faceNumber==2):
        intX = -1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        # //intZ = PR * (centroid_surface_lack[1]-2048/2) * PR
        generated_gps_point_on_cube = [intX, intY,-PR]

    if(faceNumber==3):
        # // intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[1]*1)
        generated_gps_point_on_cube = [PR,intY, intZ]

    if(faceNumber==4):
        # //intX = 1*(- PR+point[1]*1)
        intY = -1*(- PR+point[0]*1)
        intZ = 1*(- PR+point[1]*1)
        generated_gps_point_on_cube = [-PR,intY, intZ]

    if(faceNumber==5):
        intX = -1*(- PR+point[1]*1)
        # // intY = -1*(- PR+point[0]*1)
        intZ = -1*(- PR+point[0]*1)
        # //generated_gps_point_on_cube = arr.array('d', [intX,PR, intZ,]+center_of_planet)
        generated_gps_point_on_cube = [intX,PR, intZ]

    return generated_gps_point_on_cube



planet_radius = PR = 30000

with open(file_path,'rb') as f:
    dVAS = pickle.load(f)

pass
from collections import Counter


def isThisInBounds(point):
    if(point[0] >= 2048):
        return False
    if(point[0] < 0):
        return False
    if(point[1] >= 2048):
        return False
    if(point[1] < 0):
        return False
    return True

dVASpertamFilteredStr = folderNameSource + "dVASpertamFilteredWithLabels.pickle"

dVASpertamFilteredToBePickled = {}

for faceIndex in range(6):
    print("faceIndex",faceIndex)
    for x in range(0,2048):
        if(x%256==0):
            print("x",x)
        for y in range(0,2048):
            point2Ds = [[x,y],[x+1,y],[x,y+1],[x+1,y+1]]

            pixels = []

            for point2D in point2Ds:
                if(isThisInBounds(point2D)==True):
                    pixels.append(conv2dTo3D(point2D,faceIndex))

            pixelsValue = []

            for point3D in pixels:
                l2norm = np.linalg.norm(point3D,2)
                pointIn3D = point3D / l2norm
                pixelsValue.append(dVAS[tuple(pointIn3D)])
                # print("pixelsValue",pixelsValue)
                pass

            numberOfdifferentsValue = len(Counter(pixelsValue).values())

            if (numberOfdifferentsValue == 2):
                pass
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = 2
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(Counter(pixelsValue).values())
                dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(pixelsValue)
                # print("pixelsValue",pixelsValue)
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = Counter(pixelsValue).values()
            if (numberOfdifferentsValue == 3):
                pass
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = 3
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(Counter(pixelsValue).values())
                dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(pixelsValue)
                # print("point2Ds[0]:",point2Ds[0])
                # print("pixels[0]:",pixels[0])
            if (numberOfdifferentsValue == 4):
                pass
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = 4
                # dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(Counter(pixelsValue).values())
                dVASpertamFilteredToBePickled[tuple(pixels[0])] = tuple(pixelsValue)
                # print("point2Ds[0]:",point2Ds[0])
                # print("pixels[0]:",pixels[0])
                # print("p1:",p1)
            # if (numberOfdifferentsValue == 4):



with open(dVASpertamFilteredStr, 'wb') as f1:
    pickle.dump(dVASpertamFilteredToBePickled, f1)
