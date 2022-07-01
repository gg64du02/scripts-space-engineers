#
# import cv2 as cv
# import numpy as np
# from matplotlib import pyplot as plt
# files = {"back.png","front.png"}
# for file in files:
#     print("file:",file)
#     img = cv.imread(file,0)
#     plt.imshow(img)
#     # plt.imshow(img, cmap='gray')
#     plt.show()

# generate gradient maps to know where to avoid and build a stock terrain aware autopilot rover script
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

import os

folderNameSource = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/EarthLike/"

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
files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)
# exit()
for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)
    # plt.imshow(img)
    # if(bool(img)!=None):
    #     print("img is empty: exiting")
    #     exit()
    sobelx = cv.Sobel(img,cv.CV_64F,1,0,ksize=1)
    sobely = cv.Sobel(img,cv.CV_64F,0,1,ksize=1)
    sobelxy = np.add(sobelx , sobely)

    abs_sobelx = np.absolute(sobelx)
    abs_sobely = np.absolute(sobely)
    abs_sobelxy = np.add(abs_sobelx , abs_sobely)

    ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 4  , 128,cv.THRESH_BINARY)

    # for i in range(0,2048):
    #     for j in range(0,2048):
    #         if( i % 64==0):
    #             if( j % 64==0):
    #                 print("i",i,"j",j)
    #                 thres_abs_sobelxy[i][j] = 255
    #                 # diagonals
    #                 cv.line(thres_abs_sobelxy,(i,j),(i+64,j+64),(255,255,255))
    #                 # vertical lines
    #                 cv.line(thres_abs_sobelxy,(i,j),(i,j+64),(255,255,255))
    #                 # horizontals
    #                 cv.line(thres_abs_sobelxy,(i,j),(i+64,j),(255,255,255))

    # plt.imshow(sobelxy,cmap='gray')
    # plt.imshow(abs_sobelxy,cmap='gray')
    # plt.imshow(thres_abs_sobelxy)
    # plt.imshow(thres_abs_sobelxy,cmap='gray')

    stringTmpSplitted = file_path.split(".")[0]
    print("stringTmpSplitted",stringTmpSplitted)

    fileNameTarget = stringTmpSplitted + "_thres_abs_sobelxy_step1" + ".png"

    cv.imwrite(fileNameTarget,thres_abs_sobelxy)
    print(fileNameTarget ,"wrote")

    # plt.show()
    # img.close()
    # exit()
    # img.delete()