import pickle


with open('arrayOfCirclesPointsList.pickle','rb') as f:
    arrayOfCirclesPointsList = pickle.load(f)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

import numpy as np

from matplotlib import pyplot as plt


from multiprocessing import Pool


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


def processThisPointsAgainstLabels(points):
    iDistances = []
    iClosestDistances = []
    # iDistance = 0
    # print("points:"+str(points))
    print("points[0]:"+str(points[0]))
    # print("str(len(points)):"+str(len(points)))
    with open('arrayOfCirclesPointsList.pickle','rb') as f:
        arrayOfCirclesPointsList = pickle.load(f)
    # print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))
    pointsIndex = 0
    previousPointsIndex = 0
    radiusToBechecked = 0
    radiusToRemember = 0
    for point in points:
        # print("test1")
        x = point[0]
        y = point[1]
        numberOfHitOnSinglePoint = 0
        currentPointChecked = [x,y]
        # print("currentPointChecked",currentPointChecked)
        # print("radiusToBechecked",radiusToBechecked)

        listOfClosestPoints = []
        listlendiffPoints = []
        listOfLabels = []

        # print("radiusToRemember:",radiusToRemember)
        radiusToBechecked = int(radiusToRemember) - 2
        if(radiusToBechecked <0):
            radiusToBechecked = 0
        # print("radiusToBechecked:",radiusToBechecked)
        oneHit = False
        if(thres_abs_sobelxy[x,y]!=0):
            iDistances.append(labels[x,y])
            iClosestDistances.append(0)
        else:
            # iDistances.append(0)
            # for pointOnCircle in arrayOfCirclesPointsList[radiusToBechecked:]:
            # print("radiusToBechecked", radiusToBechecked)
            # for pointOnCircleList in arrayOfCirclesPointsList:
            for pointOnCircleList in arrayOfCirclesPointsList[radiusToBechecked:]:
                # print("radiusToBechecked", radiusToBechecked)
                for pointOnCircle in pointOnCircleList:
                # for pointOnCircle in arrayOfCirclesPointsList[radiusToBechecked:radiusToBechecked+2]:
                #     checkPointOnCircle = [currentPointChecked[0]+pointOnCircle[0],currentPointChecked[1]+pointOnCircle[1]]
                    checkPointOnCircle = [currentPointChecked[0]+pointOnCircle[0],currentPointChecked[1]+pointOnCircle[1]]
                    # print("checkPointOnCircle:",checkPointOnCircle)
                    diffPoints = [x-checkPointOnCircle[0],y-checkPointOnCircle[1]]
                    if(isThisInBounds([checkPointOnCircle[0],checkPointOnCircle[1]])==True):
                        if(thres_abs_sobelxy[checkPointOnCircle[0],checkPointOnCircle[1]]!=0):
                            lendiffPoints = np.linalg.norm(diffPoints,ord=2)
                            listOfClosestPoints.append(checkPointOnCircle)
                            listlendiffPoints.append(lendiffPoints)
                            # labels ?
                            readLabel = labels[checkPointOnCircle[0],checkPointOnCircle[1]]
                            listOfLabels.append(readLabel)
                            # if(readLabel!=0):
                            #     print("lendiffPoints",lendiffPoints)
                            #     print(checkPointOnCircle, "added")
                            #     print(lendiffPoints, "added")
                            #     print(readLabel, "added")
                            #     print("readLabel",readLabel)
                            radiusToRemember = int(arrayOfCirclesPointsList.index(pointOnCircleList))
                            oneHit = True
                    if(oneHit==True):
                        break
                if(oneHit==True):
                    break
                radiusToRemember = radiusToRemember + 1
            radiusToBechecked = radiusToRemember
            # print("radiusToBechecked", radiusToBechecked)
            # print("oneHit", oneHit)
            # print("listOfClosestPoints",listOfClosestPoints)
            # print("listlendiffPoints",listlendiffPoints)
            # print("listOfLabels",listOfLabels)

            minIndex = listlendiffPoints.index(min(listlendiffPoints))
            # print("minIndex",minIndex)

            # resultTmp[x,y] = listOfLabels[minIndex]
            # if(thres_abs_sobelxy[x,y]!=0):
            #     resultTmp[x,y] = labels[x,y]

            iDistances.append(listOfLabels[minIndex])
            iClosestDistances.append(radiusToRemember)

            # if(numberOfHitOnSinglePoint==1):
            #     resultTmp[checkPointOnCircle[0], checkPointOnCircle[1]] = lastLabelHit

            del pointOnCircleList

    del arrayOfCirclesPointsList

    # print(points, iDistances)
    # return points, iDistances
    return points, iDistances,iClosestDistances


import cv2 as cv

folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/preping_omsss_patch/"

# files = {"back_mat.png"}
files = {"left_mat.png"}
# files = {"left_mat.png","back.png"}
# files = {"back_mat.png","down_mat.png","front_mat.png","left_mat.png","right_mat.png","up_mat.png"}
full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)

