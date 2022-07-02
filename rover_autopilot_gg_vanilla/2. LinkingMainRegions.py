
# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

import numpy as np

import os

from skimage import measure

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

# fileNamePath = folderName + "back.png"
#
# if(Path(fileNamePath).exists()==True):
#     print(fileNamePath, "exists")
# else:
#     print(fileNamePath, "does not exist")


# files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
# files = {"back_thres_abs_sobelxy_step1.png","down_thres_abs_sobelxy_step1.png","front_thres_abs_sobelxy_step1.png","left_thres_abs_sobelxy_step1.png","right_thres_abs_sobelxy_step1.png","up_thres_abs_sobelxy_step1.png"}

files = {"back_thres_abs_sobelxy_step1.png"}

full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)
# exit()

planetRegionIndexFace = 0;

for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)

    img_inverted = np.invert(img)


    # # plt.imshow(img,cmap='gray')
    # plt.imshow(img_inverted,cmap='gray')
    # plt.show()
    # img.close()
    # exit()
    # img.delete()
    # continue

    # # top
    # for i in range(0,2048):
    #     j=0
    #     print("i,j:",i,j)

    # print(img_inverted[i][j])
    # print(img[i][j])

    labeled = measure.label(img_inverted, background=False, connectivity=1)
    # labeled = measure.label(img, background=False, connectivity=1)

    # label = labeled[i, j]
    label = labeled[0, 0]

    # y,x
    # label = labeled[1245,91]

    rp = measure.regionprops(labeled)
    # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
    props = rp[label - 1]  # background is labeled 0, not in rp
    # props.bbox  # (min_row, min_col, max_row, max_col)
    # props.image  # array matching the bbox sub-image
    # print(len(props.coords))  # list of (row,col) pixel indices
    regionSize = len(props.coords)
    print("regionSize", regionSize)

    print("props.bbox:",props.bbox)

    # relevant infos for the TA auto pilot
    print("props.centroid_local:",props.centroid_local)
    planetRegionIndexFace += 1
    print("planetRegionIndexFace:",planetRegionIndexFace)

    # props.image[:, 0] vertical
    # props.image[0, :] horizontal
    # props.image[:,2047]

    left_top = []
    left_bot = []
    right_top = []
    right_bot = []

    bot_right = []
    bot_left = []
    top_right = []
    top_left = []

    print("img[0,0]:",img[0,0])
    print("img_inverted[0,0]:",img_inverted[0,0])

    print("props.bbox:",props.bbox)
    # props.bbox: (1114, 0, 1405, 300)
    # props.bbox: (y_0, x_0, y_1, x_1)

    y_0 = props.bbox[0]
    x_0 = props.bbox[1]
    y_1 = props.bbox[2]
    x_1 = props.bbox[3]

    availiablePathRet = np.zeros((2048,2048))

    print("map:putting the coords into a map")

    tmpI = 0

    connectedCoords = props.coords
    for coord in connectedCoords:
        availiablePathRet[coord[0],coord[1]] = 1
        # tmpI=tmpI+1
        # if(tmpI%100000==0):
        #     print(tmpI," done")

    print("map:finished putting the coords into a")


    if(x_0==0):
        print("bounding box is on the left line")
        for tmpK in range(0,2048,1):
            if(availiablePathRet[tmpK,0]==1):
                if(left_top==[]):
                    left_top=[tmpK,0]
        for tmpK in range(2047,0,-1):
            if(availiablePathRet[tmpK,0]==1):
                if(left_bot==[]):
                    left_bot=[tmpK,0]

    if (x_1 == 2048):
        print("bounding box is on the right line")
        for tmpK in range(0, 2048, 1):
            if (availiablePathRet[tmpK, 2047] == 1):
                if (right_top == []):
                    right_top = [tmpK, 2047]
        for tmpK in range(2047, 0, -1):
            if (availiablePathRet[tmpK, 2047] == 1):
                if (right_bot == []):
                    right_bot = [tmpK, 2047]

    if(y_0==0):
        print("bounding box is on the top line")
        for tmpK in range(0, 2048, 1):
            if (availiablePathRet[0, tmpK] == 1):
                if (top_left == []):
                    top_left = [0, tmpK]
        for tmpK in range(2047,0,-1):
            if(availiablePathRet[0, tmpK]==1):
                if(top_right==[]):
                    top_right=[0, tmpK]

    if(y_1==2048):
        print("bounding box is on the bottom line")
        for tmpK in range(0, 2048, 1):
            if (availiablePathRet[0, tmpK] == 1):
                if (bot_left == []):
                    bot_left = [0, tmpK]
        for tmpK in range(2047,0,-1):
            if(availiablePathRet[0, tmpK]==1):
                if(bot_right==[]):
                    bot_right=[0, tmpK]


    print("left_top:",left_top)
    print("left_bot:",left_bot)
    print("right_top:",right_top)
    print("right_bot:",right_bot)

    print("bot_right:",bot_right)
    print("bot_left:",bot_left)
    print("top_right:",top_right)
    print("top_left:",top_left)

    # TODO: generate data that can link region between faces

    # # possible values are 0 128 255
    # for m in range(0,2048):
    #     if(img[m,m]==128):
    #         print("[m,m]:",[m,m])
    #         print("img[m,m]:",img[m,m])


