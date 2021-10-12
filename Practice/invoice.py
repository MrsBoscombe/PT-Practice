# OrderList.py
# This program imports data from the file "orders.txt"
# and displays the order information, including line items,
# shipping cost and total order amount


from customer import *

def main():
    custList = []
    currCustomerID = ''
    custFile = open("orders.txt");

    for line in custFile:
        if (line[0:5] != currCustomerID):
            currCustomerID = line[0:5]
            cust = Customer()
            cust.setInfo(line)
            custList.append(cust)
        order = Order()
        order.setInfo(line)
        custList[len(custList)-1].addOrder(order)
    
    for customer in custList:
        print(customer);
    
main()
    
