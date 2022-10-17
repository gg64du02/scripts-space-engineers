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


# # appending to the planets points
# # X is going to be the planets points
# print("number of points",len(linksArray))
# tree = KDTree(linksArray, leaf_size=2)
#
# heapToProcess = []
#
# heapToProcess.append(np.asarray(linksArray[0]))
#
# print()
#
# print("len(heapToProcess)",len(heapToProcess))
#
# while(len(heapToProcess)!=0):
#     print()
#
#     explore_points_nearby = heapToProcess[len(heapToProcess)-1]
#
#     heapToProcess.remove(explore_points_nearby)
#     linksArray.remove(explore_points_nearby)
#
#     tree = KDTree(linksArray, leaf_size=2)
#
#     labels_for_this_link = dVASfiltered[tuple(explore_points_nearby)]
#
#     labels_for_this_link_unique = np.unique(labels_for_this_link)
#
#     print("labels_for_this_link_unique",labels_for_this_link_unique)
#
#     dist, ind = tree.query([explore_points_nearby], k=3)
#
#     for index in ind[0]:
#         print("linksArray[ind]",linksArray[index])
#         print("labels",dVASfiltered[tuple(linksArray[index])])
#
#     print()


print()

# dist, ind = tree.query([pointIn3D], k=1)

# points_to_process = linksArray.copy()
# add the first point of linksArray onto the heapToProcess
# take last heapToProcess element and store it checkTheLinkFromThisPoint
# while len(points_to_process) != 0 :
#     get the labels of checkTheLinkFromThisPoint
#     FROM points_to_process GET all points with 2(and 3?) labels in common


points_to_process = linksArray.copy()
print()
# add the first point of linksArray onto the heapToProcess
heapToProcess = []
heapToProcess.append(np.asarray(linksArray[0]))
# take last heapToProcess element and store it checkTheLinkFromThisPoint
checkTheLinkFromThisPoint = heapToProcess[len(heapToProcess)-1]
# while len(points_to_process) != 0 :
while len(points_to_process) != 0 :
    pass
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
    #(debug) displaying the potential nodes
    for potentialNodeToCheck in potentialNodesToCheck:
        print("potentialNodeToCheck",potentialNodeToCheck)
        print(np.unique(dVASfiltered[tuple(potentialNodeToCheck)]))
    # checking for neighboring point of the

    # tree = KDTree(allExistingPointsWithSameCommonLabels, leaf_size=2)
    # dist, ind = tree.query([checkTheLinkFromThisPoint], k=3)
    # print(dist, ind )


    break
    pass
