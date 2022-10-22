

import pickle


folderNameSource = "C:/github_ws/scripts-space-engineers/rover_autopilot_gg_vanilla/game_data/SS/PlanetDataFiles/Pertam/"

file_path = folderNameSource + "nodes_run1.pickle"

with open(file_path,'rb') as f:
    nodes = pickle.load(f)


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

plt.show()

#
# # Data for a three-dimensional line
# zline = np.linspace(0, 15, 1000)
# xline = np.sin(zline)
# yline = np.cos(zline)
# ax.plot3D(xline, yline, zline, 'gray')