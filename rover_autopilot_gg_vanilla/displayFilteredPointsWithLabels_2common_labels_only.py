import pickle

import numpy as np

from matplotlib import pyplot as plt

from sklearn.neighbors import KDTree


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "dVASpertamFilteredWithLabels.pickle"

with open(file_path,'rb') as f:
    dVASfiltered = pickle.load(f)


xs = []
ys = []
zs = []

mucolors = []

for key in dVASfiltered.keys():
    pass
    # print(key, '->', dVASfiltered[key])
    # print(key[0])
    # print(key[1])
    # print(key[2])
    xs.append(key[0])
    ys.append(key[1])
    zs.append(key[2])
    mucolors.append(key[2]%3000)
    # mucolors.append((key[0]+key[1]+key[2])%100)

# dist, ind = tree.query([[500,500,PR]], k=5)
# print(ind)  # indices of 5 closest neighbors
# print(dist)  # distances to 5 closest neighbors

# for t3dPoint in range(len(X)):
#     xs.append(X[t3dPoint][0])
#     ys.append(X[t3dPoint][1])
#     zs.append(X[t3dPoint][2])

import matplotlib.pyplot as plt

fig = plt.figure(figsize=(12,7))
ax = fig.add_subplot(projection='3d')
# img = ax.scatter(xs, ys, zs,s=1)
# img = ax.scatter(xs, ys, zs,s=0.2)
img = ax.scatter(xs, ys, zs, c=mucolors,s=0.2)
# img = ax.scatter(xs, ys, s=0.2)
fig.colorbar(img)

# plt.show()
pass

nodes = {}

linksArray = []

counting2 = 0
counting3 = 0
counting4 = 0
for key in dVASfiltered.keys():
    pass
    # print(key, '->', dVASfiltered[key])
    # counted = dVASfiltered[key]
    counted = len(np.unique(np.asarray(dVASfiltered[key])))
    if(counted==2):
        counting2 = counting2 + 1
        linksArray.append(key)
    if(counted==3):
        counting3 = counting3 + 1
        nodes[key] = -1
    if(counted==4):
        counting4 = counting4 + 1
        nodes[key] = -1

print("counting2",counting2)
print("counting3",counting3)
print("counting4",counting4)

print()

# dist, ind = tree.query([pointIn3D], k=1)

# points_to_process = linksArray.copy()
# add the first point of linksArray onto the heapToProcess
# take last heapToProcess element and store it checkTheLinkFromThisPoint
# while len(points_to_process) != 0 :
#     get the labels of checkTheLinkFromThisPoint
#     FROM points_to_process GET all points with 2(and 3?) labels in common


def recursiveNeighboringPoints( checkTheLinkFromThisPointPar):
    global allExistingPointsWithSameCommonLabels
    global recursiveNeighboringPointsNumberOfCalls
    global neighborsPoints
    recursiveNeighboringPointsNumberOfCalls = recursiveNeighboringPointsNumberOfCalls + 1
    tree = KDTree(allExistingPointsWithSameCommonLabels, leaf_size=2)
    print("checkTheLinkFromThisPointPar",checkTheLinkFromThisPointPar)
    print("recursiveNeighboringPointsNumberOfCalls",recursiveNeighboringPointsNumberOfCalls)
    dist, ind = tree.query([checkTheLinkFromThisPointPar], k=3)
    indexDist = 0
    for distance in dist[0]:
        print("distance",distance)
        if (distance == 0):
            pass
            neighborsPoints.append(checkTheLinkFromThisPointPar)
            allExistingPointsWithSameCommonLabels.remove(tuple(checkTheLinkFromThisPointPar))
        if (distance < 30):
            if (distance != 0):
                print("indexDist",indexDist)
                print("len(ind[0])",len(ind[0]))
                if(tuple(allExistingPointsWithSameCommonLabels[ind[0][indexDist]]) in allExistingPointsWithSameCommonLabels):
                    recursiveNeighboringPoints( allExistingPointsWithSameCommonLabels[ind[0][indexDist]])
        indexDist = indexDist + 1
    print("recursiveNeighboringPoints:end")
    # if(recursiveNeighboringPointsNumberOfCalls >len(allExistingPointsWithSameCommonLabels)):
    #     return

