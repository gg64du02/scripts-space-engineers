import pickle


# back_mk3_step1_64_no
# with open('back_clos_dis_par_proc.pickle', 'rb') as f:
import numpy as np

# with open('back_voronoi_par_proc.pickle', 'rb') as f1:
#     back_voronoi_par_proc = pickle.load(f1)
# # print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))
#
# with open('back_clos_dis_par_proc.pickle', 'rb') as f2:
#     back_clos_dis_par_proc = pickle.load(f2)

with open('.\\game_data\\SS\\PlanetDataFiles\\Pertam\\right_voronoi_par_proc.pickle', 'rb') as f1:
# with open('front_voronoi_par_proc.pickle', 'rb') as f1:
    back_voronoi_par_proc = pickle.load(f1)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

with open('.\\game_data\\SS\\PlanetDataFiles\\Pertam\\right_clos_dis_par_proc.pickle', 'rb') as f2:
# with open('front_clos_dis_par_proc.pickle', 'rb') as f2:
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

def decodeStr__NumberMax4095(strMax4095):


    resultInt = decodeAsCharNumberMax64(strMax4095[0]) * 64 + decodeAsCharNumberMax64(strMax4095[1]) * 1

    # resultInt = 0
    return resultInt



def decodeAsCharNumberMax64(character):
    # print("number:"+str(number))

    # print("character:",character)
    numberToProcess = ord(character)
    # print("numberToProcess:",numberToProcess)

    resultNumberUnder64 = 0


    if(character=="-"):
        resultNumberUnder64 = 62
        return resultNumberUnder64
    if(character=="_"):
        resultNumberUnder64 = 63
        return resultNumberUnder64

    # "0" "9" 48 58     0 9       58= 48 +10
    # "A" "Z" 65 90     36 62     91= 65 + 26
    # "a" "z" 97 122    10 35     122

    if(numberToProcess<58):
        # 48 is "0"
        resultNumberUnder64 = numberToProcess - 48
        return resultNumberUnder64
    if(numberToProcess<(90+1)):
        # 97 is "A"
        resultNumberUnder64 = numberToProcess - (90+1) + 26 + 36
        return resultNumberUnder64
    if(numberToProcess<(122+1)):
        # 97 is "a"
        resultNumberUnder64 = numberToProcess - (122+1) + 10 + 26
        return resultNumberUnder64




    return resultNumberUnder64


def encodeAsStringNumberMax64(number):
    # print("number:"+str(number))
    resultEncodeStr = ""
    if(number<10):
        # print("if(number<10):")
        resultEncodeStr = "" + str(number)
    else:
        if(number<36):
            # print("if(number<36):")
            resultEncodeStr = "" + chr(number + 87)
        else:
            if(number<62):
                # print("if(number<62):")
                resultEncodeStr = "" + chr(number + 29)
            else:
                if(number==62):
                    # print("-")
                    resultEncodeStr = "-"
                if(number==63):
                    # print("_")
                    resultEncodeStr = "_"


    return resultEncodeStr

