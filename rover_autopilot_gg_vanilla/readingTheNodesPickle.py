

import pickle


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "nodes_run1.pickle"

with open(file_path,'rb') as f:
    nodes = pickle.load(f)

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

def encodeAsString___Range(number):

    print("number",number)

    # max range is 0-262 144
    first_int = number // 4096
    second_int = (number-(first_int*4096)) // 64
    third_part = (number-(first_int*4096+second_int*64)) % 64

    # print("first_int",first_int)
    # print("second_int",second_int)
    # print("third_part",third_part)

    first_part = encodeAsStringNumberMax64(first_int)
    second_part = encodeAsStringNumberMax64(second_int)
    third_part = encodeAsStringNumberMax64(third_part)

    # print("first_part",first_part)
    # print("second_part",second_part)
    # print("third_part",third_part)

    resultEncodeStr = str(first_part) + str(second_part) + str(third_part)

    print("resultEncodeStr",resultEncodeStr)

    # resultEncodeStr = ""
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

def encodeToSigned4096m2048(numberPrepedToConv):
    # ?? is -2048 | 100000 000000
    # 00 is 0     | 000000 000000

    print("numberPrepedToConv",numberPrepedToConv)

    # print(bin(numberPrepedToConv))
    bin2048 = bin(2048)

    if(numberPrepedToConv>=0):
        firstStr = encodeAsStringNumberMax64(numberPrepedToConv // 64)
        secondStr = encodeAsStringNumberMax64(numberPrepedToConv % 64)
    else:
        firstStr = ""
        secondStr = ""
        absValue = abs(numberPrepedToConv)
        print("absValue",absValue)

        # secondCmplt =

        binary_number = int("{0:08b}".format(absValue))

        flipped_binary_number = ~ binary_number
        flipped_binary_number = flipped_binary_number + 1
        str_twos_complement = str(flipped_binary_number)
        twos_complement = int(str_twos_complement, 2)
        print("twos_complement",twos_complement)
        print("bin(twos_complement)",bin(twos_complement))
        formattingToConv = int("{0:08b}".format(absValue))
        print("formattingToConv",formattingToConv)

        numberOfZeroTobeAdded = 12 - (len(str(formattingToConv)) + 1)

        zeros = ''
        for x in range (numberOfZeroTobeAdded):
            zeros = zeros + '0'

        print("zeros",zeros)

        output_enc_str = '1' + zeros + str(formattingToConv)

        print("output_enc_str",output_enc_str)

        output_enc_int = int(output_enc_str,2)

        print("output_enc_int",output_enc_int)

        firstStr = encodeAsStringNumberMax64(output_enc_int // 64)
        secondStr = encodeAsStringNumberMax64(output_enc_int % 64)

    print("firstStr",firstStr)
    print("secondStr",secondStr)

    # print("==============================")

    resultStr = str(firstStr) + str(secondStr)

    return resultStr

def decodeSignedStr(EncodedStr):
    if(len(EncodedStr)!=2):
        print("if(len(EncodedStr)!=2):")
        print("ERROR")
        exit()
    firstChar = EncodedStr[0]
    secondChar = EncodedStr[1]
    intFirstDecoded = decodeAsCharNumberMax64(firstChar)
    intSecondDecoded = decodeAsCharNumberMax64(secondChar)

    print("intFirstDecoded",intFirstDecoded)
    print("intSecondDecoded",intSecondDecoded)

    signNumber = intFirstDecoded * 64 + intSecondDecoded

    print("signNumber",signNumber)

    minus2048 = signNumber // 2048

    print("minus2048",minus2048)

    if(minus2048 == 1):
        pass
        resultInt = - (signNumber - 2048)
    else:
        resultInt = signNumber

    print("resultInt",resultInt)
    print("=============================")

    resultInt = int(resultInt)

    return resultInt

xs = []
ys = []
zs = []

# mucolors = []

# for key in nodes.keys():
#     pass
#     print(key, '->', nodes[key])
#     print(key, '->', nodes[key])
#     # print(key[0])
#     # print(key[1])
#     # print(key[2])
#     xs.append(key[0])
#     ys.append(key[1])
#     zs.append(key[2])
#     # mucolors.append(key[2]%3000)
#     # mucolors.append((key[0]+key[1]+key[2])%100)

import numpy as np

import matplotlib.pyplot as plt

fig = plt.figure(figsize=(12,7))
ax = fig.add_subplot(projection='3d')

colorCounting = 0

for node in nodes:
    colorCounting = colorCounting + 1
    pass
    key = node
    values = nodes[node]
    # print("key",key)
    # print("values",values)
    xs.append(key[0])
    ys.append(key[1])
    zs.append(key[2])
    # print("key[0]*1024/30000",key[0]*1024/30000)
    # print("key[1]*1024/30000",key[1]*1024/30000)
    # print("key[2]*1024/30000",key[2]*1024/30000)

    # # ax = plt.axes(projection='3d')

    for value in values:
        sweepValue = np.linspace(0,100,3)
        # print(len(sweepValue))

        x_ori = key[0]
        y_ori = key[1]
        z_ori = key[2]
        # print("x_ori/30000",x_ori/30000)
        # print("y_ori/30000",y_ori/30000)
        # print("z_ori/30000",z_ori/30000)

        x_fin = value[0]
        y_fin = value[1]
        z_fin = value[2]

        # print()
        # Data for a three-dimensional line
        xline = x_ori + (x_fin-x_ori)*sweepValue/100
        yline = y_ori + (y_fin-y_ori)*sweepValue/100
        zline = z_ori + (z_fin-z_ori)*sweepValue/100
        # ax.plot3D(xline, yline, zline, 'gray')
        # ax.plot3D(xline, yline, zline, c=np.random.randn(1,len(sweepValue)))
        # ax.plot3D(xline, yline, zline, c=(.1,.5,.54))
        ax.plot3D(xline, yline, zline, c=((colorCounting%3//2),(colorCounting%4//3),(colorCounting%6//5)))




# img = ax.scatter(xs, ys, zs,s=1)
# img = ax.scatter(xs, ys, zs,s=0.2)
# img = ax.scatter(xs, ys, zs, c=mucolors,s=0.2)
img = ax.scatter(xs, ys, zs, s=0.2)
# img = ax.scatter(xs, ys, s=0.2)
fig.colorbar(img)

# plt.show()

dictNodesIndex = {}
# fixing nodes ref
nodesIndexes = 0
for node in nodes:
    key = node
    values = nodes[node]
    dictNodesIndex[key] = nodesIndexes
    # print("key",key)
    # print("values",values)
    nodesIndexes = nodesIndexes + 1


dictOfRef = {}
# make reference to other nodes
for node in nodes:
    key = node
    values = nodes[node]
    # print("key", key)
    # print("values", values)
    dictOfRef[key] = []
    listValueFor_dictOfRef = []
    for value in values:
        if(value!=-1):
            # print(dictNodesIndex[value])
            # print(node, "got",dictNodesIndex[value])
            listValueFor_dictOfRef.append(dictNodesIndex[value])
        else:
            print(node,"got", value)
            listValueFor_dictOfRef.append(-1)
    dictOfRef[key] = listValueFor_dictOfRef

def convertToRangeForUnsignedConv(pointOnPlanet):
    PR = 30000
    # print(pointOnPlanet[0])
    # print(pointOnPlanet[0]*1024/PR)
    # print(round(pointOnPlanet[0]*1024/PR))
    x = round(pointOnPlanet[0]*1024/PR)
    y = round(pointOnPlanet[1]*1024/PR)
    z = round(pointOnPlanet[2]*1024/PR)
    return (x,y,z)


# convertToSigned4096m2048(-5)
# convertToSigned4096m2048(5)
# convertToSigned4096m2048(-2047)
# convertToSigned4096m2048(-2048)

resultStr = ''
dictToToBeEncoded = {}
# checking if counting ?
for node in nodes:
    key = node
    values = nodes[node]
    print("key", key)
    print("values", values)
    print("index",dictNodesIndex[key])
    print(convertToRangeForUnsignedConv(key))
    print()


    # key processing
    for keySubInt in convertToRangeForUnsignedConv(key):
        resultStr = resultStr + encodeToSigned4096m2048(keySubInt)


    for index in dictOfRef[key]:
        # if(index>4095):
        #     print(index)
        tmpCheckingFunction =  encodeAsString___Range(index)
        if(len(tmpCheckingFunction)%3!=0):
            print("if(len(tmpCheckingFunction)%3!=0):")
            print("tmpCheckingFunction",tmpCheckingFunction)
            print(index)
        resultStr = resultStr + tmpCheckingFunction

    # # values processing
    # for value in values:
    #     tmpValueConverted = convertToRangeForUnsignedConv(value)
    #     for valueSubInt in tmpValueConverted:
    #         resultStr = resultStr + encodeToSigned4096m2048(valueSubInt)

    resultStr = resultStr + '|'

    # print(resultStr)
    # exit()

print(resultStr)

#sorting the nodes for the kdtree
pass
print("sorting the nodes for the kdtree")
nodesNpList = []
for node in nodes:
    key = node
    values = nodes[node]
    print("key", key)
    print("values", values)
    print("index",dictNodesIndex[key])
    nodesNpList.append(node)

resultStr = ""
nodesNpList.sort()
pass
for sortedNode in nodesNpList:
    print("sortedNode",sortedNode)

    key = sortedNode
    values = nodes[node]
    print("key", key)
    print("values", values)
    print("index", dictNodesIndex[key])
    print(convertToRangeForUnsignedConv(key))
    print()

    # key processing
    for keySubInt in convertToRangeForUnsignedConv(key):
        resultStr = resultStr + encodeToSigned4096m2048(keySubInt)

    for index in dictOfRef[key]:
        # if(index>4095):
        #     print(index)
        tmpCheckingFunction = encodeAsString___Range(index)
        if (len(tmpCheckingFunction) % 3 != 0):
            print("if(len(tmpCheckingFunction)%3!=0):")
            print("tmpCheckingFunction", tmpCheckingFunction)
            print(index)
        resultStr = resultStr + tmpCheckingFunction

    # # values processing
    # for value in values:
    #     tmpValueConverted = convertToRangeForUnsignedConv(value)
    #     for valueSubInt in tmpValueConverted:
    #         resultStr = resultStr + encodeToSigned4096m2048(valueSubInt)

    resultStr = resultStr + '|'

    # print(resultStr)
    # exit()

print(resultStr)