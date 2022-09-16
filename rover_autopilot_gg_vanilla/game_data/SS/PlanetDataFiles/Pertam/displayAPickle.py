import pickle


# back_mk3_step1_64_no
# with open('back_clos_dis_par_proc.pickle', 'rb') as f:
# with open('back_voronoi_par_proc.pickle', 'rb') as f:
with open('right_voronoi_par_proc.pickle', 'rb') as f:
# with open('right_clos_dis_par_proc.pickle', 'rb') as f:
# with open('back_clos_dis_par_proc.pickle', 'rb') as f:
    back_voronoi_par_proc = pickle.load(f)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))

def displayLarger(point):
    x = point[0]
    y = point[1]
    back_voronoi_par_proc[x:x+10,y:y+10] = 0

displayLarger([107,41])
displayLarger([278,175])
displayLarger([319,240])
displayLarger([331,238])
displayLarger([498,360])
displayLarger([553,298])
displayLarger([561,259])
displayLarger([607,263])
displayLarger([630,238])
displayLarger([658,245])
displayLarger([670,265])
displayLarger([707,266])
displayLarger([747,343])
displayLarger([741,375])
displayLarger([748,383])
displayLarger([765,431])
displayLarger([845,455])
displayLarger([855,503])
displayLarger([861,508])
displayLarger([894,514])
displayLarger([998,567])
displayLarger([1004,584])
displayLarger([998,600])
displayLarger([999,618])
displayLarger([1057,653])
displayLarger([1194,602])
displayLarger([1270,582])
displayLarger([1286,609])
displayLarger([1390,640])
displayLarger([1482,655])
displayLarger([1452,750])
displayLarger([1442,770])
displayLarger([1438,771])
displayLarger([1421,851])
displayLarger([1449,868])
displayLarger([1462,926])
displayLarger([1521,957])
displayLarger([1567,987])
displayLarger([1773,1106])
displayLarger([1787,1104])
displayLarger([1791,1103])
displayLarger([1809,1127])
displayLarger([1841,1157])
displayLarger([1849,1195])
displayLarger([1913,1326])
displayLarger([1915,1334])
displayLarger([1937,1375])
displayLarger([1941,1379])
displayLarger([1963,1396])
displayLarger([1961,1466])
displayLarger([1974,1523])
displayLarger([1975,1531])

from matplotlib import pyplot as plt

plt.imshow(back_voronoi_par_proc)
plt.show()