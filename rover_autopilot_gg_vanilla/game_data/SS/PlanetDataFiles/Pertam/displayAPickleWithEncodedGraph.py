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

displayLarger([385,1290])
displayLarger([393,1193])
displayLarger([426,1186])



from matplotlib import pyplot as plt

def displayLine(point1 , point2):
    plt.plot([point1[0],point2[0]],[point1[1],point2[1]])
    pass



displayLine([500,500] , [550,550])

plt.imshow(back_voronoi_par_proc)
plt.show()