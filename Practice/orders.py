# PrintOrders.py
# This program imports data from the file "orders.txt"
# and displays the order information, including line items,
# shipping cost and total invoice amount
#

import customer.py

grandTotal = 0
subTotal = 0

def startNewOrder(line):
    customerID = line[0:5]
    customerName = line[6:30]
    address1 = line[31:55]
    address2 = line[56:80]
    city = line[81:100]
    state = line[101:102]
    zip4 = line[103:111]
    shipCode = line[112:113]

def addItem(line):    
    itemCode = line[114:117]
    quantity = line[118:119]
    description = line[120:129]
    cost = line[130:136]

def finishOrder():

def main():
    currCustomerID = ''
    custFile = open("orders.txt");
    for order in custFile:
        if (order[0:5] == currCustomerID):
            addItem(line);
        elif currCustomerID != '':
            finishOrder(line);
            startNewOrder(order);
        print(order)

    
main()
    
