Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc

Namespace SimpleCrud
    Public Class Address_TypeController
        Inherits Controller

        Private db As New SimpleCrudTestEntities

        ' GET: Address_Type
        Function Index() As ActionResult
            Return View(db.Address_Type.ToList())
        End Function

        ' GET: Address_Type/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Address_Type/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Address_Type_Id,Description")> ByVal address_Type As Address_Type) As ActionResult
            If ModelState.IsValid Then
                db.Address_Type.Add(address_Type)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(address_Type)
        End Function

        ' GET: Address_Type/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim address_Type As Address_Type = db.Address_Type.Find(id)
            If IsNothing(address_Type) Then
                Return HttpNotFound()
            End If
            Return View(address_Type)
        End Function

        ' POST: Address_Type/Edit/5
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Address_Type_Id,Description")> ByVal address_Type As Address_Type) As ActionResult
            If ModelState.IsValid Then
                db.Entry(address_Type).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(address_Type)
        End Function

        ' GET: Address_Type/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim address_Type As Address_Type = db.Address_Type.Find(id)
            If IsNothing(address_Type) Then
                Return HttpNotFound()
            End If
            Return View(address_Type)
        End Function

        ' POST: Address_Type/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim address_Type As Address_Type = db.Address_Type.Find(id)
            db.Address_Type.Remove(address_Type)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
