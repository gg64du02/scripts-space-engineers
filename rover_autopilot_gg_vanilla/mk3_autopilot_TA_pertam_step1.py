
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
    # print("point:"+str(point))
    # global thres_abs_sobelxy
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

def processThisPoints(points):
    iDistances = []
    iDistance = 0
    print("points:"+str(points))
    # print("str(len(points)):"+str(len(points)))
    for point in points:
        x = point[0]
        y = point[1]
        if(iDistance!=0):
            iDistance=iDistance-1
        # print("point:"+str(point))
        # global thres_abs_sobelxy
        if (thres_abs_sobelxy[x, y] == 0):
            # print(thres_abs_sobelxy[x,y])
            # for iDistance in range(0, 2048):
            for iDistance in range(iDistance, 2048):
                positiveHit = False
                testListingDiamond = generateDiamondList([x, y], iDistance)
                # testListingDiamond = generateCircleList([x, y], iDistance)
                for pTT in testListingDiamond:
                    # print(thres_abs_sobelxy[x,y])
                    if (thres_abs_sobelxy[pTT[0], pTT[1]] == 128):
                        positiveHit = True
                        # npAccumalator[x, y] = iDistance
                        iDistances.append(iDistance)
                        # print("iDistance:"+str(iDistance))
                    if (positiveHit == True):
                        break
                if (positiveHit == True):
                    break
        else:
            iDistances.append(0)

    print(points, iDistances)
    return points, iDistances

import pickle

def processThisPointsAgainstCircles(points):
    iDistances = []
    # iDistance = 0
    # print("points:"+str(points))
    # print("str(len(points)):"+str(len(points)))
    with open('arrayOfCirclesPointsList.pickle','rb') as f:
        arrayOfCirclesPointsList = pickle.load(f)
    # print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))
    for point in points:
        x = point[0]
        y = point[1]
        # if(iDistance!=0):
        #     iDistance=iDistance-1
        # print("point:"+str(point))
        # global thres_abs_sobelxy
        if (thres_abs_sobelxy[x, y] == 0):
            # print(thres_abs_sobelxy[x,y])
            # for iDistance in range(0, 2048):
            smallestDistanceSoFar = 50000
            pointsIndex = 0
            positiveHit = False
            for listPointForARadius in arrayOfCirclesPointsList:
                # print("pointsIndex:",pointsIndex)
                for pointOnARadius in listPointForARadius:
                    testingPoint = [x+pointOnARadius[0],y+pointOnARadius[1]]
                    pass
                    if(isThisInBounds(testingPoint)==True):
                        if(thres_abs_sobelxy[testingPoint[0],testingPoint[1]]!=0):
                            iDistances.append(pointsIndex)
                            positiveHit = True
                    if (positiveHit == True):
                        break
                if (positiveHit == True):
                    break
                pointsIndex = pointsIndex + 1
        else:
            iDistances.append(0)

    # print(points, iDistances)
    return points, iDistances


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

    tmpPoint = [point[0], point[1] - distance]
    if (isThisInBounds(tmpPoint) == True):
        resultList.append(tmpPoint)
        # print("tmpPoint:" + str(tmpPoint))
    tmpPoint = [point[0], point[1] + distance]
    if (isThisInBounds(tmpPoint) == True):
        resultList.append(tmpPoint)
        # print("tmpPoint:" + str(tmpPoint))

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

    ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 4  , 128,cv.THRESH_BINARY)
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 1 , 128,cv.THRESH_BINARY)
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 12 , 256,cv.THRESH_BINARY)
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 6 , 128,cv.THRESH_BINARY)

    # plt.imshow(sobelxy,cmap='gray')
    # plt.imshow(abs_sobelxy,cmap='gray')
    # plt.imshow(thres_abs_sobelxy)
    # plt.imshow(thres_abs_sobelxy,cmap='gray')

    stringTmpSplitted = file_path.split(".")[0]
    # print("stringTmpSplitted",stringTmpSplitted)


    # plt.imshow(img,cmap='gray')
    # plt.show()

    # npAccumalator
    npAccumalator = np.zeros_like(img)

    npAccumalator = npAccumalator.astype('float64')

    # # debugging
    # testListingDiamond = generateDiamondList([64,64], 3)
    # npAccumalator[64,64] = 100
    # for point in testListingDiamond:
    #     print(point[0],point[1])
    #     npAccumalator[point[0],point[1]] = 50


    # # plt.imshow(npAccumalator,cmap='gray')



    # thres_abs_sobelxy = thres_abs_sobelxy.astype('float64')
    thres_abs_sobelxy = thres_abs_sobelxy.astype('uint8')

    # image = thres_abs_sobelxy
    # # image = img
    #
    # connectivity = 8
    #
    # # output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_32S)
    # output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_8U)
    #
    # num_stats = output[0]
    # labels = output[1]
    # stats = output[2]
    #
    # new_image = image.copy()
    #
    # for label in range(num_stats):
    #     # if stats[label,cv.CC_STAT_AREA] == 1:
    #     #     new_image[labels == label] = 0
    #     # if stats[label,cv.CC_STAT_AREA] == 2:
    #     #     new_image[labels == label] = 0
    #     # if stats[label,cv.CC_STAT_AREA] == 3:
    #     #     new_image[labels == label] = 0
    #     if stats[label,cv.CC_STAT_AREA] <64:
    #         new_image[labels == label] = 0
    #     # else:
    #     #     print(stats[label,cv.CC_STAT_AREA])
    #
    # thres_abs_sobelxy = new_image

    # plt.imshow(thres_abs_sobelxy,cmap='gray')
    #
    # plt.show()
    # plt.close()
#     plt.imshow(new_image,cmap='gray')
#
#     plt.show()
#     plt.close()
# exit()
    # img.delete()

# exit()

if __name__ == '__main__':

    p = Pool(processes = 16)

    print("pooling....")
    lines =  [[[i,j] for i in range(k,k+1) for j in range(0,256)] for k in range(0,256)]

    # data = p.map(processThisPoints , lines)
    data = p.map(processThisPointsAgainstCircles , lines)

    for dataPoint in data:
        pixels = dataPoint[0]
        iDistances = dataPoint[1]
        # print("pixels"+str(pixels))
        # print("iDistances"+str(iDistances))
        for iPixels in range(0,len(pixels)):
            # print("iPixels"+str(iPixels))
            # if(iPixels==2046):
            #     pass
            # using processThisPoints
            # xData = pixels[iPixels]
            # yData = pixels[iPixels]
            # using processThisPointsAgainstCircles
            xData = pixels[iPixels][0]
            yData = pixels[iPixels][1]
            # print("xData"+str(xData))
            # print("yData"+str(yData))
            # print("len(iDistances)"+str(len(iDistances)))
            # print("len(pixels)"+str(len(pixels)))
            # print("len(iDistances)"+str(len(iDistances)))
            iDistance = iDistances[iPixels]
            # print("iDistance"+str(iDistance))
            npAccumalator[xData,yData]=iDistance

        # iDistanceData = dataPoint[1]
        # npAccumalator[xData,yData]=iDistanceData

    # for debbuging purposes
    # for x in range(0, 2048):
    #     for y in range(0,2048):
    #         if(thres_abs_sobelxy[x,y]==128):
    #             npAccumalator[x,y] = 128

    plt.imshow(npAccumalator,cmap='gray')
    plt.show()
    # p.start()
    # p.join()
    p.close()
    # print(data)

    fileNameTarget = stringTmpSplitted + "_mk3_step1" + ".png"
    #
    # cv.imwrite(fileNameTarget,npAccumalator)
    print(fileNameTarget ,"wrote")


