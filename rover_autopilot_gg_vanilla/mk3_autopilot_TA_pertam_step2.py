
# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

import os

from multiprocessing import Pool

# folderNameSource = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/EarthLike/"

folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"


# folderNameTarget = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/EarthLikeProcessed/"

if(os.path.exists(folderNameSource)==True):
    print(folderNameSource, "exists")
else:
    print(folderNameSource, "does not exist")

# if(os.path.exists(folderNameTarget)==True):
#     print(folderNameTarget, "exists")
# else:
#     print(folderNameTarget, "does not exist")

from pathlib import Path

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

files = {"back_mk3_step1.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)
# exit()

for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)
    # plt.imshow(img)
    # plt.show()
    # # exit()

    nodeNumber = 0

    max_value = np.max(img)
    print("max_value:"+str(max_value))

    for i in range(0,10):
        max_value = np.max(img)
        print("max_value:"+str(max_value))
        # Find index of maximum value from 2D numpy array
        result = np.where(img == np.amax(img))
        # print('Tuple of arrays returned : ', result)
        # print('List of coordinates of maximum value in Numpy array : ')
        # zip the 2 arrays to get the exact coordinates
        listOfCordinates = list(zip(result[0], result[1]))
        # travese over the list of cordinates
        for cord in listOfCordinates:
            # print(cord)
            if img[cord[0],cord[1]] != 0:
                nodeNumber = nodeNumber + 1
                for procX in range(cord[0]-max_value,cord[0]+max_value):
                    for procY in range(cord[1]-max_value,cord[1]+max_value):
                        # if np.linalg.norm([cord[0]-procX,cord[1]-procY])<max_value*.7:
                        if np.linalg.norm([cord[0]-procX,cord[1]-procY])<max_value*.9:
                            if isThisInBounds([procX,procY])==True :
                                img[procX,procY]=0

            img[cord[0],cord[1]]=0

    print("nodeNumber:" + str(nodeNumber))


    plt.imshow(img)
    plt.show()

