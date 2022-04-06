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

# import os
#
# folderName = "E:/Github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/"
#
# if(os.path.exists(folderName)==True):
#     print(folderName, "exists")
# else:
#     print(folderName, "does not exist")
#
# fileNamePath = folderName + "back.png"
#
# from pathlib import Path
#
# if(Path(fileNamePath).exists()==True):
#     print(fileNamePath, "exists")
# else:
#     print(fileNamePath, "does not exist")

files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
for file in files:
    print("file:",file)
    img = cv.imread(file,0)
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
    plt.imshow(thres_abs_sobelxy)
    # plt.imshow(thres_abs_sobelxy,cmap='gray')

    plt.show()
    # img.close()
    exit()
    # img.delete()