def encodeAsString__Range(number):

    # max range is 0-4095
    first_part = encodeAsStringNumberMax64(number // 64)
    second_part = encodeAsStringNumberMax64(number % 64)

    # print("first_part",first_part)
    # print("second_part",second_part)


    resultEncodeStr = str(first_part) + str(second_part)


    # resultEncodeStr = ""
    return resultEncodeStr

# print(""+encodeAsStringNumberMax64(0))
# print(""+encodeAsStringNumberMax64(1))
# print(""+encodeAsStringNumberMax64(9))
# print(""+encodeAsStringNumberMax64(10))
# print(""+encodeAsStringNumberMax64(11))
#
# print(""+encodeAsStringNumberMax64(35))
# print(""+encodeAsStringNumberMax64(36))
#
# print(""+encodeAsStringNumberMax64(61))
# print(""+encodeAsStringNumberMax64(62))
# print(""+encodeAsStringNumberMax64(63))
# print(""+encodeAsStringNumberMax64(64))

# print(""+str(encodeAsString__Range(654)))
# print(""+str(encodeAsString__Range(546)))
# print(""+str(encodeAsString__Range(128)))
# print(""+str(encodeAsString__Range(4095)))
#
# exit()

# print(""+str(decodeAsCharNumberMax64("0")))
# print(""+str(decodeAsCharNumberMax64("9")))
# print(""+str(decodeAsCharNumberMax64("a")))
# print(""+str(decodeAsCharNumberMax64("z")))
# print(""+str(decodeAsCharNumberMax64("A")))
# print(""+str(decodeAsCharNumberMax64("Z")))
# print(""+str(decodeAsCharNumberMax64("-")))
# print(""+str(decodeAsCharNumberMax64("_")))
#
#
# print(""+str(decodeStr__NumberMax4095("aa")))



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

nodesArrayWithLabels = []

# generating the nodes
for x in range(0,2048):
    for y in range(0,2048):
        point1 = [x,y]
        if(isThisInBounds(point1)==True):
            point2 = [x+1,y+1]
            tmpNode = []
            tmpNodeWithLabels = []
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
                    tmpNodeWithLabels = [[x,y],radiusAtThisVertex,np.unique(pixelsValue)]
                    # print("radiusAtThisVertex",radiusAtThisVertex)
                    # print("nodesArray[0]",nodesArray[0])
                if(numberOfdifferentsValue==4):
                    pass
                    # # potential node rare
                    # # print("rare node")
                    # # print("rare node:x,y",x,y)
                    # # print(np.unique(pixelsValue))
                    radiusAtThisVertex = back_clos_dis_par_proc[x,y]
                    nodeNumberIndex = nodeNumberIndex + 1
                    tmpNode = [[x,y],np.unique(pixelsValue),radiusAtThisVertex]
                    tmpNodeWithLabels = [[x,y],radiusAtThisVertex,np.unique(pixelsValue)]
        if(tmpNode!=[]):
            nodesArray.append(tmpNode)
            nodesArrayWithLabels.append(tmpNodeWithLabels)
            # print("tmpNode added")
        # if(nodeNumberIndex==10):
        #     break

print("nodeNumberIndex",nodeNumberIndex)
print("nodesArray[0]",nodesArray[0])

resultStr = []

finalIndex = 0

dictPointStrIndex = {}

for indexStr in range(len(nodesArray)):
    # tmpStr = "" + finalIndex + "," + str(nodesArray[indexStr][0])
    # tmpStr = "" + encodeAsString__Range(finalIndex) + str(encodeAsString__Range(nodesArray[indexStr][0][0])) + str(
    #     encodeAsString__Range(nodesArray[indexStr][0][1]))
    tmpStr = ""  + str(encodeAsString__Range(nodesArray[indexStr][0][0])) + str(
        encodeAsString__Range(nodesArray[indexStr][0][1]))+ encodeAsString__Range(finalIndex)

    dictPointStrIndex[tmpStr[0:4]] = encodeAsString__Range(finalIndex)

    # print("tmpStr:"+tmpStr)
    resultStr.append(tmpStr)
    finalIndex = finalIndex + 1

# print("dictPointStrIndex[tmpStr]"+dictPointStrIndex[tmpStr])
# exit()
# print(""+str(resultStr[0]))
# exit()

nodesArrayWithChilds = []

listOfIntervalsBetweenVertexes = []

dictionnaryOfNodes = {}

print("len(nodesArrayWithLabels)")
# linking the nodes with potential childs
for nodeWithLabels1 in nodesArrayWithLabels:
    if(nodeWithLabels1 !=[]):
        # print(nodeWithLabels)
        # print(np.unique(nodeWithLabels[2]))
        for nodeWithLabels2 in nodesArrayWithLabels:
            if(nodeWithLabels2 !=[]):
                if(nodeWithLabels1 != nodeWithLabels2):

                    labels1 = nodeWithLabels1[2]
                    labels2 = nodeWithLabels2[2]

                    numberOfLabels1 = len(np.unique(labels1))
                    numberOfLabels2 = len(np.unique(labels2))
                    # print("numberOfLabels1:"+str(numberOfLabels1))
                    # print("numberOfLabels2:"+str(numberOfLabels2))

                    pass

                    combinedLabels = []

                    for lbl in labels1:
                        combinedLabels.append(lbl)
                    for lbl in labels2:
                        combinedLabels.append(lbl)

                    if(numberOfLabels1 >= 3):
                        if(numberOfLabels2 >= 3):
                            combinedLabels = sorted(set(combinedLabels))
                            # print("len(combinedLabels):"+str(len(combinedLabels)))
                            numberOfuniqueLabels = len(np.unique(combinedLabels))
                            # print("numberOfuniqueLabels:" + str(numberOfuniqueLabels))

                            numberOfCommonLabels = 0
                            if(labels1.size>labels2.size):
                                for label1 in labels1:
                                    if(label1 in labels2):
                                        numberOfCommonLabels = numberOfCommonLabels + 1
                            else:
                                for label2 in labels2:
                                    if(label2 in labels1):
                                        numberOfCommonLabels = numberOfCommonLabels + 1

                            # if(numberOfCommonLabels>2):
                            #     print("numberOfCommonLabels:", numberOfCommonLabels)

                            # # two common label, and two not neighbors
                            # if(numberOfuniqueLabels==4):
                            # two common labels
                            if(numberOfCommonLabels==2):
                                # print("numberOfuniqueLabels:"+str(numberOfuniqueLabels))
                                # print("nodeWithLabels1[0]:"+str(nodeWithLabels1[0]))
                                # print("nodeWithLabels2[0]:"+str(nodeWithLabels2[0]))

                                encodedLabel1 = str(encodeAsString__Range(nodeWithLabels1[0][0])) + str(
                                    encodeAsString__Range(nodeWithLabels1[0][1]))
                                encodedLabel2 = str(encodeAsString__Range(nodeWithLabels2[0][0])) + str(
        encodeAsString__Range(nodeWithLabels2[0][1]))
                                # print("==================")
                                # print("encodedLabel1:"+str(encodedLabel1))
                                # print("encodedLabel2:"+str(encodedLabel2))

                                previousInsert1 = ""
                                previousInsert2 = ""

                                # print("dictPointStrIndex[encodedLabel1]"+dictPointStrIndex[encodedLabel1])
                                # print("dictPointStrIndex[encodedLabel2]"+dictPointStrIndex[encodedLabel2])

                                # TODO: needs decoding
                                tmpIndex1 = decodeStr__NumberMax4095(dictPointStrIndex[encodedLabel1])
                                tmpIndex2 = decodeStr__NumberMax4095(dictPointStrIndex[encodedLabel2])

                                # resultStr[tmpIndex1] = resultStr[tmpIndex1] + dictPointStrIndex[encodedLabel2]
                                # resultStr[tmpIndex2] = resultStr[tmpIndex2] + dictPointStrIndex[encodedLabel1]

                                allIndexes1 = resultStr[tmpIndex1][6:]
                                allIndexes2 = resultStr[tmpIndex2][6:]
                                # print("resultStr[tmpIndex1]",resultStr[tmpIndex1])
                                # print("allIndexes1",allIndexes1)

                                n = 2
                                listIndexes1 = [allIndexes1[i:i+n] for i in range(0, len(allIndexes1), n)]
                                listIndexes2 = [allIndexes2[i:i+n] for i in range(0, len(allIndexes2), n)]
                                pass

                                if(dictPointStrIndex[encodedLabel2] not in listIndexes1):
                                    resultStr[tmpIndex1] = resultStr[tmpIndex1] + dictPointStrIndex[encodedLabel2]
                                if(dictPointStrIndex[encodedLabel1] not in listIndexes2):
                                    resultStr[tmpIndex2] = resultStr[tmpIndex2] + dictPointStrIndex[encodedLabel1]





pass

# print("resultStr[0]:"+resultStr[0])

resultStrNoRN = ""

for strres  in resultStr:
    # print("strres:",strres)
    # print(strres)
    # print(strres[0:4]+strres[6:])

    # adding radius to allow linking between faces
    xRadius = decodeStr__NumberMax4095(strres[0:2])
    yRadius = decodeStr__NumberMax4095(strres[2:4])

    radiusAtPoint = back_clos_dis_par_proc[xRadius,yRadius]
    strRadiusToAdd  = encodeAsString__Range(int(radiusAtPoint))
    if(resultStrNoRN==""):
        # resultStrNoRN = "" + strres[0:4]+strres[6:]
        resultStrNoRN = "" + strres[0:4]+strRadiusToAdd+strres[6:]
    else:
        # resultStrNoRN = resultStrNoRN + "|" + strres[0:4]+strres[6:]
        resultStrNoRN = resultStrNoRN + "|" + strres[0:4]+strRadiusToAdd+strres[6:]

print(resultStrNoRN)
# testing
# start
# testStartPoint = [50,50]
# testStartPoint = [2021,572]

# for nodeTest in nodesArrayWithChilds:
#     diffPoint = [nodeTest[0][0]-testStartPoint[0],nodeTest[0][1]-testStartPoint[1]]
#     diff_radius = np.linalg.norm(diffPoint,ord=2)
#     # print("diff_radius",diff_radius)
#
#     if (diff_radius < nodeTest[2]):
#         print("node close start:", nodeTest)
#
# # end
# # testEndPoint = [800,220]
# testEndPoint = [2042,1665]
#
# for nodeTest in nodesArrayWithChilds:
#     diffPoint = [nodeTest[0][0]-testEndPoint[0],nodeTest[0][1]-testEndPoint[1]]
#     diff_radius = np.linalg.norm(diffPoint,ord=2)
#     # print("diff_radius",diff_radius)
#
#     if (diff_radius < nodeTest[2]):
#         print("node close end:", nodeTest)


# public Node(int index, Point position, int radius){
# this.index = index;
# this.position = position;
# this.radius = radius;
# }

def generateCSinitCode(index,position,radius):
    tmpString = ""

    tmpString += "  nodes.Add(new Node("+str(index)+",new Point("+str(position[0])+","+str(position[1])+"),"+str(int(radius))+"));"


    return tmpString

def generateStingInitCode(index,position,radius):
    tmpString = ""

    tmpString += ""+str(position[0])+","+str(position[1])+","+str(int(radius))+"";


    return tmpString

print("// ================")
print(" string nodesStringRight = @\"")
for nodeGen in nodesArray:
    # print("nodeGen",nodeGen)
    indexNode = nodesArray.index(nodeGen)
    position = nodeGen[0]
    radius = nodeGen[2]
    # print("generateCSinitCode",generateCSinitCode(indexNode,position,radius))
    # print(generateStingInitCode(indexNode,position,radius))
print("\";")