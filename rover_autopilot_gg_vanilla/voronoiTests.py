import pickle


with open('arrayOfCirclesPointsList.pickle','rb') as f:
    arrayOfCirclesPointsList = pickle.load(f)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))



with open('back_mk3_step1_64_no.pickle','rb') as f:
    back_mk3_step1_64_no = pickle.load(f)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

import numpy as np

from matplotlib import pyplot as plt



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



# for line in back_mk3_step1_64_no:
#     for point in line:
#         print("point",point)

resultTmp = np.zeros_like(back_mk3_step1_64_no)

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



numberOfHitOnSinglePoint = 0


lastLabelHit = -1
for x in range(500,800):
    for y in range(500,800):
# for x in range(500,800):
#     for y in range(0,100):
# for x in range(600,700):
#     for y in range(600,700):
# for x in range(500,550):
#     for y in range(500,550):
# for x in range(400,900):
#     for y in range(400,900):
# for x in range(0,256):
#     for y in range(0,256):
# for x in range(1000,1200):
#     for y in range(1000,1200):
# for x in range(500,1000):
#     for y in range(500,1500):
# for x in range(0,2048):
#     for y in range(0,2048):
        numberOfHitOnSinglePoint = 0
        currentPointChecked = [x,y]
        # print("currentPointChecked",currentPointChecked)
        radiusToBechecked = int(back_mk3_step1_64_no[currentPointChecked[0],currentPointChecked[1]])
        # print("radiusToBechecked",radiusToBechecked)
        # if (isThisInBounds(currentPointChecked)):
        #     if (back_mk3_step1_64_no[currentPointChecked[0], currentPointChecked[1]] == 0):
        #         resultTmp[currentPointChecked[0], currentPointChecked[1]] = labels[currentPointChecked[0], currentPointChecked[1]]

        listOfClosestPoints = []
        listlendiffPoints = []
        listOfLabels = []

        for pointOnCircle in arrayOfCirclesPointsList[radiusToBechecked]:
            checkPointOnCircle = [currentPointChecked[0]+pointOnCircle[0],currentPointChecked[1]+pointOnCircle[1]]
            diffPoints = [x-checkPointOnCircle[0],y-checkPointOnCircle[1]]
            lendiffPoints = np.linalg.norm(diffPoints,ord=2)
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

        # print("listOfClosestPoints",listOfClosestPoints)
        # print("listlendiffPoints",listlendiffPoints)
        # print("listOfLabels",listOfLabels)

        minIndex = listlendiffPoints.index(min(listlendiffPoints))
        # print("minIndex",minIndex)

        # resultTmp[x,y] = listOfLabels[minIndex]
        if(thres_abs_sobelxy[x,y]!=0):
            resultTmp[x,y] = labels[x,y]

        # if(numberOfHitOnSinglePoint==1):
        #     resultTmp[checkPointOnCircle[0], checkPointOnCircle[1]] = lastLabelHit


plt.imshow(resultTmp,cmap='gray')
plt.show()