def addToNodes(keyToAddTo,keyToAdd):
    global nodes
    if(nodes[keyToAddTo]==-1):
        initList = []
        initList.append(keyToAdd)
        nodes[keyToAddTo] = initList
    else:
        initedList = nodes[keyToAddTo]
        initedList.append(keyToAdd)
        nodes[keyToAddTo] = initedList

# # inputs
# linksArray
# nodes
# # outputs
# dictionnaryNodes (key is position) = list of nodes(Index)

points_to_process = linksArray.copy()
print()
# add the first point of linksArray onto the heapToProcess
heapToProcess = []
heapToProcess.append(np.asarray(linksArray[0]))
# take last heapToProcess element and store it checkTheLinkFromThisPoint
checkTheLinkFromThisPoint = heapToProcess[len(heapToProcess)-1]

print("len(points_to_process)",len(points_to_process))
while len(points_to_process) != 0 :
    # pass points_to_process
    # get the labels of checkTheLinkFromThisPoint
    labelsFromTheCheckedPoint = np.unique(dVASfiltered[tuple(checkTheLinkFromThisPoint)])
    print("labelsFromTheCheckedPoint",labelsFromTheCheckedPoint)
    # FROM points_to_process GET all points with 2 labels in common
    allExistingPointsWithSameCommonLabels = []
    for pointTested in points_to_process:
        pass
        # print(np.unique(dVASfiltered[tuple(pointTested)]))
        pointTestedLabels = np.unique(dVASfiltered[tuple(pointTested)])
        if(np.array_equal(pointTestedLabels,labelsFromTheCheckedPoint)==True):
            allExistingPointsWithSameCommonLabels.append(pointTested)
    print("len(allExistingPointsWithSameCommonLabels)",len(allExistingPointsWithSameCommonLabels))
    potentialNodesToCheck = []
    for node in nodes.keys():
        pass
        # print(node, '->', nodes[node])
        countingCommon = 0
        labelsInNode = dVASfiltered[tuple(node)]
        # print("labelsInNode",labelsInNode)
        for label in labelsFromTheCheckedPoint:
            if(label in labelsInNode):
                countingCommon = countingCommon + 1
        if(countingCommon>=2):
            potentialNodesToCheck.append(node)
    print("len(potentialNodesToCheck)",len(potentialNodesToCheck))
    if(len(potentialNodesToCheck)!=2):
        #(debug) displaying the potential nodes
        for potentialNodeToCheck in potentialNodesToCheck:
            print("potentialNodeToCheck",potentialNodeToCheck)
            print(np.unique(dVASfiltered[tuple(potentialNodeToCheck)]))

        # lenSqAllExistingPointsWithSameCommonLabels = len(allExistingPointsWithSameCommonLabels)*len(allExistingPointsWithSameCommonLabels)
        lenSqAllExistingPointsWithSameCommonLabels = 4*len(allExistingPointsWithSameCommonLabels)
        print("lenSqAllExistingPointsWithSameCommonLabels",lenSqAllExistingPointsWithSameCommonLabels)

        # checking for neighboring point of the
        neighborsPoints = []
        countingTrys = 0
        if(tuple(checkTheLinkFromThisPoint) in allExistingPointsWithSameCommonLabels):
            neighborsPoints.append(checkTheLinkFromThisPoint)
            allExistingPointsWithSameCommonLabels.remove(tuple(checkTheLinkFromThisPoint))
            print("first points done")
        print("len(allExistingPointsWithSameCommonLabels)",len(allExistingPointsWithSameCommonLabels))
        if(tuple(checkTheLinkFromThisPoint) in allExistingPointsWithSameCommonLabels):
            print("error !!!")
            break
        while(lenSqAllExistingPointsWithSameCommonLabels>countingTrys):
            if(len(allExistingPointsWithSameCommonLabels)==0):
                break
            pass
            onePointAdded = False
            for knownNeighbor in neighborsPoints:
                if(len(allExistingPointsWithSameCommonLabels)==0):
                    break
                # print("len(allExistingPointsWithSameCommonLabels)",len(allExistingPointsWithSameCommonLabels))
                tree = KDTree(allExistingPointsWithSameCommonLabels, leaf_size=2)
                # print("knownNeighbors", knownNeighbor)
                numberOfNeighbors1 = min([3,len(allExistingPointsWithSameCommonLabels)])
                # print("numberOfNeighbors1",numberOfNeighbors1)
                dist, ind = tree.query([knownNeighbor], k=numberOfNeighbors1)
                # dist, ind = tree.query([knownNeighbor], k=2)
                dist = dist[0]
                ind = ind[0]
                # print(dist)
                tmpAllExistingPointsWithSameCommonLabels = allExistingPointsWithSameCommonLabels.copy()
                indexOfDist = 0
                for distance in dist:
                    countingTrys = countingTrys + 1
                    if(distance<30):
                        thePoint = allExistingPointsWithSameCommonLabels[ind[indexOfDist]]
                        neighborsPoints.append(thePoint)
                        tmpAllExistingPointsWithSameCommonLabels.remove(tuple(thePoint))
                        # print("len(neighborsPoints)",len(neighborsPoints))

                    indexOfDist = indexOfDist + 1
                allExistingPointsWithSameCommonLabels = tmpAllExistingPointsWithSameCommonLabels.copy()
            # if (len(allExistingPointsWithSameCommonLabels)==0):
            #     break


            # print("len(allExistingPointsWithSameCommonLabels)", len(allExistingPointsWithSameCommonLabels))
            countingTrys = countingTrys + 1
            #     if(tuple(checkTheLinkFromThisPoint) in allExistingPointsWithSameCommonLabels):

        print("len(neighborsPoints)",len(neighborsPoints))

        # for neighborsPoint in neighborsPoints:
        #     print("neighborsPoint",neighborsPoint)

        tree = KDTree(neighborsPoints, leaf_size=2)

        linksToBeAddedToNodes = []

        for potentialNodeToCheck in potentialNodesToCheck:
            # print("knownNeighbors", knownNeighbor)
            numberOfNeighbors2 = min([3,len(neighborsPoints)])
            dist, ind = tree.query([potentialNodeToCheck], k=numberOfNeighbors2)
            dist = dist[0]
            ind = ind[0]
            print("potentialNodeToCheck",dist,ind)
            for distance in dist:
                if(distance<30):
                    linksToBeAddedToNodes.append(potentialNodeToCheck)

        for linkToBeAddedToNodes in linksToBeAddedToNodes:
            print("linkToBeAddedToNodes",linkToBeAddedToNodes)
            if(nodes[linkToBeAddedToNodes]==-1):
                pass
                # nodes[linkToBeAddedToNodes] =
    else:
        neighborsPoints = allExistingPointsWithSameCommonLabels.copy()

        linksToBeAddedToNodes = []
        linksToBeAddedToNodes.append(potentialNodesToCheck[0])
        linksToBeAddedToNodes.append(potentialNodesToCheck[1])

    if(len(linksToBeAddedToNodes)!=2):
        print("error len(linksToBeAddedToNodes)!=2")
    if(len(linksToBeAddedToNodes)==2):
        addToNodes(linksToBeAddedToNodes[0],linksToBeAddedToNodes[1])
        addToNodes(linksToBeAddedToNodes[1],linksToBeAddedToNodes[0])
        print("nodes[linksToBeAddedToNodes[0]]",nodes[linksToBeAddedToNodes[0]])
        print("nodes[linksToBeAddedToNodes[1]]",nodes[linksToBeAddedToNodes[1]])

    print()

    # removing the points from points_to_process
    for neighborsPoint in neighborsPoints:
        points_to_process.remove(tuple(neighborsPoint))

    print("len(points_to_process)",len(points_to_process))

    if(len(points_to_process)==0):
        break

    checkTheLinkFromThisPoint = np.asarray(points_to_process[0])
    # break
    # break

    # if((len(points_to_process)//5000)%2):
    nodesStr = folderNameSource + "nodes.pickle"
    with open(nodesStr, 'wb') as f1:
        pickle.dump(nodes, f1)



nodesStr = folderNameSource + "nodes.pickle"

with open(nodesStr, 'wb') as f1:
    pickle.dump(nodes, f1)






