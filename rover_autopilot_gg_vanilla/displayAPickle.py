import pickle

with open('back_voronoi_par_proc.pickle', 'rb') as f:
    back_voronoi_par_proc = pickle.load(f)
# print("len(arrayOfCirclesPointsList):",str(len(arrayOfCirclesPointsList)))


from matplotlib import pyplot as plt

plt.imshow(back_voronoi_par_proc)
plt.show()