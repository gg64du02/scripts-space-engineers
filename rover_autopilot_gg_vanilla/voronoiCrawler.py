
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


files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)
# exit()

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

def generateFourNeighborPoints(point):
    resultTmp = []

    left = [point[0]-1,point[1]]
    right = [point[0]+1,point[1]]
    top = [point[0],point[1]+1]
    down = [point[0],point[1]-1]

    fourNextPotential = [left,right,top,down]

    for point in fourNextPotential:
        if isThisInBounds(point):
            resultTmp.append(point)

    print("resultTmp:",resultTmp)
    return resultTmp

import pickle

for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)
    # plt.imshow(img)
    # plt.show()
    # if(bool(img)!=None):
    # print("img is empty: exiting")
    # exit()

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
    # # plt.imshow(img,cmap='gray')
    # plt.show()


    # ===================================================
    thres_abs_sobelxy = thres_abs_sobelxy.astype('uint8')

    image = thres_abs_sobelxy
    # image = img

    connectivity = 8

    # output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_32S)
    output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_8U)

    num_stats = output[0]
    labels = output[1]
    stats = output[2]

    new_image = image.copy()

    print("num_stats",num_stats)

    for label in range(num_stats):
        # if stats[label,cv.CC_STAT_AREA] == 1:
        #     new_image[labels == label] = 0
        # if stats[label,cv.CC_STAT_AREA] == 2:
        #     new_image[labels == label] = 0
        # if stats[label,cv.CC_STAT_AREA] == 3:
        #     new_image[labels == label] = 0
        if stats[label,cv.CC_STAT_AREA] <64:
            # print("label:",label)
            new_image[labels == label] = 0
        # else:
        #     print(stats[label,cv.CC_STAT_AREA])

    thres_abs_sobelxy = new_image


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
    # print("num_stats",num_stats)
    # new_image = image.copy()
    #
    # for label in range(num_stats):
    #     print("label",label)
    #     # if stats[label,cv.CC_STAT_AREA] == 1:
    #     #     new_image[labels == label] = 0
    #     # if stats[label,cv.CC_STAT_AREA] == 2:
    #     #     new_image[labels == label] = 0
    #     # if stats[label,cv.CC_STAT_AREA] == 3:
    #     #     new_image[labels == label] = 0
    #     # if stats[label,cv.CC_STAT_AREA] <64:
    #     #     # print("label:",label)
    #     #     new_image[labels == label] = 0
    # ===================================================

    # labels contains the thres_abs_sobelxy with the labels on the grouped points




    stringTmpSplitted = file_path.split(".")[0]
    # print("stringTmpSplitted",stringTmpSplitted)

    with open('arrayOfCirclesPointsList.pickle','rb') as f:
        arrayOfCirclesPointsList = pickle.load(f)
    # print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

    startingPoint = [20,4]
    # startingPoint = [2046,2000]

    if(isThisInBounds(startingPoint)==False):
        print("if(isThisInBounds(startingPoint)==False):")
        exit()

    iLoop = 0

    resultImg = np.zeros_like(thres_abs_sobelxy)

    storedNodes = []

    previousGroupedPointsLabel = 0

    while iLoop<1000:

        # # debugging purpose
        # testingFunction = generateFourNeighborPoints(startingPoint)
        # for point in testingFunction:
        #     print("point:",point)

        closestDistanceToABorder = 0

        # Distance_from_a_point_to_a_line
        # horizontals
        horDist1 = abs((startingPoint[1]-2048))
        horDist2 = abs((startingPoint[1]-0))

        # verticals
        verDist1 = abs((startingPoint[0]-2048))
        verDist2 = abs((startingPoint[0]-0))

        print(horDist1,horDist2,verDist1,verDist2)

        closestDistanceToABorder =min(horDist1,horDist2,verDist1,verDist2)

        # closestDistanceToABorderOrObstacle = 0

        print("closestDistanceToABorder",closestDistanceToABorder)

        # testRadius = 0

        for pointOnCircle in arrayOfCirclesPointsList[closestDistanceToABorder]:
            currentTestedPoint = [startingPoint[0]+pointOnCircle[0],startingPoint[1]+pointOnCircle[1]]
            # print("pointOnCircle",pointOnCircle)
            pass
            if(thres_abs_sobelxy[currentTestedPoint[0],currentTestedPoint[1]]==128):
                print("touched!")
                print("iLoop",iLoop)
                print("currentTestedPoint",currentTestedPoint)
                groupedPointsLabel = labels[currentTestedPoint[0],currentTestedPoint[1]]
                print("groupedPointsLabel",groupedPointsLabel)
                storedNodes.append([startingPoint,previousGroupedPointsLabel,groupedPointsLabel])
                previousGroupedPointsLabel = groupedPointsLabel
                # resultImg[pointOnCircle[0],pointOnCircle [1]] = 128
                # plt.imshow(thres_abs_sobelxy,cmap='gray')
                # plt.show()
                # plt.imshow(resultImg,cmap='gray')
                # plt.show()
                # exit()

                break

        nextStartingPoint = [0,0]

        if(closestDistanceToABorder==horDist1):
            nextStartingPoint = [startingPoint[0],startingPoint[1]-1]
        if (closestDistanceToABorder == horDist2):
            nextStartingPoint = [startingPoint[0],startingPoint[1]+1]

        if (closestDistanceToABorder == verDist1):
            nextStartingPoint = [startingPoint[0]-1,startingPoint[1]]
        if (closestDistanceToABorder == verDist2):
            nextStartingPoint = [startingPoint[0]+1,startingPoint[1]]

        print("nextStartingPoint",nextStartingPoint)

        resultImg[startingPoint[0],startingPoint[1]] = 128

        startingPoint = nextStartingPoint



        iLoop = iLoop + 1


    print("storedNodes",storedNodes)


    # plt.imshow(thres_abs_sobelxy,cmap='gray')
    # plt.show()
    plt.imshow(resultImg,cmap='gray')
    plt.show()

