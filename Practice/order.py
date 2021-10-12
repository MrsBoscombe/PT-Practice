# class Order
# This file defines the Order class
# including quantity, description and cost

class Order:

    def __init__ (self):
        self.itemCode = ""
        self.quantity = 0
        self.description = ""
        self.cost = 0.00

    def setInfo (self, line):
        self.itemCode = line[114:118]
        self.quantity = line[118:120]
        self.description = line[120:130]
        self.cost = line[130:137]

    def getCost(self):
        return (float)(self.quantity)*(float)(self.cost)

    def __str__(self):
        order = self.itemCode + " " + self.quantity + " " + self.description + " " + self.cost + " "
        order += '%10s' % str(self.getCost())
        return order
