# class Customer
# This file defines the Customer class
# including customer information, such as
# ID, Name, Address, ship code and
# associated orders

from order import *

class Customer:

    def __init__(self):
        self.customerID = ""
        self.customerName = ""
        self.address1 = ""
        self.address2 = ""
        self.city = ""
        self.state = ""
        self.zip4 = ""
        self.shipCode = ""
        self.orders = []

    def setInfo(self, line):
         self.customerID = line[0:6]
         self.customerName = line[6:31]
         self.address1 = line[31:55]
         self.address2 = line[56:81]
         self.city = line[81:101]
         self.city = self.city.strip()
         self.state = line[101:103]
         self.zip4 = line[103:112]
         self.shipCode = int(line[112:114])


    def getShipCost(self):
        if (self.shipCode == 0):    # shipping is free
            return 0.00
        elif (self.shipCode == 2):  # flat rate of $5
            return 5.00
        else:                       # shipping is 5% of the order
            return round(.05 * self.getTotal(), 2)
    

    def getTotal(self):
        total = 0.00
        for o in self.orders:
            total += (float)(o.getCost())
        return round(total, 2)

    def addOrder(self, order):
        self.orders.append(order);

    def __str__(self):
        msg = self.customerID + "\n" + self.customerName + "\n" + self.address1 + "\n"
        if (len(self.address2) > 0):
            msg += self.address2 + "\n"
        msg += self.city + ", " + self.state + " " + self.zip4[0:5]
        if (self.zip4[5:] != "0000"):
            msg += "-" + self.zip4[5:]
        msg += "\n"
        for o in self.orders:
            msg = msg + (str)(o) + "\n"
        msg += "Shipping: " + str(self.getShipCost()) + "\n"
        msg += "Total: " + str(round(self.getTotal() + self.getShipCost(), 2))
        msg += "\n-----------------------------------------------------\n"

        return msg