for file_path in full_files_path:
    print("file_path:",file_path)
    # img = cv.imread(file_path,0)
    img = cv.imread(file_path,cv.IMREAD_UNCHANGED)

    img = img[:,:,0]

    img = cv.bitwise_not(img)

    # plt.imshow(blue_channel)
    # plt.show()
    # exit()

    # if(bool(img)!=None):
    # print("img is empty: exiting")
    stringTmpSplitted = file_path.split(".")[0]
    # print("stringTmpSplitted",stringTmpSplitted)
    voronoiTargetFilePath = stringTmpSplitted + "_voronoi_par_proc.pickle"
    # print("voronoiTargetFilePath",voronoiTargetFilePath)
    closestDistancesTargetFilePath = stringTmpSplitted + "_clos_dis_par_proc.pickle"
    sobelBitmapTargetFilePath = stringTmpSplitted + "_sobelBitmap.pickle"
    print("closestDistancesTargetFilePath",closestDistancesTargetFilePath)
    fileNameTarget = stringTmpSplitted + "_voronoi_par_proc.png"
    # print("fileNameTarget",fileNameTarget)
    npAccumalator = np.zeros_like(img)
    # exit()
    npAccumalator = npAccumalator.astype('float64')


    npClosestDistance = np.zeros_like(img)

    npClosestDistance = npClosestDistance.astype('float64')

    resultTmp = np.zeros_like(img)
    # # exit()
    #
    # sobelx = cv.Sobel(img,cv.CV_64F,1,0,ksize=1)
    # sobely = cv.Sobel(img,cv.CV_64F,0,1,ksize=1)
    # sobelxy = np.add(sobelx , sobely)
    #
    # abs_sobelx = np.absolute(sobelx)
    # abs_sobely = np.absolute(sobely)
    # abs_sobelxy = np.add(abs_sobelx , abs_sobely)
    #
    # ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 4  , 128,cv.THRESH_BINARY)


    thres_abs_sobelxy = img

    thres_abs_sobelxy = thres_abs_sobelxy.astype('uint8')

    # sobelBitmap = thres_abs_sobelxy
    #
    # with open(sobelBitmapTargetFilePath, 'wb') as f2:
    #     pickle.dump(sobelBitmap, f2)
    #
    # continue

    image = thres_abs_sobelxy

    connectivity = 8

    # output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_32S)
    output = cv.connectedComponentsWithStats(image, connectivity, cv.CV_8U)

    num_stats = output[0]
    labels = output[1]
    stats = output[2]

    new_image = image.copy()

    for label in range(num_stats):
        # if stats[label,cv.CC_STAT_AREA] == 3:
        #     new_image[labels == label] = 0
        # if stats[label, cv.CC_STAT_AREA] < 64:
        if stats[label, cv.CC_STAT_AREA] < 1:
            # print("label:",label)
            new_image[labels == label] = 0
        # else:
        #     print(stats[label,cv.CC_STAT_AREA])

    thres_abs_sobelxy = new_image


    numberOfHitOnSinglePoint = 0


    lastLabelHit = -1

    print("new process")

    if __name__ == '__main__':

        p = Pool(processes=12)

        print("pooling....")
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 520)] for k in range(500, 520)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 600)] for k in range(500, 600)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 800)] for k in range(500, 550)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 800)] for k in range(500, 550)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 800)] for k in range(500, 800)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(0, 300)] for k in range(0, 300)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 800)] for k in range(500, 600)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(0, 200)] for k in range(0, 200)]
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 800)] for k in range(500, 550)]
        lines = [[[i, j] for i in range(k, k + 1) for j in range(0, 2048)] for k in range(0, 2048)]

        # data = p.map(processThisPoints , lines)
        # data = p.map(processThisPointsAgainstCircles, lines)
        data = p.map(processThisPointsAgainstLabels, lines)

        for dataPoint in data:
            pixels = dataPoint[0]
            iDistances = dataPoint[1]
            closestDistances = dataPoint[2]
            # print("pixels"+str(pixels))
            # print("iDistances"+str(iDistances))
            # print("len(pixels)"+str(len(pixels)))
            # print("len(iDistances)"+str(len(iDistances)))
            for iPixels in range(0, len(pixels)):
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
                iDistance = iDistances[iPixels]
                # print("iDistance"+str(iDistance))
                npAccumalator[xData, yData] = iDistance
                # print("len(closestDistances)"+str(len(closestDistances)))
                npClosestDistance[xData, yData] = closestDistances[iPixels]

        resultTmp =npAccumalator


        # with open(voronoiTargetFilePath, 'wb') as f1:
        #     pickle.dump(resultTmp, f1)
        #
        plt.imshow(npClosestDistance,cmap='gray')
        plt.show()
        with open(closestDistancesTargetFilePath, 'wb') as f2:
            pickle.dump(npClosestDistance, f2)
        #
        #
        # # with open(sobelBitmapTargetFilePath, 'wb') as f2:
        # #     pickle.dump(sobelBitmap, f2)
        #
        # cv.imwrite(fileNameTarget, resultTmp)
        # print(fileNameTarget, "wrote")

        # plt.imshow(resultTmp,cmap='gray')
        # plt.show()
