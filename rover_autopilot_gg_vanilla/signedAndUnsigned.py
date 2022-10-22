
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
def twos_comp(val, bits):
    """compute the 2's complement of int value val"""
    if (val & (1 << (bits - 1))) != 0: # if sign bit is set e.g., 8bit: 128-255
        val = val - (1 << bits)        # compute negative value
    return val

def convertBackUnsigned(signedNumber):
    return

def convertToSigned4096m2048(numberPrepedToConv):
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

    print("==============================")

    resultStr = str(firstStr) + str(secondStr)

    return resultStr

convertToSigned4096m2048(0)
convertToSigned4096m2048(5)
convertToSigned4096m2048(2046)
convertToSigned4096m2048(2047)
convertToSigned4096m2048(2048)

convertToSigned4096m2048(-2048)
convertToSigned4096m2048(-2047)
convertToSigned4096m2048(-2046)
convertToSigned4096m2048(-2045)
convertToSigned4096m2048(-5)