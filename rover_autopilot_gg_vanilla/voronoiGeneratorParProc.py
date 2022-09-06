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

        # print("radiusToBechecked1:",radiusToBechecked)
        radiusToBechecked = int(radiusToBechecked) - 1
        if(radiusToBechecked <0):
            radiusToBechecked = 0
        # print("radiusToBechecked2:",radiusToBechecked)
        for pointOnCircle in arrayOfCirclesPointsList[radiusToBechecked:]:
            # checkPointOnCircle = [currentPointChecked[0]+pointOnCircle[0],currentPointChecked[1]+pointOnCircle[1]]
            checkPointOnCircle = [currentPointChecked[0]+pointOnCircle[0][0],currentPointChecked[1]+pointOnCircle[0][1]]
            diffPoints = [x-checkPointOnCircle[0],y-checkPointOnCircle[1]]
            lendiffPoints = np.linalg.norm(diffPoints,ord=2)
            if(isThisInBounds([checkPointOnCircle[0],checkPointOnCircle[1]])==True):
                if(thres_abs_sobelxy[checkPointOnCircle[0],checkPointOnCircle[1]]!=0):
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
                    radiusToBechecked = int(radiusToBechecked)
                    # print("radiusToBechecked3:",radiusToBechecked)

        # print("listOfClosestPoints",listOfClosestPoints)
        # print("listlendiffPoints",listlendiffPoints)
        # print("listOfLabels",listOfLabels)

        minIndex = listlendiffPoints.index(min(listlendiffPoints))
        # print("minIndex",minIndex)

        resultTmp[x,y] = listOfLabels[minIndex]
        if(thres_abs_sobelxy[x,y]!=0):
            resultTmp[x,y] = labels[x,y]

        iDistances.append(listOfLabels[minIndex])

        # if(numberOfHitOnSinglePoint==1):
        #     resultTmp[checkPointOnCircle[0], checkPointOnCircle[1]] = lastLabelHit



    # print(points, iDistances)
    return points, iDistances


import cv2 as cv

folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

files = {"back.png"}
# files = {"front.png","back.png"}
# files = {"back.png","down.png","front.png","left.png","right.png","up.png"}
full_files_path=[]
for file in files:
    full_files_path.append(folderNameSource+file)
print(full_files_path)

for file_path in full_files_path:
    print("file_path:",file_path)
    img = cv.imread(file_path,0)
    # plt.imshow(img)
    # plt.show()
    # if(bool(img)!=None):
    # print("img is empty: exiting")
    stringTmpSplitted = file_path.split(".")[0]
    # print("stringTmpSplitted",stringTmpSplitted)
    voronoiTargetFilePath = stringTmpSplitted + "_voronoi.pickle"
    # print("voronoiTargetFilePath",voronoiTargetFilePath)
    npAccumalator = np.zeros_like(img)

    npAccumalator = npAccumalator.astype('float64')

    resultTmp = np.zeros_like(img)
    # exit()

    sobelx = cv.Sobel(img,cv.CV_64F,1,0,ksize=1)
    sobely = cv.Sobel(img,cv.CV_64F,0,1,ksize=1)
    sobelxy = np.add(sobelx , sobely)

    abs_sobelx = np.absolute(sobelx)
    abs_sobely = np.absolute(sobely)
    abs_sobelxy = np.add(abs_sobelx , abs_sobely)

    ret,thres_abs_sobelxy = cv.threshold(abs_sobelxy, 4  , 128,cv.THRESH_BINARY)


    thres_abs_sobelxy = thres_abs_sobelxy.astype('uint8')

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
        if stats[label, cv.CC_STAT_AREA] < 64:
            # print("label:",label)
            new_image[labels == label] = 0
        # else:
        #     print(stats[label,cv.CC_STAT_AREA])

    thres_abs_sobelxy = new_image


    numberOfHitOnSinglePoint = 0


    lastLabelHit = -1

    print("new process")

    if __name__ == '__main__':

        p = Pool(processes=10)

        print("pooling....")
        # lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 520)] for k in range(500, 520)]
        lines = [[[i, j] for i in range(k, k + 1) for j in range(500, 600)] for k in range(500, 600)]

        # data = p.map(processThisPoints , lines)
        # data = p.map(processThisPointsAgainstCircles, lines)
        data = p.map(processThisPointsAgainstLabels, lines)

        for dataPoint in data:
            pixels = dataPoint[0]
            iDistances = dataPoint[1]
            # print("pixels"+str(pixels))
            # print("iDistances"+str(iDistances))
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
                print("len(pixels)"+str(len(pixels)))
                print("len(iDistances)"+str(len(iDistances)))
                iDistance = iDistances[iPixels]
                # print("iDistance"+str(iDistance))
                npAccumalator[xData, yData] = iDistance

        resultTmp =npAccumalator

        # with open(voronoiTargetFilePath, 'wb') as f:
        #     pickle.dump(resultTmp, f)

        plt.imshow(resultTmp,cmap='gray')
        plt.show()
