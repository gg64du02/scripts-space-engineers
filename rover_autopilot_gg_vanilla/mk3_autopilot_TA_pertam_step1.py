
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

# fileNamePath = folderName + "back.png"
#
# if(Path(fileNamePath).exists()==True):
#     print(fileNamePath, "exists")
# else:
#     print(fileNamePath, "does not exist")

def processThisPoint(point):
    x = point[0]
    y = point[1]
    iDistance = 0
    if (thres_abs_sobelxy[x, y] == 0):
        # print(thres_abs_sobelxy[x,y])
        for iDistance in range(0, 2048):
            positiveHit = False
            testListingDiamond = generateDiamondList([x, y], iDistance)
            for pTT in testListingDiamond:
                # print(thres_abs_sobelxy[x,y])
                if (thres_abs_sobelxy[pTT[0], pTT[1]] == 128):
                    positiveHit = True
                    npAccumalator[x, y] = iDistance
                    # print("iDistance:"+str(iDistance))
                if (positiveHit == True):
                    break
            if (positiveHit == True):
                break
    return point, iDistance


def generateDiamondList(point, distance):
    resultList =[]

    tmpPoint = []

    for i1 in range(0,distance):
        # left going up
        tmpPoint = [point[0]-(distance-i1), point[1]+i1]
        if(isThisInBounds(tmpPoint)==True):
            resultList.append(tmpPoint)
            # print("tmpPoint:"+str(tmpPoint))
        # left going down
        tmpPoint = [point[0]-(distance-i1), point[1]-i1]
        if(isThisInBounds(tmpPoint)==True):
            resultList.append(tmpPoint)
            # print("tmpPoint:"+str(tmpPoint))

    tmpPoint = [point[0], point[1] - distance]
    if (isThisInBounds(tmpPoint) == True):
        resultList.append(tmpPoint)
        # print("tmpPoint:" + str(tmpPoint))
    tmpPoint = [point[0], point[1] + distance]
    if (isThisInBounds(tmpPoint) == True):
        resultList.append(tmpPoint)
        # print("tmpPoint:" + str(tmpPoint))

    for i1 in range(0,distance):
        # right going up
        tmpPoint = [point[0]+(distance-i1), point[1]+i1]
        if(isThisInBounds(tmpPoint)==True):
            resultList.append(tmpPoint)
            # print("tmpPoint:"+str(tmpPoint))
        # right going down
        tmpPoint = [point[0]+(distance-i1), point[1]-i1]
        if(isThisInBounds(tmpPoint)==True):
            resultList.append(tmpPoint)
            # print("tmpPoint:"+str(tmpPoint))

    return resultList

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





files = {"back.png"}
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
    # if(bool(img)!=None):
    #     print("img is empty: exiting")
    #     exit()
    sobelx = cv.Sobel(img,cv.CV_64F,1,0,ksize=1)
    sobely = cv.Sobel(img,cv.CV_64F,0,1,ksize=1)
    sobelxy = np.add(sobelx , sobely)

    abs_sobelx = np.absolute(sobelx)
    abs_sobely = np.absolute(sobely)
    abs_sobelxy = np.add(abs_sobelx , abs_sobely)

    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 4  , 128,cv.THRESH_BINARY)
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 1 , 128,cv.THRESH_BINARY)
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 12 , 256,cv.THRESH_BINARY)
    ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 6 , 128,cv.THRESH_BINARY)

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
    plt.imshow(thres_abs_sobelxy,cmap='gray')

    stringTmpSplitted = file_path.split(".")[0]
    # print("stringTmpSplitted",stringTmpSplitted)

    fileNameTarget = stringTmpSplitted + "_thres_abs_sobelxy_step1" + ".png"

    # cv.imwrite(fileNameTarget,thres_abs_sobelxy)
    # print(fileNameTarget ,"wrote")

    # plt.imshow(img,cmap='gray')
    # plt.show()

    # npAccumalator
    npAccumalator = np.zeros_like(img)

    # # debugging
    # testListingDiamond = generateDiamondList([64,64], 3)
    # npAccumalator[64,64] = 100
    # for point in testListingDiamond:
    #     print(point[0],point[1])
    #     npAccumalator[point[0],point[1]] = 50




        # # TODO: do a diamond generator
        # # 128 0
        # # for x in range(500,800):
        # for x in range(0,2048):
        #     print("line x:"+str(x))
        #     # for y in range(500,800):
        #     for y in range(0,2048):
        #         # print("line y:"+str(y))
        #         if(thres_abs_sobelxy[x,y]==0):
        #             # print(thres_abs_sobelxy[x,y])
        #             for iDistance in range(0,2048):
        #                 positiveHit = False
        #                 testListingDiamond = generateDiamondList([x, y], iDistance)
        #                 for pTT in testListingDiamond:
        #                     # print(thres_abs_sobelxy[x,y])
        #                     if(thres_abs_sobelxy[pTT[0],pTT[1]]==128):
        #                         positiveHit = True
        #                         npAccumalator[x,y]=iDistance
        #                         # print("iDistance:"+str(iDistance))
        #                     if(positiveHit ==True):
        #                         break
        #                 if(positiveHit ==True):
        #                     break
        #             # print("iDistance:"+str(iDistance))
        #     # if(x%20==0):
        #     #     plt.imshow(npAccumalator, cmap='gray')
        #     #     plt.show()




        # plt.imshow(npAccumalator,cmap='gray')
        # plt.show()
        # img.close()
        # exit()
        # img.delete()

if __name__ == '__main__':


    
    p = Pool(processes = 16)
    # points =  [[i,j] for i in range(0,5) for j in range(0,4)]
    # points =  [[i,j] for i in range(500,800) for j in range(500,800)]
    points =  [[i,j] for i in range(768,1024) for j in range(768,1024)]
    # points =  [[i,j] for i in range(0,2048) for j in range(0,2048)]
    # points =  [[i,j] for i in range(0,256) for j in range(0,256)]
    data = p.map(processThisPoint , points)
    for dataPoint in data:
        print("dataPoint:"+str(dataPoint))
        xData = dataPoint[0][0]
        yData = dataPoint[0][1]
        iDistanceData = dataPoint[1]
        npAccumalator[xData,yData]=iDistanceData

    plt.imshow(npAccumalator,cmap='gray')
    plt.show()
    # p.start()
    # p.join()
    p.close()
    # print(data)
