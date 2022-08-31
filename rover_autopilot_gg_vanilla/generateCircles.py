

import numpy as np


def returnGenerateListOfCircles(radius_tmp):
    resultingList = []

    for xi in range(-radius_tmp,radius_tmp+1):
        # print("xi:",xi)
        for yi in range(-radius_tmp,radius_tmp+1):
            # print("yi:",yi)
            lengthFromRadius = np.linalg.norm([xi,yi],ord=2)
            # print("lengthFromRadius:",lengthFromRadius)
            if(lengthFromRadius<=radius_tmp):
                if(lengthFromRadius>radius_tmp-1):
                    resultingList.append([xi,yi])
                # pass


    print("radius_tmp:"+str(radius_tmp))
    print("len(resultingList):"+str(len(resultingList)))
    return resultingList





radius = 512

# returnGenerateListOfCircles(2)

for circlePoints in returnGenerateListOfCircles(radius):
    pass


arrayOfCirclesPointsList = []

for radiusSweeped in range(0,512):
    arrayOfCirclesPointsList.append(returnGenerateListOfCircles(radiusSweeped))
    # print("arrayOfCirclesPointsList:",arrayOfCirclesPointsList)
    pass