Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc

Namespace SimpleCrud
    Public Class CustomersController
        Inherits System.Web.Mvc.Controller

        Private db As New SimpleCrudTestEntities

        ' GET: Customers
        Function Index() As ActionResult
            Return View(db.Customers.Include("Customer_Address").ToList())
        End Function

        ' GET: Customers/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Find(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

        ' GET: Customers/Create
        Function Create() As ActionResult
            setDropDownList()
            Return View()
        End Function

        ' POST: Customers/Create
        <HttpPost()>
        Function Create(ByVal customer As Customer) As ActionResult
            db.Customers.Add(customer)
            db.Addresses.AddRange(customer.Addresses)
            db.SaveChanges()
            For Each address As Address In customer.Addresses
                Dim customerAddress As Customer_Address = New Customer_Address
                customerAddress.Customer_Id = customer.Customer_Id
                customerAddress.Address_Id = address.Address_Id
                customerAddress.Address_Type_Id = address.Address_Type_Id
                db.Customer_Address.Add(customerAddress)
            Next
            db.SaveChanges()
            Return Json(Url.Action("Index", "Customers"))
        End Function

        ' GET: Customers/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Include("Customer_Address").FirstOrDefault(Function(i) i.Customer_Id = id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            setDropDownList()
            Return View(customer)
        End Function

        ' POST: Customers/Edit/5
        <HttpPost()>
        Function Edit(ByVal customer As Customer) As ActionResult

            Dim newAdresses As List(Of Address) = New List(Of Address)
            Dim addressNotModified As List(Of Integer) = New List(Of Integer)

            For Each address As Address In customer.Addresses
                If (address.Address_Id = 0) Then
                    newAdresses.Add(address)
                Else
                    addressNotModified.Add(address.Address_Id)
                End If
            Next

            Dim addressesToBeRemoved As List(Of Customer_Address) = db.Customer_Address.Include("Address").Where(Function(x) Not addressNotModified.Contains(x.Address_Id) And x.Customer_Id = customer.Customer_Id).ToList()
            DeleteCustomerAddresses(addressesToBeRemoved)

            If newAdresses.Count > 0 Then
                db.Addresses.AddRange(newAdresses)
            End If
            Dim result = db.SaveChanges()

            If result > 0 Then
                For Each address As Address In newAdresses
                    Dim customerAddress As Customer_Address = New Customer_Address
                    customerAddress.Customer_Id = customer.Customer_Id
                    customerAddress.Address_Id = address.Address_Id
                    customerAddress.Address_Type_Id = address.Address_Type_Id
                    db.Customer_Address.Add(customerAddress)
                Next

            End If
            db.SaveChanges()
            Return Json(Url.Action("Index", "Customers"))
        End Function

        ' POST: Customers/DeleteCustomerAddress/5
        Function DeleteCustomerAddress(ByVal customerAddress As Customer_Address) As ActionResult
            DeleteCustomerAddress(customerAddress.Customer_Address_Id)
            Return Json(True)
        End Function

        Sub DeleteCustomerAddresses(ByVal CustomerAddressToDeleted As List(Of Customer_Address))
            Dim addressesList As List(Of Address) = New List(Of Address)
            For Each customer_Address As Customer_Address In CustomerAddressToDeleted
                addressesList.Add(customer_Address.Address)
                db.Customer_Address.Remove(customer_Address)
            Next
            Dim Deleted = db.SaveChanges()
            If (Deleted > 0) Then
                db.Addresses.RemoveRange(addressesList)
                db.SaveChanges()
            End If
        End Sub

        Sub DeleteCustomerAddress(ByVal CustomerAddressId)
            Dim customer_Address As Customer_Address = db.Customer_Address.Include("Address").FirstOrDefault(Function(i) i.Customer_Address_Id = CustomerAddressId)
            Dim address As Address = customer_Address.Address
            db.Customer_Address.Remove(customer_Address)
            Dim Deleted = db.SaveChanges()
            If (Deleted > 0) Then
                db.Addresses.Remove(address)
                db.SaveChanges()
            End If
        End Sub

        ' GET: Customers/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Find(id)

            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

        ' POST: Customers/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim customer_Addresses As List(Of Customer_Address) = db.Customer_Address.Include("Customer").Include("Address").Where(Function(i) i.Customer_Id = id).ToList()

            Dim addresses As List(Of Address) = New List(Of Address)
            Dim customers As List(Of Customer) = New List(Of Customer)
            For Each customerAddress As Customer_Address In customer_Addresses
                addresses.Add(customerAddress.Address)
                customers.Add(customerAddress.Customer)
            Next

            db.Customer_Address.RemoveRange(customer_Addresses)
            Dim Deleted = db.SaveChanges()
            If (Deleted > 0) Then
                db.Addresses.RemoveRange(addresses)
                db.Customers.RemoveRange(customers)
                db.SaveChanges()
            End If

            Return RedirectToAction("Index")
        End Function

        Private Sub setDropDownList()
            ViewBag.AddressTypes = New SelectList(db.Address_Type, "Address_Type_Id", "Description")
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
