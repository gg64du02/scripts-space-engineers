import pickle


# back_mk3_step1_64_no
# with open('back_clos_dis_par_proc.pickle', 'rb') as f:
import numpy as np

with open('back_voronoi_par_proc.pickle', 'rb') as f1:
    back_voronoi_par_proc = pickle.load(f1)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

with open('back_clos_dis_par_proc.pickle', 'rb') as f2:
    back_clos_dis_par_proc = pickle.load(f2)

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




# plt.imshow(back_voronoi_par_proc)
# plt.show()

listOfDiffLabels = []

# for x in range(0,64):
#     for y in range(0,64):
# for x in range(0,264):
#     for y in range(0,264):
for x in range(0,2048):
    for y in range(0,2048):
        point = [x,y]
        if(isThisInBounds(point)==True):
            if(back_voronoi_par_proc[x,y] not in listOfDiffLabels):
                listOfDiffLabels.append(back_voronoi_par_proc[x,y])

print(len(listOfDiffLabels))

print(listOfDiffLabels)

from collections import Counter

tmpDiffListOfLabels = []

nodeNumberIndex = 0

nodesArray = []


# generating the nodes
for x in range(0,2048):
    for y in range(0,2048):
        point1 = [x,y]
        if(isThisInBounds(point1)==True):
            point2 = [x+1,y+1]
            tmpNode = []
            if(isThisInBounds(point2)==True):
                p1 = [x,y]
                p2 = [x+1,y]
                p3 = [x,y+1]
                p4 = [x+1,y+1]
                pixel1 = back_voronoi_par_proc[p1[0],p1[1]]
                pixel2 = back_voronoi_par_proc[p2[0],p2[1]]
                pixel3 = back_voronoi_par_proc[p3[0],p3[1]]
                pixel4 = back_voronoi_par_proc[p4[0],p4[1]]

                pixels = [p1,p2,p3,p4]
                pixelsValue = [pixel1,pixel2,pixel3,pixel4]

                # print(Counter(pixelsValue).values())
                # print(len(Counter(pixelsValue).values()))
                # print(np.unique(pixelsValue))

                numberOfdifferentsValue = len(Counter(pixelsValue).values())

                if(numberOfdifferentsValue==1):
                    pass
                  # do nothing
                if(numberOfdifferentsValue==2):
                    pass
                    # potential path
                    # print("path:x,y",x,y)
                if(numberOfdifferentsValue==3):
                    pass
                    # potential node
                    # print("node:x,y",x,y)
                    # print(np.unique(pixelsValue))
                    radiusAtThisVertex = back_clos_dis_par_proc[x,y]
                    nodeNumberIndex = nodeNumberIndex + 1
                    tmpNode = [[x,y],np.unique(pixelsValue),radiusAtThisVertex]
                    # print("radiusAtThisVertex",radiusAtThisVertex)
                    # print("nodesArray[0]",nodesArray[0])
                if(numberOfdifferentsValue==4):
                    pass
                    # potential node rare
                    # print("rare node")
                    # print("rare node:x,y",x,y)
                    # print(np.unique(pixelsValue))
                    radiusAtThisVertex = back_clos_dis_par_proc[x,y]
                    nodeNumberIndex = nodeNumberIndex + 1
                    tmpNode = [[x,y],np.unique(pixelsValue),radiusAtThisVertex]
        if(tmpNode!=[]):
            nodesArray.append(tmpNode)
            # print("tmpNode added")
        # if(nodeNumberIndex==10):
        #     break

print("nodeNumberIndex",nodeNumberIndex)
print("nodesArray[0]",nodesArray[0])

nodesArrayWithChilds = []

# linking the nodes with potential childs
for node1 in nodesArray:
    pass
    nodesIndexToPutinChildren = []
    for node2 in nodesArray:
        if(node1!=node2):
            pass
            # print("nodes", node1,node2)
            diff_point = [node1[0][0]-node2[0][0],node1[0][1]-node2[0][1]]
            diff_radius = np.linalg.norm(diff_point,ord=2)
            # print("diff_radius",diff_radius)

            diff_radius1 = node1[2]
            diff_radius2 = node2[2]

            # print("diff_radius1",diff_radius1)
            # print("diff_radius2",diff_radius2)

            if(diff_radius1>diff_radius):
                # print("if(diff_radius1>diff_radius):")
                nodesIndexToPutinChildren.append(nodesArray.index(node2))
            if(diff_radius2>diff_radius):
                # print("if(diff_radius2>diff_radius):")
                nodesIndexToPutinChildren.append(nodesArray.index(node2))
    # print("nodesIndexToPutinChildren",nodesIndexToPutinChildren)
    nodesArrayWithChilds.append([node1[0],node1[1],node1[2],nodesIndexToPutinChildren])


# testing
# start
testStartPoint = [50,50]

for nodeTest in nodesArrayWithChilds:
    diffPoint = [nodeTest[0][0]-testStartPoint[0],nodeTest[0][1]-testStartPoint[1]]
    diff_radius = np.linalg.norm(diffPoint,ord=2)
    # print("diff_radius",diff_radius)

    if (diff_radius < nodeTest[2]):
        print("node close:", nodeTest)

# end
testEndPoint = [800,220]

for nodeTest in nodesArrayWithChilds:
    diffPoint = [nodeTest[0][0]-testEndPoint[0],nodeTest[0][1]-testEndPoint[1]]
    diff_radius = np.linalg.norm(diffPoint,ord=2)
    # print("diff_radius",diff_radius)

    if (diff_radius < nodeTest[2]):
        print("node close:", nodeTest)
