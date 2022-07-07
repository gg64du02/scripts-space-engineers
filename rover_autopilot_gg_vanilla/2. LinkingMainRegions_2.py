import numpy as np

def parseGPS(stringGps):
    # TODO
    # positionPlanetCenter =  np.asarray([13006.69678417,145040.712265219,-115439.960197295])
    positionPlanetCenter = np.asarray([0,0,0])
    strPlanet = ""

    # print(stringGps[0:3])
    if("GPS:" in stringGps):
        # print("if(GPS: in stringGps):")
        # print(stringGps.count(":"))
        if((stringGps.count(":") == 5 ) or (stringGps.count(":") == 6 )):
            # print("if((stringGps.count(:) == 5 ) or (stringGps.count(:) == 6 )):")

            positionPlanetCenter = np.asarray([0, 0, 0])


            substrings = stringGps.split(":")
            # print(substrings)
            # print(substrings[2])
            # print(substrings[3])
            # print(substrings[4])
            intX = float(substrings[2])
            intY = float(substrings[3])
            intZ = float(substrings[4])

            positionPlanetCenter = np.asarray([intX, intY, intZ])

            # print(positionPlanetCenter)

            strPlanet = substrings[1]


    return positionPlanetCenter,strPlanet


with open('gpss_regions_pertam.txt') as f:
    lines = f.readlines()

f.close()

count = 0
for line1 in lines:
    count += 1
    # print(f'line {count}: {line}')
    # print(parseGPS(line1))
    gps1, name1 = parseGPS(line1)
    for line2 in lines:
        gps2, name2 = parseGPS(line2)
        # print(line1,line2)
        gps_diff = gps2-gps1
        # print(gps_diff)

        magnitude = np.linalg.norm(gps_diff)
        # print("magnitude:"+str(magnitude))

        if(magnitude<50):
            if(line1!=line2):
                # print("if(magnitude<50):")
                print(line1,line2)

        pass


