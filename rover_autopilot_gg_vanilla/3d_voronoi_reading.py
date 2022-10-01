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

for faceIndex in range(6):
    for x in range(0,2048):
        for y in range(0,2048):
            p1 = conv2dTo3D([x,y],faceIndex)
            p2 = conv2dTo3D([x+1,y],faceIndex)
            p3 = conv2dTo3D([x,y+1],faceIndex)
            p4 = conv2dTo3D([x+1,y+1],faceIndex)


            pixels = [p1,p2,p3,p4]

            pixelsValue = []

            for point3D in pixels:
                l2norm = np.linalg.norm(point3D,2)
                pointIn3D = point3D / l2norm
                pixelsValue.append(dVAS[tuple(pointIn3D)])
                print("pixelsValue",pixelsValue)
                pass



