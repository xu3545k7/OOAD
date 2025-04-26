'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' ALGLIB 4.04.0 (source code generated 2024-12-21)
' Copyright (c) Sergey Bochkanov (ALGLIB project).
' 
' >>> SOURCE LICENSE >>>
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation (www.fsf.org); either version 2 of the 
' License, or (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' A copy of the GNU General Public License is available at
' http://www.fsf.org/licensing/licenses
' >>> END OF LICENSE >>>
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Module XAlglib

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Callback definitions for optimizers/fitters/solvers.
'    
'    Callbacks for unparameterized (general) functions:
'    * ndimensional_func         calculates f(arg), stores result to func
'    * ndimensional_grad         calculates func = f(arg), 
'                                grad[i] = df(arg)/d(arg[i])
'    * ndimensional_hess         calculates func = f(arg),
'                                grad[i] = df(arg)/d(arg[i]),
'                                hess[i,j] = d2f(arg)/(d(arg[i])*d(arg[j]))
'    
'    Callbacks for systems of functions:
'    * ndimensional_fvec         calculates vector function f(arg),
'                                stores result to fi
'    * ndimensional_jac          calculates f[i] = fi(arg)
'                                jac[i,j] = df[i](arg)/d(arg[j])
'                                
'    Callbacks for  parameterized  functions,  i.e.  for  functions  which 
'    depend on two vectors: P and Q.  Gradient  and Hessian are calculated 
'    with respect to P only.
'    * ndimensional_pfunc        calculates f(p,q),
'                                stores result to func
'    * ndimensional_pgrad        calculates func = f(p,q),
'                                grad[i] = df(p,q)/d(p[i])
'    * ndimensional_phess        calculates func = f(p,q),
'                                grad[i] = df(p,q)/d(p[i]),
'                                hess[i,j] = d2f(p,q)/(d(p[i])*d(p[j]))
'
'    Callbacks for progress reports:
'    * ndimensional_rep          reports current position of optimization algo    
'    
'    Callbacks for ODE solvers:
'    * ndimensional_ode_rp       calculates dy/dx for given y[] and x
'    
'    Callbacks for integrators:
'    * integrator1_func          calculates f(x) for given x
'                                (additional parameters xminusa and bminusx
'                                contain x-a and b-x)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Delegate Sub ndimensional_func(arg As Double(), ByRef func As Double, obj As Object)
Public Delegate Sub ndimensional_grad(arg As Double(), ByRef func As Double, grad As Double(), obj As Object)
Public Delegate Sub ndimensional_hess(arg As Double(), ByRef func As Double, grad As Double(), hess As Double(,), obj As Object)

Public Delegate Sub ndimensional_fvec(arg As Double(), fi As Double(), obj As Object)
Public Delegate Sub ndimensional_jac(arg As Double(), fi As Double(), jac As Double(,), obj As Object)
Public Delegate Sub ndimensional_sjac(arg As Double(), fi As Double(), sjac As sparsematrix, obj As Object)

Public Delegate Sub ndimensional_pfunc(p As Double(), q As Double(), ByRef func As Double, obj As Object)
Public Delegate Sub ndimensional_pgrad(p As Double(), q As Double(), ByRef func As Double, grad As Double(), obj As Object)
Public Delegate Sub ndimensional_phess(p As Double(), q As Double(), ByRef func As Double, grad As Double(), hess As Double(,), obj As Object)

Public Delegate Sub ndimensional_rep(arg As Double(), func As Double, obj As Object)

Public Delegate Sub ndimensional_ode_rp(y As Double(), x As Double, dy As Double(), obj As Object)

Public Delegate Sub integrator1_func(x As Double, xminusa As Double, bminusx As Double, ByRef f As Double, obj As Object)

'
' ALGLIB exception
'
Public Class AlglibException
    Inherits System.ApplicationException
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

'
' Change number of worker threads
'
Public Sub setnworkers(ByVal nworkers As Integer)
    alglib.setnworkers(nworkers)
End Sub

'
' Proxy classes that convert a VB.NET delegate to a C# delegate
'
Public Class SJacConverter
    Private vbDel As ndimensional_sjac
    Private vbSp  As sparsematrix
    Public Sub New(vbDelegate As ndimensional_sjac)
        Me.vbDel = vbDelegate
        Me.vbSp = New sparsematrix()
    End Sub
    Public Sub CsCbk(arg As Double(), fi As Double(), sjac As alglib.sparsematrix, obj As Object)
        Me.vbSp.csobj = sjac
        vbDel(arg, fi, Me.vbSp, obj)
    End Sub
End Class

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This is a debug class intended for testing ALGLIB interface generator.
    'Never use it in any real life project.
    '
    '  -- ALGLIB --
    '     Copyright 20.07.2021 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class xdebugrecord1
        Public Property i() As Integer
        Get
            Return csobj.i
        End Get
        Set(ByVal Value As Integer)
            csobj.i = Value
        End Set
        End Property
        Public Property c() As alglib.complex
        Get
            Return csobj.c
        End Get
        Set(ByVal Value As alglib.complex)
            csobj.c = Value
        End Set
        End Property
        Public Property a() As Double()
        Get
            Return csobj.a
        End Get
        Set(ByVal Value As Double())
            csobj.a = Value
        End Set
        End Property
        Public csobj As alglib.xdebugrecord1
    End Class


    Public Sub xdebuginitrecord1(ByRef rec1 As xdebugrecord1)
        Try
            rec1 = New xdebugrecord1()
            alglib.xdebuginitrecord1(rec1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugupdaterecord1(ByRef rec1 As xdebugrecord1)
        Try
            alglib.xdebugupdaterecord1(rec1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugb1count(ByVal a() As Boolean) As Integer
        Try
            xdebugb1count = alglib.xdebugb1count(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugb1not(ByRef a() As Boolean)
        Try
            alglib.xdebugb1not(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugb1appendcopy(ByRef a() As Boolean)
        Try
            alglib.xdebugb1appendcopy(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugb1outeven(ByVal n As Integer, ByRef a() As Boolean)
        Try
            alglib.xdebugb1outeven(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugi1sum(ByVal a() As Integer) As Integer
        Try
            xdebugi1sum = alglib.xdebugi1sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugi1neg(ByRef a() As Integer)
        Try
            alglib.xdebugi1neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugi1appendcopy(ByRef a() As Integer)
        Try
            alglib.xdebugi1appendcopy(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugi1outeven(ByVal n As Integer, ByRef a() As Integer)
        Try
            alglib.xdebugi1outeven(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugr1sum(ByVal a() As Double) As Double
        Try
            xdebugr1sum = alglib.xdebugr1sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function xdebugr1internalcopyandsum(ByVal a() As Double) As Double
        Try
            xdebugr1internalcopyandsum = alglib.xdebugr1internalcopyandsum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugr1neg(ByRef a() As Double)
        Try
            alglib.xdebugr1neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugr1appendcopy(ByRef a() As Double)
        Try
            alglib.xdebugr1appendcopy(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugr1outeven(ByVal n As Integer, ByRef a() As Double)
        Try
            alglib.xdebugr1outeven(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugc1sum(ByVal a() As alglib.complex) As alglib.complex
        Try
            xdebugc1sum = alglib.xdebugc1sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugc1neg(ByRef a() As alglib.complex)
        Try
            alglib.xdebugc1neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugc1appendcopy(ByRef a() As alglib.complex)
        Try
            alglib.xdebugc1appendcopy(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugc1outeven(ByVal n As Integer, ByRef a() As alglib.complex)
        Try
            alglib.xdebugc1outeven(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugb2count(ByVal a(,) As Boolean) As Integer
        Try
            xdebugb2count = alglib.xdebugb2count(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugb2not(ByRef a(,) As Boolean)
        Try
            alglib.xdebugb2not(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugb2transpose(ByRef a(,) As Boolean)
        Try
            alglib.xdebugb2transpose(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugb2outsin(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As Boolean)
        Try
            alglib.xdebugb2outsin(m, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugi2sum(ByVal a(,) As Integer) As Integer
        Try
            xdebugi2sum = alglib.xdebugi2sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugi2neg(ByRef a(,) As Integer)
        Try
            alglib.xdebugi2neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugi2transpose(ByRef a(,) As Integer)
        Try
            alglib.xdebugi2transpose(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugi2outsin(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As Integer)
        Try
            alglib.xdebugi2outsin(m, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugr2sum(ByVal a(,) As Double) As Double
        Try
            xdebugr2sum = alglib.xdebugr2sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function xdebugr2internalcopyandsum(ByVal a(,) As Double) As Double
        Try
            xdebugr2internalcopyandsum = alglib.xdebugr2internalcopyandsum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugr2neg(ByRef a(,) As Double)
        Try
            alglib.xdebugr2neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugr2transpose(ByRef a(,) As Double)
        Try
            alglib.xdebugr2transpose(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugr2outsin(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As Double)
        Try
            alglib.xdebugr2outsin(m, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugc2sum(ByVal a(,) As alglib.complex) As alglib.complex
        Try
            xdebugc2sum = alglib.xdebugc2sum(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub xdebugc2neg(ByRef a(,) As alglib.complex)
        Try
            alglib.xdebugc2neg(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugc2transpose(ByRef a(,) As alglib.complex)
        Try
            alglib.xdebugc2transpose(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdebugc2outsincos(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As alglib.complex)
        Try
            alglib.xdebugc2outsincos(m, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function xdebugmaskedbiasedproductsum(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal b(,) As Double, ByVal c(,) As Boolean) As Double
        Try
            xdebugmaskedbiasedproductsum = alglib.xdebugmaskedbiasedproductsum(m, n, a, b, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    Public Class hqrndstate
        Public csobj As alglib.hqrndstate
    End Class


    Public Sub hqrndrandomize(ByRef state As hqrndstate)
        Try
            state = New hqrndstate()
            alglib.hqrndrandomize(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hqrndseed(ByVal s1 As Integer, ByVal s2 As Integer, ByRef state As hqrndstate)
        Try
            state = New hqrndstate()
            alglib.hqrndseed(s1, s2, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hqrnduniformr(ByRef state As hqrndstate) As Double
        Try
            hqrnduniformr = alglib.hqrnduniformr(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hqrnduniformi(ByRef state As hqrndstate, ByVal n As Integer) As Integer
        Try
            hqrnduniformi = alglib.hqrnduniformi(state.csobj, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hqrndnormal(ByRef state As hqrndstate) As Double
        Try
            hqrndnormal = alglib.hqrndnormal(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hqrndnormalv(ByRef state As hqrndstate, ByVal n As Integer, ByRef x() As Double)
        Try
            alglib.hqrndnormalv(state.csobj, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hqrndnormalm(ByRef state As hqrndstate, ByVal m As Integer, ByVal n As Integer, ByRef x(,) As Double)
        Try
            alglib.hqrndnormalm(state.csobj, m, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hqrndunit2(ByRef state As hqrndstate, ByRef x As Double, ByRef y As Double)
        Try
            alglib.hqrndunit2(state.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hqrndnormal2(ByRef state As hqrndstate, ByRef x1 As Double, ByRef x2 As Double)
        Try
            alglib.hqrndnormal2(state.csobj, x1, x2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hqrndexponential(ByRef state As hqrndstate, ByVal lambdav As Double) As Double
        Try
            hqrndexponential = alglib.hqrndexponential(state.csobj, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hqrnddiscrete(ByRef state As hqrndstate, ByVal x() As Double, ByVal n As Integer) As Double
        Try
            hqrnddiscrete = alglib.hqrnddiscrete(state.csobj, x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hqrndcontinuous(ByRef state As hqrndstate, ByVal x() As Double, ByVal n As Integer) As Double
        Try
            hqrndcontinuous = alglib.hqrndcontinuous(state.csobj, x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub cmatrixtranspose(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByRef b(,) As alglib.complex, ByVal ib As Integer, ByVal jb As Integer)
        Try
            alglib.cmatrixtranspose(m, n, a, ia, ja, b, ib, jb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixtranspose(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByRef b(,) As Double, ByVal ib As Integer, ByVal jb As Integer)
        Try
            alglib.rmatrixtranspose(m, n, a, ia, ja, b, ib, jb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixenforcesymmetricity(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean)
        Try
            alglib.rmatrixenforcesymmetricity(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixcopy(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByRef b(,) As alglib.complex, ByVal ib As Integer, ByVal jb As Integer)
        Try
            alglib.cmatrixcopy(m, n, a, ia, ja, b, ib, jb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rvectorcopy(ByVal n As Integer, ByVal a() As Double, ByVal ia As Integer, ByRef b() As Double, ByVal ib As Integer)
        Try
            alglib.rvectorcopy(n, a, ia, b, ib)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixcopy(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByRef b(,) As Double, ByVal ib As Integer, ByVal jb As Integer)
        Try
            alglib.rmatrixcopy(m, n, a, ia, ja, b, ib, jb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixgencopy(ByVal m As Integer, ByVal n As Integer, ByVal alpha As Double, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal beta As Double, ByRef b(,) As Double, ByVal ib As Integer, ByVal jb As Integer)
        Try
            alglib.rmatrixgencopy(m, n, alpha, a, ia, ja, beta, b, ib, jb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixger(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal alpha As Double, ByVal u() As Double, ByVal iu As Integer, ByVal v() As Double, ByVal iv As Integer)
        Try
            alglib.rmatrixger(m, n, a, ia, ja, alpha, u, iu, v, iv)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrank1(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByVal u() As alglib.complex, ByVal iu As Integer, ByVal v() As alglib.complex, ByVal iv As Integer)
        Try
            alglib.cmatrixrank1(m, n, a, ia, ja, u, iu, v, iv)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixrank1(ByVal m As Integer, ByVal n As Integer, ByRef a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal u() As Double, ByVal iu As Integer, ByVal v() As Double, ByVal iv As Integer)
        Try
            alglib.rmatrixrank1(m, n, a, ia, ja, u, iu, v, iv)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixgemv(ByVal m As Integer, ByVal n As Integer, ByVal alpha As Double, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal opa As Integer, ByVal x() As Double, ByVal ix As Integer, ByVal beta As Double, ByRef y() As Double, ByVal iy As Integer)
        Try
            alglib.rmatrixgemv(m, n, alpha, a, ia, ja, opa, x, ix, beta, y, iy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixmv(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByVal opa As Integer, ByVal x() As alglib.complex, ByVal ix As Integer, ByRef y() As alglib.complex, ByVal iy As Integer)
        Try
            alglib.cmatrixmv(m, n, a, ia, ja, opa, x, ix, y, iy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixmv(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal opa As Integer, ByVal x() As Double, ByVal ix As Integer, ByRef y() As Double, ByVal iy As Integer)
        Try
            alglib.rmatrixmv(m, n, a, ia, ja, opa, x, ix, y, iy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixsymv(ByVal n As Integer, ByVal alpha As Double, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal isupper As Boolean, ByVal x() As Double, ByVal ix As Integer, ByVal beta As Double, ByRef y() As Double, ByVal iy As Integer)
        Try
            alglib.rmatrixsymv(n, alpha, a, ia, ja, isupper, x, ix, beta, y, iy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rmatrixsyvmv(ByVal n As Integer, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal isupper As Boolean, ByVal x() As Double, ByVal ix As Integer, ByRef tmp() As Double) As Double
        Try
            rmatrixsyvmv = alglib.rmatrixsyvmv(n, a, ia, ja, isupper, x, ix, tmp)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixtrsv(ByVal n As Integer, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x() As Double, ByVal ix As Integer)
        Try
            alglib.rmatrixtrsv(n, a, ia, ja, isupper, isunit, optype, x, ix)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrighttrsm(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As alglib.complex, ByVal i1 As Integer, ByVal j1 As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x(,) As alglib.complex, ByVal i2 As Integer, ByVal j2 As Integer)
        Try
            alglib.cmatrixrighttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlefttrsm(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As alglib.complex, ByVal i1 As Integer, ByVal j1 As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x(,) As alglib.complex, ByVal i2 As Integer, ByVal j2 As Integer)
        Try
            alglib.cmatrixlefttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixrighttrsm(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal i1 As Integer, ByVal j1 As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x(,) As Double, ByVal i2 As Integer, ByVal j2 As Integer)
        Try
            alglib.rmatrixrighttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlefttrsm(ByVal m As Integer, ByVal n As Integer, ByVal a(,) As Double, ByVal i1 As Integer, ByVal j1 As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x(,) As Double, ByVal i2 As Integer, ByVal j2 As Integer)
        Try
            alglib.rmatrixlefttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixherk(ByVal n As Integer, ByVal k As Integer, ByVal alpha As Double, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByVal optypea As Integer, ByVal beta As Double, ByRef c(,) As alglib.complex, ByVal ic As Integer, ByVal jc As Integer, ByVal isupper As Boolean)
        Try
            alglib.cmatrixherk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixsyrk(ByVal n As Integer, ByVal k As Integer, ByVal alpha As Double, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal optypea As Integer, ByVal beta As Double, ByRef c(,) As Double, ByVal ic As Integer, ByVal jc As Integer, ByVal isupper As Boolean)
        Try
            alglib.rmatrixsyrk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixgemm(ByVal m As Integer, ByVal n As Integer, ByVal k As Integer, ByVal alpha As alglib.complex, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByVal optypea As Integer, ByVal b(,) As alglib.complex, ByVal ib As Integer, ByVal jb As Integer, ByVal optypeb As Integer, ByVal beta As alglib.complex, ByRef c(,) As alglib.complex, ByVal ic As Integer, ByVal jc As Integer)
        Try
            alglib.cmatrixgemm(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixgemm(ByVal m As Integer, ByVal n As Integer, ByVal k As Integer, ByVal alpha As Double, ByVal a(,) As Double, ByVal ia As Integer, ByVal ja As Integer, ByVal optypea As Integer, ByVal b(,) As Double, ByVal ib As Integer, ByVal jb As Integer, ByVal optypeb As Integer, ByVal beta As Double, ByRef c(,) As Double, ByVal ic As Integer, ByVal jc As Integer)
        Try
            alglib.rmatrixgemm(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixsyrk(ByVal n As Integer, ByVal k As Integer, ByVal alpha As Double, ByVal a(,) As alglib.complex, ByVal ia As Integer, ByVal ja As Integer, ByVal optypea As Integer, ByVal beta As Double, ByRef c(,) As alglib.complex, ByVal ic As Integer, ByVal jc As Integer, ByVal isupper As Boolean)
        Try
            alglib.cmatrixsyrk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub rmatrixqr(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef tau() As Double)
        Try
            alglib.rmatrixqr(a, m, n, tau)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlq(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef tau() As Double)
        Try
            alglib.rmatrixlq(a, m, n, tau)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixqr(ByRef a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByRef tau() As alglib.complex)
        Try
            alglib.cmatrixqr(a, m, n, tau)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlq(ByRef a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByRef tau() As alglib.complex)
        Try
            alglib.cmatrixlq(a, m, n, tau)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixqrunpackq(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal tau() As Double, ByVal qcolumns As Integer, ByRef q(,) As Double)
        Try
            alglib.rmatrixqrunpackq(a, m, n, tau, qcolumns, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixqrunpackr(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef r(,) As Double)
        Try
            alglib.rmatrixqrunpackr(a, m, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlqunpackq(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal tau() As Double, ByVal qrows As Integer, ByRef q(,) As Double)
        Try
            alglib.rmatrixlqunpackq(a, m, n, tau, qrows, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlqunpackl(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef l(,) As Double)
        Try
            alglib.rmatrixlqunpackl(a, m, n, l)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixqrunpackq(ByVal a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByVal tau() As alglib.complex, ByVal qcolumns As Integer, ByRef q(,) As alglib.complex)
        Try
            alglib.cmatrixqrunpackq(a, m, n, tau, qcolumns, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixqrunpackr(ByVal a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByRef r(,) As alglib.complex)
        Try
            alglib.cmatrixqrunpackr(a, m, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlqunpackq(ByVal a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByVal tau() As alglib.complex, ByVal qrows As Integer, ByRef q(,) As alglib.complex)
        Try
            alglib.cmatrixlqunpackq(a, m, n, tau, qrows, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlqunpackl(ByVal a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByRef l(,) As alglib.complex)
        Try
            alglib.cmatrixlqunpackl(a, m, n, l)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbd(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef tauq() As Double, ByRef taup() As Double)
        Try
            alglib.rmatrixbd(a, m, n, tauq, taup)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbdunpackq(ByVal qp(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal tauq() As Double, ByVal qcolumns As Integer, ByRef q(,) As Double)
        Try
            alglib.rmatrixbdunpackq(qp, m, n, tauq, qcolumns, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbdmultiplybyq(ByVal qp(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal tauq() As Double, ByRef z(,) As Double, ByVal zrows As Integer, ByVal zcolumns As Integer, ByVal fromtheright As Boolean, ByVal dotranspose As Boolean)
        Try
            alglib.rmatrixbdmultiplybyq(qp, m, n, tauq, z, zrows, zcolumns, fromtheright, dotranspose)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbdunpackpt(ByVal qp(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal taup() As Double, ByVal ptrows As Integer, ByRef pt(,) As Double)
        Try
            alglib.rmatrixbdunpackpt(qp, m, n, taup, ptrows, pt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbdmultiplybyp(ByVal qp(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal taup() As Double, ByRef z(,) As Double, ByVal zrows As Integer, ByVal zcolumns As Integer, ByVal fromtheright As Boolean, ByVal dotranspose As Boolean)
        Try
            alglib.rmatrixbdmultiplybyp(qp, m, n, taup, z, zrows, zcolumns, fromtheright, dotranspose)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixbdunpackdiagonals(ByVal b(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef isupper As Boolean, ByRef d() As Double, ByRef e() As Double)
        Try
            alglib.rmatrixbdunpackdiagonals(b, m, n, isupper, d, e)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixhessenberg(ByRef a(,) As Double, ByVal n As Integer, ByRef tau() As Double)
        Try
            alglib.rmatrixhessenberg(a, n, tau)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixhessenbergunpackq(ByVal a(,) As Double, ByVal n As Integer, ByVal tau() As Double, ByRef q(,) As Double)
        Try
            alglib.rmatrixhessenbergunpackq(a, n, tau, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixhessenbergunpackh(ByVal a(,) As Double, ByVal n As Integer, ByRef h(,) As Double)
        Try
            alglib.rmatrixhessenbergunpackh(a, n, h)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub smatrixtd(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef tau() As Double, ByRef d() As Double, ByRef e() As Double)
        Try
            alglib.smatrixtd(a, n, isupper, tau, d, e)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub smatrixtdunpackq(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal tau() As Double, ByRef q(,) As Double)
        Try
            alglib.smatrixtdunpackq(a, n, isupper, tau, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hmatrixtd(ByRef a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef tau() As alglib.complex, ByRef d() As Double, ByRef e() As Double)
        Try
            alglib.hmatrixtd(a, n, isupper, tau, d, e)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hmatrixtdunpackq(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal tau() As alglib.complex, ByRef q(,) As alglib.complex)
        Try
            alglib.hmatrixtdunpackq(a, n, isupper, tau, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub rmatrixrndorthogonal(ByVal n As Integer, ByRef a(,) As Double)
        Try
            alglib.rmatrixrndorthogonal(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As Double)
        Try
            alglib.rmatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrndorthogonal(ByVal n As Integer, ByRef a(,) As alglib.complex)
        Try
            alglib.cmatrixrndorthogonal(n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As alglib.complex)
        Try
            alglib.cmatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub smatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As Double)
        Try
            alglib.smatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As Double)
        Try
            alglib.spdmatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hmatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As alglib.complex)
        Try
            alglib.hmatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixrndcond(ByVal n As Integer, ByVal c As Double, ByRef a(,) As alglib.complex)
        Try
            alglib.hpdmatrixrndcond(n, c, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixrndorthogonalfromtheright(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer)
        Try
            alglib.rmatrixrndorthogonalfromtheright(a, m, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixrndorthogonalfromtheleft(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer)
        Try
            alglib.rmatrixrndorthogonalfromtheleft(a, m, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrndorthogonalfromtheright(ByRef a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer)
        Try
            alglib.cmatrixrndorthogonalfromtheright(a, m, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixrndorthogonalfromtheleft(ByRef a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer)
        Try
            alglib.cmatrixrndorthogonalfromtheleft(a, m, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub smatrixrndmultiply(ByRef a(,) As Double, ByVal n As Integer)
        Try
            alglib.smatrixrndmultiply(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hmatrixrndmultiply(ByRef a(,) As alglib.complex, ByVal n As Integer)
        Try
            alglib.hmatrixrndmultiply(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class sparsematrix
        Public csobj As alglib.sparsematrix
    End Class
    Public Class sparsebuffers
        Public csobj As alglib.sparsebuffers
    End Class
    Public Sub sparseserialize(ByVal obj As sparsematrix, ByRef s_out As String)
        Try
            alglib.sparseserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub sparseunserialize(ByVal s_in As String, ByRef obj As sparsematrix)
        Try
            alglib.sparseunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreate(ByVal m As Integer, ByVal n As Integer, ByVal k As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreate(m, n, k, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreate(ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreate(m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatebuf(ByVal m As Integer, ByVal n As Integer, ByVal k As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatebuf(m, n, k, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatebuf(ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatebuf(m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrs(ByVal m As Integer, ByVal n As Integer, ByVal ner() As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatecrs(m, n, ner, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsbuf(ByVal m As Integer, ByVal n As Integer, ByVal ner() As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatecrsbuf(m, n, ner, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsempty(ByVal n As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatecrsempty(n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsemptybuf(ByVal n As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatecrsemptybuf(n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdense(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatecrsfromdense(a, m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdense(ByVal a(,) As Double, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatecrsfromdense(a, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdensebuf(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatecrsfromdensebuf(a, m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdensebuf(ByVal a(,) As Double, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatecrsfromdensebuf(a, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdensev(ByVal a() As Double, ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatecrsfromdensev(a, m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatecrsfromdensevbuf(ByVal a() As Double, ByVal m As Integer, ByVal n As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatecrsfromdensevbuf(a, m, n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatesks(ByVal m As Integer, ByVal n As Integer, ByVal d() As Integer, ByVal u() As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatesks(m, n, d, u, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatesksbuf(ByVal m As Integer, ByVal n As Integer, ByVal d() As Integer, ByVal u() As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatesksbuf(m, n, d, u, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatesksband(ByVal m As Integer, ByVal n As Integer, ByVal bw As Integer, ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsecreatesksband(m, n, bw, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecreatesksbandbuf(ByVal m As Integer, ByVal n As Integer, ByVal bw As Integer, ByRef s As sparsematrix)
        Try
            alglib.sparsecreatesksbandbuf(m, n, bw, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopy(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            s1 = New sparsematrix()
            alglib.sparsecopy(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopybuf(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopybuf(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseswap(ByRef s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparseswap(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseadd(ByRef s As sparsematrix, ByVal i As Integer, ByVal j As Integer, ByVal v As Double)
        Try
            alglib.sparseadd(s.csobj, i, j, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseset(ByRef s As sparsematrix, ByVal i As Integer, ByVal j As Integer, ByVal v As Double)
        Try
            alglib.sparseset(s.csobj, i, j, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparseget(ByVal s As sparsematrix, ByVal i As Integer, ByVal j As Integer) As Double
        Try
            sparseget = alglib.sparseget(s.csobj, i, j)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparseexists(ByVal s As sparsematrix, ByVal i As Integer, ByVal j As Integer) As Boolean
        Try
            sparseexists = alglib.sparseexists(s.csobj, i, j)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsegetdiagonal(ByVal s As sparsematrix, ByVal i As Integer) As Double
        Try
            sparsegetdiagonal = alglib.sparsegetdiagonal(s.csobj, i)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsemv(ByVal s As sparsematrix, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.sparsemv(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemtv(ByVal s As sparsematrix, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.sparsemtv(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsegemv(ByVal s As sparsematrix, ByVal alpha As Double, ByVal ops As Integer, ByVal x() As Double, ByVal ix As Integer, ByVal beta As Double, ByRef y() As Double, ByVal iy As Integer)
        Try
            alglib.sparsegemv(s.csobj, alpha, ops, x, ix, beta, y, iy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemv2(ByVal s As sparsematrix, ByVal x() As Double, ByRef y0() As Double, ByRef y1() As Double)
        Try
            alglib.sparsemv2(s.csobj, x, y0, y1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesmv(ByVal s As sparsematrix, ByVal isupper As Boolean, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.sparsesmv(s.csobj, isupper, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemultiplycolsby(ByRef s As sparsematrix, ByVal x() As Double)
        Try
            alglib.sparsemultiplycolsby(s.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemultiplyrowsby(ByRef s As sparsematrix, ByVal x() As Double)
        Try
            alglib.sparsemultiplyrowsby(s.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparsevsmv(ByVal s As sparsematrix, ByVal isupper As Boolean, ByVal x() As Double) As Double
        Try
            sparsevsmv = alglib.sparsevsmv(s.csobj, isupper, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsemm(ByVal s As sparsematrix, ByVal a(,) As Double, ByVal k As Integer, ByRef b(,) As Double)
        Try
            alglib.sparsemm(s.csobj, a, k, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemtm(ByVal s As sparsematrix, ByVal a(,) As Double, ByVal k As Integer, ByRef b(,) As Double)
        Try
            alglib.sparsemtm(s.csobj, a, k, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsemm2(ByVal s As sparsematrix, ByVal a(,) As Double, ByVal k As Integer, ByRef b0(,) As Double, ByRef b1(,) As Double)
        Try
            alglib.sparsemm2(s.csobj, a, k, b0, b1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesmm(ByVal s As sparsematrix, ByVal isupper As Boolean, ByVal a(,) As Double, ByVal k As Integer, ByRef b(,) As Double)
        Try
            alglib.sparsesmm(s.csobj, isupper, a, k, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsetrmv(ByVal s As sparsematrix, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x() As Double, ByRef y() As Double)
        Try
            alglib.sparsetrmv(s.csobj, isupper, isunit, optype, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsetrsv(ByVal s As sparsematrix, ByVal isupper As Boolean, ByVal isunit As Boolean, ByVal optype As Integer, ByRef x() As Double)
        Try
            alglib.sparsetrsv(s.csobj, isupper, isunit, optype, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesymmpermtbl(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal p() As Integer, ByRef b As sparsematrix)
        Try
            b = New sparsematrix()
            alglib.sparsesymmpermtbl(a.csobj, isupper, p, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesymmpermtbltranspose(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal p() As Integer, ByRef b As sparsematrix)
        Try
            b = New sparsematrix()
            alglib.sparsesymmpermtbltranspose(a.csobj, isupper, p, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesymmpermtblbuf(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal p() As Integer, ByRef b As sparsematrix)
        Try
            alglib.sparsesymmpermtblbuf(a.csobj, isupper, p, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesymmpermtbltransposebuf(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal p() As Integer, ByRef b As sparsematrix)
        Try
            alglib.sparsesymmpermtbltransposebuf(a.csobj, isupper, p, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseresizematrix(ByRef s As sparsematrix)
        Try
            alglib.sparseresizematrix(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparseenumerate(ByVal s As sparsematrix, ByRef t0 As Integer, ByRef t1 As Integer, ByRef i As Integer, ByRef j As Integer, ByRef v As Double) As Boolean
        Try
            sparseenumerate = alglib.sparseenumerate(s.csobj, t0, t1, i, j, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparserewriteexisting(ByRef s As sparsematrix, ByVal i As Integer, ByVal j As Integer, ByVal v As Double) As Boolean
        Try
            sparserewriteexisting = alglib.sparserewriteexisting(s.csobj, i, j, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsegetrow(ByVal s As sparsematrix, ByVal i As Integer, ByRef irow() As Double)
        Try
            alglib.sparsegetrow(s.csobj, i, irow)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsegetcompressedrow(ByVal s As sparsematrix, ByVal i As Integer, ByRef colidx() As Integer, ByRef vals() As Double, ByRef nzcnt As Integer)
        Try
            alglib.sparsegetcompressedrow(s.csobj, i, colidx, vals, nzcnt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseappendcompressedrow(ByRef s As sparsematrix, ByVal colidx() As Integer, ByVal vals() As Double, ByVal nz As Integer)
        Try
            alglib.sparseappendcompressedrow(s.csobj, colidx, vals, nz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseappendemptyrow(ByRef s As sparsematrix)
        Try
            alglib.sparseappendemptyrow(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseappendelement(ByRef s As sparsematrix, ByVal k As Integer, ByVal v As Double)
        Try
            alglib.sparseappendelement(s.csobj, k, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseappendmatrix(ByRef sdst As sparsematrix, ByVal ssrc As sparsematrix)
        Try
            alglib.sparseappendmatrix(sdst.csobj, ssrc.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsetransposesks(ByRef s As sparsematrix)
        Try
            alglib.sparsetransposesks(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsetransposecrs(ByRef s As sparsematrix)
        Try
            alglib.sparsetransposecrs(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytransposecrs(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            s1 = New sparsematrix()
            alglib.sparsecopytransposecrs(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytransposecrsbuf(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopytransposecrsbuf(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseconvertto(ByRef s0 As sparsematrix, ByVal fmt As Integer)
        Try
            alglib.sparseconvertto(s0.csobj, fmt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytobuf(ByVal s0 As sparsematrix, ByVal fmt As Integer, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopytobuf(s0.csobj, fmt, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseconverttohash(ByRef s As sparsematrix)
        Try
            alglib.sparseconverttohash(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytohash(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            s1 = New sparsematrix()
            alglib.sparsecopytohash(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytohashbuf(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopytohashbuf(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseconverttocrs(ByRef s As sparsematrix)
        Try
            alglib.sparseconverttocrs(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytocrs(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            s1 = New sparsematrix()
            alglib.sparsecopytocrs(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytocrsbuf(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopytocrsbuf(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparseconverttosks(ByRef s As sparsematrix)
        Try
            alglib.sparseconverttosks(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytosks(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            s1 = New sparsematrix()
            alglib.sparsecopytosks(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsecopytosksbuf(ByVal s0 As sparsematrix, ByRef s1 As sparsematrix)
        Try
            alglib.sparsecopytosksbuf(s0.csobj, s1.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparsegetmatrixtype(ByVal s As sparsematrix) As Integer
        Try
            sparsegetmatrixtype = alglib.sparsegetmatrixtype(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparseishash(ByVal s As sparsematrix) As Boolean
        Try
            sparseishash = alglib.sparseishash(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparseiscrs(ByVal s As sparsematrix) As Boolean
        Try
            sparseiscrs = alglib.sparseiscrs(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparseissks(ByVal s As sparsematrix) As Boolean
        Try
            sparseissks = alglib.sparseissks(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsefree(ByRef s As sparsematrix)
        Try
            s = New sparsematrix()
            alglib.sparsefree(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparsegetnrows(ByVal s As sparsematrix) As Integer
        Try
            sparsegetnrows = alglib.sparsegetnrows(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsegetncols(ByVal s As sparsematrix) As Integer
        Try
            sparsegetncols = alglib.sparsegetncols(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsegetuppercount(ByVal s As sparsematrix) As Integer
        Try
            sparsegetuppercount = alglib.sparsegetuppercount(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsegetlowercount(ByVal s As sparsematrix) As Integer
        Try
            sparsegetlowercount = alglib.sparsegetlowercount(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsescale(ByRef s As sparsematrix, ByVal scltype As Integer, ByVal scalerows As Boolean, ByVal scalecols As Boolean, ByVal colsfirst As Boolean, ByRef r() As Double, ByRef c() As Double)
        Try
            alglib.sparsescale(s.csobj, scltype, scalerows, scalecols, colsfirst, r, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class eigsubspacestate
        Public csobj As alglib.eigsubspacestate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This object stores state of the subspace iteration algorithm.
    '
    'You should use ALGLIB functions to work with this object.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class eigsubspacereport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public csobj As alglib.eigsubspacereport
    End Class


    Public Sub eigsubspacecreate(ByVal n As Integer, ByVal k As Integer, ByRef state As eigsubspacestate)
        Try
            state = New eigsubspacestate()
            alglib.eigsubspacecreate(n, k, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspacecreatebuf(ByVal n As Integer, ByVal k As Integer, ByRef state As eigsubspacestate)
        Try
            alglib.eigsubspacecreatebuf(n, k, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspacesetcond(ByRef state As eigsubspacestate, ByVal eps As Double, ByVal maxits As Integer)
        Try
            alglib.eigsubspacesetcond(state.csobj, eps, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspacesetwarmstart(ByRef state As eigsubspacestate, ByVal usewarmstart As Boolean)
        Try
            alglib.eigsubspacesetwarmstart(state.csobj, usewarmstart)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspaceoocstart(ByRef state As eigsubspacestate, ByVal mtype As Integer)
        Try
            alglib.eigsubspaceoocstart(state.csobj, mtype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function eigsubspaceooccontinue(ByRef state As eigsubspacestate) As Boolean
        Try
            eigsubspaceooccontinue = alglib.eigsubspaceooccontinue(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub eigsubspaceoocgetrequestinfo(ByRef state As eigsubspacestate, ByRef requesttype As Integer, ByRef requestsize As Integer)
        Try
            alglib.eigsubspaceoocgetrequestinfo(state.csobj, requesttype, requestsize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspaceoocgetrequestdata(ByRef state As eigsubspacestate, ByRef x(,) As Double)
        Try
            alglib.eigsubspaceoocgetrequestdata(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspaceoocsendresult(ByRef state As eigsubspacestate, ByVal ax(,) As Double)
        Try
            alglib.eigsubspaceoocsendresult(state.csobj, ax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspaceoocstop(ByRef state As eigsubspacestate, ByRef w() As Double, ByRef z(,) As Double, ByRef rep As eigsubspacereport)
        Try
            rep = New eigsubspacereport()
            alglib.eigsubspaceoocstop(state.csobj, w, z, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspacesolvedenses(ByRef state As eigsubspacestate, ByVal a(,) As Double, ByVal isupper As Boolean, ByRef w() As Double, ByRef z(,) As Double, ByRef rep As eigsubspacereport)
        Try
            rep = New eigsubspacereport()
            alglib.eigsubspacesolvedenses(state.csobj, a, isupper, w, z, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub eigsubspacesolvesparses(ByRef state As eigsubspacestate, ByVal a As sparsematrix, ByVal isupper As Boolean, ByRef w() As Double, ByRef z(,) As Double, ByRef rep As eigsubspacereport)
        Try
            rep = New eigsubspacereport()
            alglib.eigsubspacesolvesparses(state.csobj, a.csobj, isupper, w, z, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function smatrixevd(ByVal a(,) As Double, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByRef d() As Double, ByRef z(,) As Double) As Boolean
        Try
            smatrixevd = alglib.smatrixevd(a, n, zneeded, isupper, d, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixevdr(ByVal a(,) As Double, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByVal b1 As Double, ByVal b2 As Double, ByRef m As Integer, ByRef w() As Double, ByRef z(,) As Double) As Boolean
        Try
            smatrixevdr = alglib.smatrixevdr(a, n, zneeded, isupper, b1, b2, m, w, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixevdi(ByVal a(,) As Double, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByVal i1 As Integer, ByVal i2 As Integer, ByRef w() As Double, ByRef z(,) As Double) As Boolean
        Try
            smatrixevdi = alglib.smatrixevdi(a, n, zneeded, isupper, i1, i2, w, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hmatrixevd(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByRef d() As Double, ByRef z(,) As alglib.complex) As Boolean
        Try
            hmatrixevd = alglib.hmatrixevd(a, n, zneeded, isupper, d, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hmatrixevdr(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByVal b1 As Double, ByVal b2 As Double, ByRef m As Integer, ByRef w() As Double, ByRef z(,) As alglib.complex) As Boolean
        Try
            hmatrixevdr = alglib.hmatrixevdr(a, n, zneeded, isupper, b1, b2, m, w, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hmatrixevdi(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal zneeded As Integer, ByVal isupper As Boolean, ByVal i1 As Integer, ByVal i2 As Integer, ByRef w() As Double, ByRef z(,) As alglib.complex) As Boolean
        Try
            hmatrixevdi = alglib.hmatrixevdi(a, n, zneeded, isupper, i1, i2, w, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixtdevd(ByRef d() As Double, ByVal e() As Double, ByVal n As Integer, ByVal zneeded As Integer, ByRef z(,) As Double) As Boolean
        Try
            smatrixtdevd = alglib.smatrixtdevd(d, e, n, zneeded, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixtdevdr(ByRef d() As Double, ByVal e() As Double, ByVal n As Integer, ByVal zneeded As Integer, ByVal a As Double, ByVal b As Double, ByRef m As Integer, ByRef z(,) As Double) As Boolean
        Try
            smatrixtdevdr = alglib.smatrixtdevdr(d, e, n, zneeded, a, b, m, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixtdevdi(ByRef d() As Double, ByVal e() As Double, ByVal n As Integer, ByVal zneeded As Integer, ByVal i1 As Integer, ByVal i2 As Integer, ByRef z(,) As Double) As Boolean
        Try
            smatrixtdevdi = alglib.smatrixtdevdi(d, e, n, zneeded, i1, i2, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixevd(ByVal a(,) As Double, ByVal n As Integer, ByVal vneeded As Integer, ByRef wr() As Double, ByRef wi() As Double, ByRef vl(,) As Double, ByRef vr(,) As Double) As Boolean
        Try
            rmatrixevd = alglib.rmatrixevd(a, n, vneeded, wr, wi, vl, vr)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function













    Public Class sparsedecompositionanalysis
        Public csobj As alglib.sparsedecompositionanalysis
    End Class


    Public Sub rmatrixlu(ByRef a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef pivots() As Integer)
        Try
            alglib.rmatrixlu(a, m, n, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlu(ByRef a(,) As Double, ByRef pivots() As Integer)
        Try
            alglib.rmatrixlu(a, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlu(ByRef a(,) As alglib.complex, ByVal m As Integer, ByVal n As Integer, ByRef pivots() As Integer)
        Try
            alglib.cmatrixlu(a, m, n, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlu(ByRef a(,) As alglib.complex, ByRef pivots() As Integer)
        Try
            alglib.cmatrixlu(a, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hpdmatrixcholesky(ByRef a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean) As Boolean
        Try
            hpdmatrixcholesky = alglib.hpdmatrixcholesky(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixcholesky(ByRef a(,) As alglib.complex, ByVal isupper As Boolean) As Boolean
        Try
            hpdmatrixcholesky = alglib.hpdmatrixcholesky(a, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholesky(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean) As Boolean
        Try
            spdmatrixcholesky = alglib.spdmatrixcholesky(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholesky(ByRef a(,) As Double, ByVal isupper As Boolean) As Boolean
        Try
            spdmatrixcholesky = alglib.spdmatrixcholesky(a, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spdmatrixcholeskyupdateadd1(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal u() As Double)
        Try
            alglib.spdmatrixcholeskyupdateadd1(a, n, isupper, u)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyupdateadd1(ByRef a(,) As Double, ByVal isupper As Boolean, ByVal u() As Double)
        Try
            alglib.spdmatrixcholeskyupdateadd1(a, isupper, u)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyupdatefix(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal fix() As Boolean)
        Try
            alglib.spdmatrixcholeskyupdatefix(a, n, isupper, fix)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyupdatefix(ByRef a(,) As Double, ByVal isupper As Boolean, ByVal fix() As Boolean)
        Try
            alglib.spdmatrixcholeskyupdatefix(a, isupper, fix)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyupdateadd1buf(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal u() As Double, ByRef bufr() As Double)
        Try
            alglib.spdmatrixcholeskyupdateadd1buf(a, n, isupper, u, bufr)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyupdatefixbuf(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal fix() As Boolean, ByRef bufr() As Double)
        Try
            alglib.spdmatrixcholeskyupdatefixbuf(a, n, isupper, fix, bufr)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparselu(ByRef a As sparsematrix, ByVal pivottype As Integer, ByRef p() As Integer, ByRef q() As Integer) As Boolean
        Try
            sparselu = alglib.sparselu(a.csobj, pivottype, p, q)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsecholeskyskyline(ByRef a As sparsematrix, ByVal n As Integer, ByVal isupper As Boolean) As Boolean
        Try
            sparsecholeskyskyline = alglib.sparsecholeskyskyline(a.csobj, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsecholesky(ByRef a As sparsematrix, ByVal isupper As Boolean) As Boolean
        Try
            sparsecholesky = alglib.sparsecholesky(a.csobj, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsecholeskyp(ByRef a As sparsematrix, ByVal isupper As Boolean, ByRef p() As Integer) As Boolean
        Try
            sparsecholeskyp = alglib.sparsecholeskyp(a.csobj, isupper, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsecholeskyanalyze(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal facttype As Integer, ByVal permtype As Integer, ByRef analysis As sparsedecompositionanalysis) As Boolean
        Try
            analysis = New sparsedecompositionanalysis()
            sparsecholeskyanalyze = alglib.sparsecholeskyanalyze(a.csobj, isupper, facttype, permtype, analysis.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sparsecholeskyfactorize(ByRef analysis As sparsedecompositionanalysis, ByVal needupper As Boolean, ByRef a As sparsematrix, ByRef d() As Double, ByRef p() As Integer) As Boolean
        Try
            a = New sparsematrix()
            sparsecholeskyfactorize = alglib.sparsecholeskyfactorize(analysis.csobj, needupper, a.csobj, d, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsecholeskyreload(ByRef analysis As sparsedecompositionanalysis, ByVal a As sparsematrix, ByVal isupper As Boolean)
        Try
            alglib.sparsecholeskyreload(analysis.csobj, a.csobj, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class polynomialsolverreport
        Public Property maxerr() As Double
        Get
            Return csobj.maxerr
        End Get
        Set(ByVal Value As Double)
            csobj.maxerr = Value
        End Set
        End Property
        Public csobj As alglib.polynomialsolverreport
    End Class


    Public Sub polynomialsolve(ByVal a() As Double, ByVal n As Integer, ByRef x() As alglib.complex, ByRef rep As polynomialsolverreport)
        Try
            rep = New polynomialsolverreport()
            alglib.polynomialsolve(a, n, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function rmatrixbdsvd(ByRef d() As Double, ByVal e() As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal isfractionalaccuracyrequired As Boolean, ByRef u(,) As Double, ByVal nru As Integer, ByRef c(,) As Double, ByVal ncc As Integer, ByRef vt(,) As Double, ByVal ncvt As Integer) As Boolean
        Try
            rmatrixbdsvd = alglib.rmatrixbdsvd(d, e, n, isupper, isfractionalaccuracyrequired, u, nru, c, ncc, vt, ncvt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function rmatrixsvd(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer, ByVal uneeded As Integer, ByVal vtneeded As Integer, ByVal additionalmemory As Integer, ByRef w() As Double, ByRef u(,) As Double, ByRef vt(,) As Double) As Boolean
        Try
            rmatrixsvd = alglib.rmatrixsvd(a, m, n, uneeded, vtneeded, additionalmemory, w, u, vt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function rmatrixrcond1(ByVal a(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixrcond1 = alglib.rmatrixrcond1(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixrcond2(ByVal a(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixrcond2 = alglib.rmatrixrcond2(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixrcond2rect(ByVal a(,) As Double, ByVal m As Integer, ByVal n As Integer) As Double
        Try
            rmatrixrcond2rect = alglib.rmatrixrcond2rect(a, m, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixrcondinf(ByVal a(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixrcondinf = alglib.rmatrixrcondinf(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixrcond(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            spdmatrixrcond = alglib.spdmatrixrcond(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixrcond2(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            spdmatrixrcond2 = alglib.spdmatrixrcond2(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixtrrcond1(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean) As Double
        Try
            rmatrixtrrcond1 = alglib.rmatrixtrrcond1(a, n, isupper, isunit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixtrrcond2(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean) As Double
        Try
            rmatrixtrrcond2 = alglib.rmatrixtrrcond2(a, n, isupper, isunit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixtrrcondinf(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean) As Double
        Try
            rmatrixtrrcondinf = alglib.rmatrixtrrcondinf(a, n, isupper, isunit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixrcond(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            hpdmatrixrcond = alglib.hpdmatrixrcond(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixrcond1(ByVal a(,) As alglib.complex, ByVal n As Integer) As Double
        Try
            cmatrixrcond1 = alglib.cmatrixrcond1(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixrcondinf(ByVal a(,) As alglib.complex, ByVal n As Integer) As Double
        Try
            cmatrixrcondinf = alglib.cmatrixrcondinf(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixlurcond1(ByVal lua(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixlurcond1 = alglib.rmatrixlurcond1(lua, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixlurcondinf(ByVal lua(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixlurcondinf = alglib.rmatrixlurcondinf(lua, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholeskyrcond(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            spdmatrixcholeskyrcond = alglib.spdmatrixcholeskyrcond(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixcholeskyrcond(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            hpdmatrixcholeskyrcond = alglib.hpdmatrixcholeskyrcond(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixlurcond1(ByVal lua(,) As alglib.complex, ByVal n As Integer) As Double
        Try
            cmatrixlurcond1 = alglib.cmatrixlurcond1(lua, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixlurcondinf(ByVal lua(,) As alglib.complex, ByVal n As Integer) As Double
        Try
            cmatrixlurcondinf = alglib.cmatrixlurcondinf(lua, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixtrrcond1(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean) As Double
        Try
            cmatrixtrrcond1 = alglib.cmatrixtrrcond1(a, n, isupper, isunit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixtrrcondinf(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean) As Double
        Try
            cmatrixtrrcondinf = alglib.cmatrixtrrcondinf(a, n, isupper, isunit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class densesolverreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property r1() As Double
        Get
            Return csobj.r1
        End Get
        Set(ByVal Value As Double)
            csobj.r1 = Value
        End Set
        End Property
        Public Property rinf() As Double
        Get
            Return csobj.rinf
        End Get
        Set(ByVal Value As Double)
            csobj.rinf = Value
        End Set
        End Property
        Public csobj As alglib.densesolverreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class densesolverlsreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public Property cx() As Double(,)
        Get
            Return csobj.cx
        End Get
        Set(ByVal Value As Double(,))
            csobj.cx = Value
        End Set
        End Property
        Public Property n() As Integer
        Get
            Return csobj.n
        End Get
        Set(ByVal Value As Integer)
            csobj.n = Value
        End Set
        End Property
        Public Property k() As Integer
        Get
            Return csobj.k
        End Get
        Set(ByVal Value As Integer)
            csobj.k = Value
        End Set
        End Property
        Public csobj As alglib.densesolverlsreport
    End Class


    Public Sub rmatrixsolve(ByVal a(,) As Double, ByVal n As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixsolve(a, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixsolve(ByVal a(,) As Double, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixsolve(a, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rmatrixsolvefast(ByVal a(,) As Double, ByVal n As Integer, ByRef b() As Double) As Boolean
        Try
            rmatrixsolvefast = alglib.rmatrixsolvefast(a, n, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixsolvefast(ByVal a(,) As Double, ByRef b() As Double) As Boolean
        Try
            rmatrixsolvefast = alglib.rmatrixsolvefast(a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixsolvem(ByVal a(,) As Double, ByVal n As Integer, ByVal b(,) As Double, ByVal m As Integer, ByVal rfs As Boolean, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixsolvem(a, n, b, m, rfs, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixsolvem(ByVal a(,) As Double, ByVal b(,) As Double, ByVal rfs As Boolean, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixsolvem(a, b, rfs, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rmatrixsolvemfast(ByVal a(,) As Double, ByVal n As Integer, ByRef b(,) As Double, ByVal m As Integer) As Boolean
        Try
            rmatrixsolvemfast = alglib.rmatrixsolvemfast(a, n, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixsolvemfast(ByVal a(,) As Double, ByRef b(,) As Double) As Boolean
        Try
            rmatrixsolvemfast = alglib.rmatrixsolvemfast(a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixlusolve(ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixlusolve(lua, p, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlusolve(ByVal lua(,) As Double, ByVal p() As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixlusolve(lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rmatrixlusolvefast(ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByRef b() As Double) As Boolean
        Try
            rmatrixlusolvefast = alglib.rmatrixlusolvefast(lua, p, n, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixlusolvefast(ByVal lua(,) As Double, ByVal p() As Integer, ByRef b() As Double) As Boolean
        Try
            rmatrixlusolvefast = alglib.rmatrixlusolvefast(lua, p, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixlusolvem(ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByVal b(,) As Double, ByVal m As Integer, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixlusolvem(lua, p, n, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixlusolvem(ByVal lua(,) As Double, ByVal p() As Integer, ByVal b(,) As Double, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixlusolvem(lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rmatrixlusolvemfast(ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByRef b(,) As Double, ByVal m As Integer) As Boolean
        Try
            rmatrixlusolvemfast = alglib.rmatrixlusolvemfast(lua, p, n, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixlusolvemfast(ByVal lua(,) As Double, ByVal p() As Integer, ByRef b(,) As Double) As Boolean
        Try
            rmatrixlusolvemfast = alglib.rmatrixlusolvemfast(lua, p, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixmixedsolve(ByVal a(,) As Double, ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixmixedsolve(a, lua, p, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixmixedsolve(ByVal a(,) As Double, ByVal lua(,) As Double, ByVal p() As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixmixedsolve(a, lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixmixedsolvem(ByVal a(,) As Double, ByVal lua(,) As Double, ByVal p() As Integer, ByVal n As Integer, ByVal b(,) As Double, ByVal m As Integer, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixmixedsolvem(a, lua, p, n, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixmixedsolvem(ByVal a(,) As Double, ByVal lua(,) As Double, ByVal p() As Integer, ByVal b(,) As Double, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.rmatrixmixedsolvem(a, lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixsolvem(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal b(,) As alglib.complex, ByVal m As Integer, ByVal rfs As Boolean, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixsolvem(a, n, b, m, rfs, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixsolvem(ByVal a(,) As alglib.complex, ByVal b(,) As alglib.complex, ByVal rfs As Boolean, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixsolvem(a, b, rfs, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function cmatrixsolvemfast(ByVal a(,) As alglib.complex, ByVal n As Integer, ByRef b(,) As alglib.complex, ByVal m As Integer) As Boolean
        Try
            cmatrixsolvemfast = alglib.cmatrixsolvemfast(a, n, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixsolvemfast(ByVal a(,) As alglib.complex, ByRef b(,) As alglib.complex) As Boolean
        Try
            cmatrixsolvemfast = alglib.cmatrixsolvemfast(a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub cmatrixsolve(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixsolve(a, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixsolve(ByVal a(,) As alglib.complex, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixsolve(a, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function cmatrixsolvefast(ByVal a(,) As alglib.complex, ByVal n As Integer, ByRef b() As alglib.complex) As Boolean
        Try
            cmatrixsolvefast = alglib.cmatrixsolvefast(a, n, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixsolvefast(ByVal a(,) As alglib.complex, ByRef b() As alglib.complex) As Boolean
        Try
            cmatrixsolvefast = alglib.cmatrixsolvefast(a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub cmatrixlusolvem(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByVal b(,) As alglib.complex, ByVal m As Integer, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixlusolvem(lua, p, n, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlusolvem(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal b(,) As alglib.complex, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixlusolvem(lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function cmatrixlusolvemfast(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByRef b(,) As alglib.complex, ByVal m As Integer) As Boolean
        Try
            cmatrixlusolvemfast = alglib.cmatrixlusolvemfast(lua, p, n, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixlusolvemfast(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByRef b(,) As alglib.complex) As Boolean
        Try
            cmatrixlusolvemfast = alglib.cmatrixlusolvemfast(lua, p, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub cmatrixlusolve(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixlusolve(lua, p, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixlusolve(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixlusolve(lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function cmatrixlusolvefast(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByRef b() As alglib.complex) As Boolean
        Try
            cmatrixlusolvefast = alglib.cmatrixlusolvefast(lua, p, n, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixlusolvefast(ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByRef b() As alglib.complex) As Boolean
        Try
            cmatrixlusolvefast = alglib.cmatrixlusolvefast(lua, p, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub cmatrixmixedsolvem(ByVal a(,) As alglib.complex, ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByVal b(,) As alglib.complex, ByVal m As Integer, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixmixedsolvem(a, lua, p, n, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixmixedsolvem(ByVal a(,) As alglib.complex, ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal b(,) As alglib.complex, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixmixedsolvem(a, lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixmixedsolve(ByVal a(,) As alglib.complex, ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal n As Integer, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixmixedsolve(a, lua, p, n, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixmixedsolve(ByVal a(,) As alglib.complex, ByVal lua(,) As alglib.complex, ByVal p() As Integer, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.cmatrixmixedsolve(a, lua, p, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixsolvem(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal b(,) As Double, ByVal m As Integer, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixsolvem(a, n, isupper, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixsolvem(ByVal a(,) As Double, ByVal isupper As Boolean, ByVal b(,) As Double, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixsolvem(a, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spdmatrixsolvemfast(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef b(,) As Double, ByVal m As Integer) As Boolean
        Try
            spdmatrixsolvemfast = alglib.spdmatrixsolvemfast(a, n, isupper, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixsolvemfast(ByVal a(,) As Double, ByVal isupper As Boolean, ByRef b(,) As Double) As Boolean
        Try
            spdmatrixsolvemfast = alglib.spdmatrixsolvemfast(a, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spdmatrixsolve(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixsolve(a, n, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixsolve(ByVal a(,) As Double, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixsolve(a, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spdmatrixsolvefast(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef b() As Double) As Boolean
        Try
            spdmatrixsolvefast = alglib.spdmatrixsolvefast(a, n, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixsolvefast(ByVal a(,) As Double, ByVal isupper As Boolean, ByRef b() As Double) As Boolean
        Try
            spdmatrixsolvefast = alglib.spdmatrixsolvefast(a, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spdmatrixcholeskysolvem(ByVal cha(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal b(,) As Double, ByVal m As Integer, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixcholeskysolvem(cha, n, isupper, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskysolvem(ByVal cha(,) As Double, ByVal isupper As Boolean, ByVal b(,) As Double, ByRef x(,) As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixcholeskysolvem(cha, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spdmatrixcholeskysolvemfast(ByVal cha(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef b(,) As Double, ByVal m As Integer) As Boolean
        Try
            spdmatrixcholeskysolvemfast = alglib.spdmatrixcholeskysolvemfast(cha, n, isupper, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholeskysolvemfast(ByVal cha(,) As Double, ByVal isupper As Boolean, ByRef b(,) As Double) As Boolean
        Try
            spdmatrixcholeskysolvemfast = alglib.spdmatrixcholeskysolvemfast(cha, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spdmatrixcholeskysolve(ByVal cha(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixcholeskysolve(cha, n, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskysolve(ByVal cha(,) As Double, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.spdmatrixcholeskysolve(cha, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spdmatrixcholeskysolvefast(ByVal cha(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef b() As Double) As Boolean
        Try
            spdmatrixcholeskysolvefast = alglib.spdmatrixcholeskysolvefast(cha, n, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholeskysolvefast(ByVal cha(,) As Double, ByVal isupper As Boolean, ByRef b() As Double) As Boolean
        Try
            spdmatrixcholeskysolvefast = alglib.spdmatrixcholeskysolvefast(cha, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hpdmatrixsolvem(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal b(,) As alglib.complex, ByVal m As Integer, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixsolvem(a, n, isupper, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixsolvem(ByVal a(,) As alglib.complex, ByVal isupper As Boolean, ByVal b(,) As alglib.complex, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixsolvem(a, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hpdmatrixsolvemfast(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef b(,) As alglib.complex, ByVal m As Integer) As Boolean
        Try
            hpdmatrixsolvemfast = alglib.hpdmatrixsolvemfast(a, n, isupper, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixsolvemfast(ByVal a(,) As alglib.complex, ByVal isupper As Boolean, ByRef b(,) As alglib.complex) As Boolean
        Try
            hpdmatrixsolvemfast = alglib.hpdmatrixsolvemfast(a, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hpdmatrixsolve(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixsolve(a, n, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixsolve(ByVal a(,) As alglib.complex, ByVal isupper As Boolean, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixsolve(a, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hpdmatrixsolvefast(ByVal a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef b() As alglib.complex) As Boolean
        Try
            hpdmatrixsolvefast = alglib.hpdmatrixsolvefast(a, n, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixsolvefast(ByVal a(,) As alglib.complex, ByVal isupper As Boolean, ByRef b() As alglib.complex) As Boolean
        Try
            hpdmatrixsolvefast = alglib.hpdmatrixsolvefast(a, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hpdmatrixcholeskysolvem(ByVal cha(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal b(,) As alglib.complex, ByVal m As Integer, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixcholeskysolvem(cha, n, isupper, b, m, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixcholeskysolvem(ByVal cha(,) As alglib.complex, ByVal isupper As Boolean, ByVal b(,) As alglib.complex, ByRef x(,) As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixcholeskysolvem(cha, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hpdmatrixcholeskysolvemfast(ByVal cha(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef b(,) As alglib.complex, ByVal m As Integer) As Boolean
        Try
            hpdmatrixcholeskysolvemfast = alglib.hpdmatrixcholeskysolvemfast(cha, n, isupper, b, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixcholeskysolvemfast(ByVal cha(,) As alglib.complex, ByVal isupper As Boolean, ByRef b(,) As alglib.complex) As Boolean
        Try
            hpdmatrixcholeskysolvemfast = alglib.hpdmatrixcholeskysolvemfast(cha, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hpdmatrixcholeskysolve(ByVal cha(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixcholeskysolve(cha, n, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixcholeskysolve(ByVal cha(,) As alglib.complex, ByVal isupper As Boolean, ByVal b() As alglib.complex, ByRef x() As alglib.complex, ByRef rep As densesolverreport)
        Try
            rep = New densesolverreport()
            alglib.hpdmatrixcholeskysolve(cha, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function hpdmatrixcholeskysolvefast(ByVal cha(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef b() As alglib.complex) As Boolean
        Try
            hpdmatrixcholeskysolvefast = alglib.hpdmatrixcholeskysolvefast(cha, n, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hpdmatrixcholeskysolvefast(ByVal cha(,) As alglib.complex, ByVal isupper As Boolean, ByRef b() As alglib.complex) As Boolean
        Try
            hpdmatrixcholeskysolvefast = alglib.hpdmatrixcholeskysolvefast(cha, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rmatrixsolvels(ByVal a(,) As Double, ByVal nrows As Integer, ByVal ncols As Integer, ByVal b() As Double, ByVal threshold As Double, ByRef x() As Double, ByRef rep As densesolverlsreport)
        Try
            rep = New densesolverlsreport()
            alglib.rmatrixsolvels(a, nrows, ncols, b, threshold, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixsolvels(ByVal a(,) As Double, ByVal b() As Double, ByVal threshold As Double, ByRef x() As Double, ByRef rep As densesolverlsreport)
        Try
            rep = New densesolverlsreport()
            alglib.rmatrixsolvels(a, b, threshold, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure is a sparse solver report (both direct and iterative solvers
    'use this structure).
    '
    'Following fields can be accessed by users:
    '* TerminationType (specific error codes depend on the solver  being  used,
    '  with positive values ALWAYS signaling  that something useful is returned
    '  in X, and negative values ALWAYS meaning critical failures.
    '* NMV - number of matrix-vector products performed (0 for direct solvers)
    '* IterationsCount - inner iterations count (0 for direct solvers)
    '* R2 - squared residual
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class sparsesolverreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property nmv() As Integer
        Get
            Return csobj.nmv
        End Get
        Set(ByVal Value As Integer)
            csobj.nmv = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public csobj As alglib.sparsesolverreport
    End Class
    Public Class sparsesolverstate
        Public csobj As alglib.sparsesolverstate
    End Class


    Public Sub sparsesolvesymmetricgmres(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByVal k As Integer, ByVal epsf As Double, ByVal maxits As Integer, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolvesymmetricgmres(a.csobj, isupper, b, k, epsf, maxits, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolvegmres(ByVal a As sparsematrix, ByVal b() As Double, ByVal k As Integer, ByVal epsf As Double, ByVal maxits As Integer, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolvegmres(a.csobj, b, k, epsf, maxits, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolvercreate(ByVal n As Integer, ByRef state As sparsesolverstate)
        Try
            state = New sparsesolverstate()
            alglib.sparsesolvercreate(n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversetalgogmres(ByRef state As sparsesolverstate, ByVal k As Integer)
        Try
            alglib.sparsesolversetalgogmres(state.csobj, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversetstartingpoint(ByRef state As sparsesolverstate, ByVal x() As Double)
        Try
            alglib.sparsesolversetstartingpoint(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversetcond(ByRef state As sparsesolverstate, ByVal epsf As Double, ByVal maxits As Integer)
        Try
            alglib.sparsesolversetcond(state.csobj, epsf, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversolvesymmetric(ByRef state As sparsesolverstate, ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double)
        Try
            alglib.sparsesolversolvesymmetric(state.csobj, a.csobj, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversolve(ByRef state As sparsesolverstate, ByVal a As sparsematrix, ByVal b() As Double)
        Try
            alglib.sparsesolversolve(state.csobj, a.csobj, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolverresults(ByRef state As sparsesolverstate, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolverresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolversetxrep(ByRef state As sparsesolverstate, ByVal needxrep As Boolean)
        Try
            alglib.sparsesolversetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolveroocstart(ByRef state As sparsesolverstate, ByVal b() As Double)
        Try
            alglib.sparsesolveroocstart(state.csobj, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function sparsesolverooccontinue(ByRef state As sparsesolverstate) As Boolean
        Try
            sparsesolverooccontinue = alglib.sparsesolverooccontinue(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sparsesolveroocgetrequestinfo(ByRef state As sparsesolverstate, ByRef requesttype As Integer)
        Try
            alglib.sparsesolveroocgetrequestinfo(state.csobj, requesttype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolveroocgetrequestdata(ByRef state As sparsesolverstate, ByRef x() As Double)
        Try
            alglib.sparsesolveroocgetrequestdata(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolveroocgetrequestdata1(ByRef state As sparsesolverstate, ByRef v As Double)
        Try
            alglib.sparsesolveroocgetrequestdata1(state.csobj, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolveroocsendresult(ByRef state As sparsesolverstate, ByVal ax() As Double)
        Try
            alglib.sparsesolveroocsendresult(state.csobj, ax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolveroocstop(ByRef state As sparsesolverstate, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolveroocstop(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolverrequesttermination(ByRef state As sparsesolverstate)
        Try
            alglib.sparsesolverrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class lincgstate
        Public csobj As alglib.lincgstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class lincgreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nmv() As Integer
        Get
            Return csobj.nmv
        End Get
        Set(ByVal Value As Integer)
            csobj.nmv = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public csobj As alglib.lincgreport
    End Class


    Public Sub lincgcreate(ByVal n As Integer, ByRef state As lincgstate)
        Try
            state = New lincgstate()
            alglib.lincgcreate(n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetstartingpoint(ByRef state As lincgstate, ByVal x() As Double)
        Try
            alglib.lincgsetstartingpoint(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetprecunit(ByRef state As lincgstate)
        Try
            alglib.lincgsetprecunit(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetprecdiag(ByRef state As lincgstate)
        Try
            alglib.lincgsetprecdiag(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetcond(ByRef state As lincgstate, ByVal epsf As Double, ByVal maxits As Integer)
        Try
            alglib.lincgsetcond(state.csobj, epsf, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsolvesparse(ByRef state As lincgstate, ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double)
        Try
            alglib.lincgsolvesparse(state.csobj, a.csobj, isupper, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgresults(ByVal state As lincgstate, ByRef x() As Double, ByRef rep As lincgreport)
        Try
            rep = New lincgreport()
            alglib.lincgresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetrestartfreq(ByRef state As lincgstate, ByVal srf As Integer)
        Try
            alglib.lincgsetrestartfreq(state.csobj, srf)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetrupdatefreq(ByRef state As lincgstate, ByVal freq As Integer)
        Try
            alglib.lincgsetrupdatefreq(state.csobj, freq)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lincgsetxrep(ByRef state As lincgstate, ByVal needxrep As Boolean)
        Try
            alglib.lincgsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class normestimatorstate
        Public csobj As alglib.normestimatorstate
    End Class


    Public Sub normestimatorcreate(ByVal m As Integer, ByVal n As Integer, ByVal nstart As Integer, ByVal nits As Integer, ByRef state As normestimatorstate)
        Try
            state = New normestimatorstate()
            alglib.normestimatorcreate(m, n, nstart, nits, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub normestimatorsetseed(ByRef state As normestimatorstate, ByVal seedval As Integer)
        Try
            alglib.normestimatorsetseed(state.csobj, seedval)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub normestimatorestimatesparse(ByRef state As normestimatorstate, ByVal a As sparsematrix)
        Try
            alglib.normestimatorestimatesparse(state.csobj, a.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub normestimatorresults(ByVal state As normestimatorstate, ByRef nrm As Double)
        Try
            alglib.normestimatorresults(state.csobj, nrm)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class linlsqrstate
        Public csobj As alglib.linlsqrstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class linlsqrreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nmv() As Integer
        Get
            Return csobj.nmv
        End Get
        Set(ByVal Value As Integer)
            csobj.nmv = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.linlsqrreport
    End Class


    Public Sub linlsqrcreate(ByVal m As Integer, ByVal n As Integer, ByRef state As linlsqrstate)
        Try
            state = New linlsqrstate()
            alglib.linlsqrcreate(m, n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrcreatebuf(ByVal m As Integer, ByVal n As Integer, ByRef state As linlsqrstate)
        Try
            alglib.linlsqrcreatebuf(m, n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsetprecunit(ByRef state As linlsqrstate)
        Try
            alglib.linlsqrsetprecunit(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsetprecdiag(ByRef state As linlsqrstate)
        Try
            alglib.linlsqrsetprecdiag(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsetlambdai(ByRef state As linlsqrstate, ByVal lambdai As Double)
        Try
            alglib.linlsqrsetlambdai(state.csobj, lambdai)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsolvesparse(ByRef state As linlsqrstate, ByVal a As sparsematrix, ByVal b() As Double)
        Try
            alglib.linlsqrsolvesparse(state.csobj, a.csobj, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsetcond(ByRef state As linlsqrstate, ByVal epsa As Double, ByVal epsb As Double, ByVal maxits As Integer)
        Try
            alglib.linlsqrsetcond(state.csobj, epsa, epsb, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrresults(ByVal state As linlsqrstate, ByRef x() As Double, ByRef rep As linlsqrreport)
        Try
            rep = New linlsqrreport()
            alglib.linlsqrresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub linlsqrsetxrep(ByRef state As linlsqrstate, ByVal needxrep As Boolean)
        Try
            alglib.linlsqrsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function linlsqrpeekiterationscount(ByVal s As linlsqrstate) As Integer
        Try
            linlsqrpeekiterationscount = alglib.linlsqrpeekiterationscount(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub linlsqrrequesttermination(ByRef state As linlsqrstate)
        Try
            alglib.linlsqrrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub sparsespdsolvesks(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsespdsolvesks(a.csobj, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsespdsolve(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsespdsolve(a.csobj, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsespdcholeskysolve(ByVal a As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsespdcholeskysolve(a.csobj, isupper, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolve(ByVal a As sparsematrix, ByVal b() As Double, ByVal solvertype As Integer, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolve(a.csobj, b, solvertype, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolve(ByVal a As sparsematrix, ByVal b() As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolve(a.csobj, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolvelsreg(ByVal a As sparsematrix, ByVal b() As Double, ByVal reg As Double, ByVal solvertype As Integer, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolvelsreg(a.csobj, b, reg, solvertype, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparsesolvelsreg(ByVal a As sparsematrix, ByVal b() As Double, ByVal reg As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparsesolvelsreg(a.csobj, b, reg, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sparselusolve(ByVal a As sparsematrix, ByVal p() As Integer, ByVal q() As Integer, ByVal b() As Double, ByRef x() As Double, ByRef rep As sparsesolverreport)
        Try
            rep = New sparsesolverreport()
            alglib.sparselusolve(a.csobj, p, q, b, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class nleqstate
        Public csobj As alglib.nleqstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class nleqreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfunc() As Integer
        Get
            Return csobj.nfunc
        End Get
        Set(ByVal Value As Integer)
            csobj.nfunc = Value
        End Set
        End Property
        Public Property njac() As Integer
        Get
            Return csobj.njac
        End Get
        Set(ByVal Value As Integer)
            csobj.njac = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.nleqreport
    End Class


    Public Sub nleqcreatelm(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByRef state As nleqstate)
        Try
            state = New nleqstate()
            alglib.nleqcreatelm(n, m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqcreatelm(ByVal m As Integer, ByVal x() As Double, ByRef state As nleqstate)
        Try
            state = New nleqstate()
            alglib.nleqcreatelm(m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqsetcond(ByRef state As nleqstate, ByVal epsf As Double, ByVal maxits As Integer)
        Try
            alglib.nleqsetcond(state.csobj, epsf, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqsetxrep(ByRef state As nleqstate, ByVal needxrep As Boolean)
        Try
            alglib.nleqsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqsetstpmax(ByRef state As nleqstate, ByVal stpmax As Double)
        Try
            alglib.nleqsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function nleqiteration(ByRef state As nleqstate) As Boolean
        Try
            nleqiteration = alglib.nleqiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear solver
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    '   -- ALGLIB --
    '      Copyright 20.03.2009 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub nleqsolve(ByVal state As nleqstate, ByVal func As ndimensional_func, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.nleqsolve(state.csobj, New alglib.ndimensional_func(AddressOf func.Invoke), New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub nleqresults(ByVal state As nleqstate, ByRef x() As Double, ByRef rep As nleqreport)
        Try
            rep = New nleqreport()
            alglib.nleqresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqresultsbuf(ByVal state As nleqstate, ByRef x() As Double, ByRef rep As nleqreport)
        Try
            alglib.nleqresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nleqrestartfrom(ByRef state As nleqstate, ByVal x() As Double)
        Try
            alglib.nleqrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure is used to store  OptGuard  report,  i.e.  report  on   the
    'properties of the nonlinear function being optimized with ALGLIB.
    '
    'After you tell your optimizer to activate OptGuard  this technology starts
    'to silently monitor function values and gradients/Jacobians  being  passed
    'all around during your optimization session. Depending on specific set  of
    'checks enabled OptGuard may perform additional function evaluations  (say,
    'about 3*N evaluations if you want to check analytic gradient for errors).
    '
    'Upon discovering that something strange happens  (function  values  and/or
    'gradient components change too sharply and/or unexpectedly) OptGuard  sets
    'one of the "suspicion  flags" (without interrupting optimization session).
    'After optimization is done, you can examine OptGuard report.
    '
    'Following report fields can be set:
    '* nonc0suspected
    '* nonc1suspected
    '* badgradsuspected
    '
    '
    '=== WHAT CAN BE DETECTED WITH OptGuard INTEGRITY CHECKER =================
    '
    'Following  types  of  errors  in your target function (constraints) can be
    'caught:
    'a) discontinuous functions ("non-C0" part of the report)
    'b) functions with discontinuous derivative ("non-C1" part of the report)
    'c) errors in the analytic gradient provided by user
    '
    'These types of errors result in optimizer  stopping  well  before reaching
    'solution (most often - right after encountering discontinuity).
    '
    'Type A errors are usually  coding  errors  during  implementation  of  the
    'target function. Most "normal" problems involve continuous functions,  and
    'anyway you can't reliably optimize discontinuous function.
    '
    'Type B errors are either coding errors or (in case code itself is correct)
    'evidence of the fact  that  your  problem  is  an  "incorrect"  one.  Most
    'optimizers (except for ones provided by MINNS subpackage) do  not  support
    'nonsmooth problems.
    '
    'Type C errors are coding errors which often prevent optimizer from  making
    'even one step  or result in optimizing stopping  too  early,  as  soon  as
    'actual descent direction becomes too different from one suggested by user-
    'supplied gradient.
    '
    '
    '=== WHAT IS REPORTED =====================================================
    '
    'Following set of report fields deals with discontinuous  target functions,
    'ones not belonging to C0 continuity class:
    '
    '* nonc0suspected - is a flag which is set upon discovering some indication
    '  of the discontinuity. If this flag is false, the rest of "non-C0" fields
    '  should be ignored
    '* nonc0fidx - is an index of the function (0 for  target  function,  1  or
    '  higher for nonlinear constraints) which is suspected of being "non-C0"
    '* nonc0lipshitzc - a Lipchitz constant for a function which was  suspected
    '  of being non-continuous.
    '* nonc0test0positive -  set  to  indicate  specific  test  which  detected
    '  continuity violation (test #0)
    '
    'Following set of report fields deals with discontinuous gradient/Jacobian,
    'i.e. with functions violating C1 continuity:
    '
    '* nonc1suspected - is a flag which is set upon discovering some indication
    '  of the discontinuity. If this flag is false, the rest of "non-C1" fields
    '  should be ignored
    '* nonc1fidx - is an index of the function (0 for  target  function,  1  or
    '  higher for nonlinear constraints) which is suspected of being "non-C1"
    '* nonc1lipshitzc - a Lipchitz constant for a function gradient  which  was
    '  suspected of being non-smooth.
    '* nonc1test0positive -  set  to  indicate  specific  test  which  detected
    '  continuity violation (test #0)
    '* nonc1test1positive -  set  to  indicate  specific  test  which  detected
    '  continuity violation (test #1)
    '
    'Following set of report fields deals with errors in the gradient:
    '* badgradsuspected - is a flad which is set upon discovering an  error  in
    '  the analytic gradient supplied by user
    '* badgradfidx - index  of   the  function  with bad gradient (0 for target
    '  function, 1 or higher for nonlinear constraints)
    '* badgradvidx - index of the variable
    '* badgradxbase - location where Jacobian is tested
    '* following  matrices  store  user-supplied  Jacobian  and  its  numerical
    '  differentiation version (which is assumed to be  free  from  the  coding
    '  errors), both of them computed near the initial point:
    '  * badgraduser, an array[K,N], analytic Jacobian supplied by user
    '  * badgradnum,  an array[K,N], numeric  Jacobian computed by ALGLIB
    '  Here K is a total number of  nonlinear  functions  (target  +  nonlinear
    '  constraints), N is a variable number.
    '  The  element  of  badgraduser[] with index [badgradfidx,badgradvidx]  is
    '  assumed to be wrong.
    '
    'More detailed error log can  be  obtained  from  optimizer  by  explicitly
    'requesting reports for tests C0.0, C1.0, C1.1.
    '
    '  -- ALGLIB --
    '     Copyright 19.11.2018 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class optguardreport
        Public Property nonc0suspected() As Boolean
        Get
            Return csobj.nonc0suspected
        End Get
        Set(ByVal Value As Boolean)
            csobj.nonc0suspected = Value
        End Set
        End Property
        Public Property nonc0test0positive() As Boolean
        Get
            Return csobj.nonc0test0positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.nonc0test0positive = Value
        End Set
        End Property
        Public Property nonc0fidx() As Integer
        Get
            Return csobj.nonc0fidx
        End Get
        Set(ByVal Value As Integer)
            csobj.nonc0fidx = Value
        End Set
        End Property
        Public Property nonc0lipschitzc() As Double
        Get
            Return csobj.nonc0lipschitzc
        End Get
        Set(ByVal Value As Double)
            csobj.nonc0lipschitzc = Value
        End Set
        End Property
        Public Property nonc1suspected() As Boolean
        Get
            Return csobj.nonc1suspected
        End Get
        Set(ByVal Value As Boolean)
            csobj.nonc1suspected = Value
        End Set
        End Property
        Public Property nonc1test0positive() As Boolean
        Get
            Return csobj.nonc1test0positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.nonc1test0positive = Value
        End Set
        End Property
        Public Property nonc1test1positive() As Boolean
        Get
            Return csobj.nonc1test1positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.nonc1test1positive = Value
        End Set
        End Property
        Public Property nonc1fidx() As Integer
        Get
            Return csobj.nonc1fidx
        End Get
        Set(ByVal Value As Integer)
            csobj.nonc1fidx = Value
        End Set
        End Property
        Public Property nonc1lipschitzc() As Double
        Get
            Return csobj.nonc1lipschitzc
        End Get
        Set(ByVal Value As Double)
            csobj.nonc1lipschitzc = Value
        End Set
        End Property
        Public Property badgradsuspected() As Boolean
        Get
            Return csobj.badgradsuspected
        End Get
        Set(ByVal Value As Boolean)
            csobj.badgradsuspected = Value
        End Set
        End Property
        Public Property badgradfidx() As Integer
        Get
            Return csobj.badgradfidx
        End Get
        Set(ByVal Value As Integer)
            csobj.badgradfidx = Value
        End Set
        End Property
        Public Property badgradvidx() As Integer
        Get
            Return csobj.badgradvidx
        End Get
        Set(ByVal Value As Integer)
            csobj.badgradvidx = Value
        End Set
        End Property
        Public Property badgradxbase() As Double()
        Get
            Return csobj.badgradxbase
        End Get
        Set(ByVal Value As Double())
            csobj.badgradxbase = Value
        End Set
        End Property
        Public Property badgraduser() As Double(,)
        Get
            Return csobj.badgraduser
        End Get
        Set(ByVal Value As Double(,))
            csobj.badgraduser = Value
        End Set
        End Property
        Public Property badgradnum() As Double(,)
        Get
            Return csobj.badgradnum
        End Get
        Set(ByVal Value As Double(,))
            csobj.badgradnum = Value
        End Set
        End Property
        Public csobj As alglib.optguardreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This  structure  is  used  for  detailed   reporting  about  suspected  C0
    'continuity violation.
    '
    '=== WHAT IS TESTED =======================================================
    '
    'C0 test  studies  function  values (not gradient!)  obtained  during  line
    'searches and monitors estimate of the Lipschitz  constant.  Sudden  spikes
    'usually indicate that discontinuity was detected.
    '
    '
    '=== WHAT IS REPORTED =====================================================
    '
    'Actually, report retrieval function returns TWO report structures:
    '
    '* one for most suspicious point found so far (one with highest  change  in
    '  the function value), so called "strongest" report
    '* another one for most detailed line search (more function  evaluations  =
    '  easier to understand what's going on) which triggered  test #0 criteria,
    '  so called "longest" report
    '
    'In both cases following fields are returned:
    '
    '* positive - is TRUE  when test flagged suspicious point;  FALSE  if  test
    '  did not notice anything (in the latter cases fields below are empty).
    '* fidx - is an index of the function (0 for  target  function, 1 or higher
    '  for nonlinear constraints) which is suspected of being "non-C1"
    '* x0[], d[] - arrays of length N which store initial point  and  direction
    '  for line search (d[] can be normalized, but does not have to)
    '* stp[], f[] - arrays of length CNT which store step lengths and  function
    '  values at these points; f[i] is evaluated in x0+stp[i]*d.
    '* stpidxa, stpidxb - we  suspect  that  function  violates  C1  continuity
    '  between steps #stpidxa and #stpidxb (usually we have  stpidxb=stpidxa+3,
    '  with  most  likely  position  of  the  violation  between  stpidxa+1 and
    '  stpidxa+2.
    '* inneriter, outeriter - inner and outer iteration indexes (can be -1 if no
    '  iteration information was specified)
    '
    'You can plot function values stored in stp[]  and  f[]  arrays  and  study
    'behavior of your function by your own eyes, just  to  be  sure  that  test
    'correctly reported C1 violation.
    '
    '  -- ALGLIB --
    '     Copyright 19.11.2018 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class optguardnonc0report
        Public Property positive() As Boolean
        Get
            Return csobj.positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.positive = Value
        End Set
        End Property
        Public Property fidx() As Integer
        Get
            Return csobj.fidx
        End Get
        Set(ByVal Value As Integer)
            csobj.fidx = Value
        End Set
        End Property
        Public Property x0() As Double()
        Get
            Return csobj.x0
        End Get
        Set(ByVal Value As Double())
            csobj.x0 = Value
        End Set
        End Property
        Public Property d() As Double()
        Get
            Return csobj.d
        End Get
        Set(ByVal Value As Double())
            csobj.d = Value
        End Set
        End Property
        Public Property n() As Integer
        Get
            Return csobj.n
        End Get
        Set(ByVal Value As Integer)
            csobj.n = Value
        End Set
        End Property
        Public Property stp() As Double()
        Get
            Return csobj.stp
        End Get
        Set(ByVal Value As Double())
            csobj.stp = Value
        End Set
        End Property
        Public Property f() As Double()
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double())
            csobj.f = Value
        End Set
        End Property
        Public Property cnt() As Integer
        Get
            Return csobj.cnt
        End Get
        Set(ByVal Value As Integer)
            csobj.cnt = Value
        End Set
        End Property
        Public Property stpidxa() As Integer
        Get
            Return csobj.stpidxa
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxa = Value
        End Set
        End Property
        Public Property stpidxb() As Integer
        Get
            Return csobj.stpidxb
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxb = Value
        End Set
        End Property
        Public Property inneriter() As Integer
        Get
            Return csobj.inneriter
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriter = Value
        End Set
        End Property
        Public Property outeriter() As Integer
        Get
            Return csobj.outeriter
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriter = Value
        End Set
        End Property
        Public csobj As alglib.optguardnonc0report
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This  structure  is  used  for  detailed   reporting  about  suspected  C1
    'continuity violation as flagged by C1 test #0 (OptGuard  has several tests
    'for C1 continuity, this report is used by #0).
    '
    '=== WHAT IS TESTED =======================================================
    '
    'C1 test #0 studies function values (not gradient!)  obtained  during  line
    'searches and monitors behavior of directional  derivative  estimate.  This
    'test is less powerful than test #1, but it does  not  depend  on  gradient
    'values  and  thus  it  is  more  robust  against  artifacts  introduced by
    'numerical differentiation.
    '
    '
    '=== WHAT IS REPORTED =====================================================
    '
    'Actually, report retrieval function returns TWO report structures:
    '
    '* one for most suspicious point found so far (one with highest  change  in
    '  the directional derivative), so called "strongest" report
    '* another one for most detailed line search (more function  evaluations  =
    '  easier to understand what's going on) which triggered  test #0 criteria,
    '  so called "longest" report
    '
    'In both cases following fields are returned:
    '
    '* positive - is TRUE  when test flagged suspicious point;  FALSE  if  test
    '  did not notice anything (in the latter cases fields below are empty).
    '* fidx - is an index of the function (0 for  target  function, 1 or higher
    '  for nonlinear constraints) which is suspected of being "non-C1"
    '* x0[], d[] - arrays of length N which store initial point  and  direction
    '  for line search (d[] can be normalized, but does not have to)
    '* stp[], f[] - arrays of length CNT which store step lengths and  function
    '  values at these points; f[i] is evaluated in x0+stp[i]*d.
    '* stpidxa, stpidxb - we  suspect  that  function  violates  C1  continuity
    '  between steps #stpidxa and #stpidxb (usually we have  stpidxb=stpidxa+3,
    '  with  most  likely  position  of  the  violation  between  stpidxa+1 and
    '  stpidxa+2.
    '* inneriter, outeriter - inner and outer iteration indexes (can be -1 if no
    '  iteration information was specified)
    '
    'You can plot function values stored in stp[]  and  f[]  arrays  and  study
    'behavior of your function by your own eyes, just  to  be  sure  that  test
    'correctly reported C1 violation.
    '
    '  -- ALGLIB --
    '     Copyright 19.11.2018 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class optguardnonc1test0report
        Public Property positive() As Boolean
        Get
            Return csobj.positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.positive = Value
        End Set
        End Property
        Public Property fidx() As Integer
        Get
            Return csobj.fidx
        End Get
        Set(ByVal Value As Integer)
            csobj.fidx = Value
        End Set
        End Property
        Public Property x0() As Double()
        Get
            Return csobj.x0
        End Get
        Set(ByVal Value As Double())
            csobj.x0 = Value
        End Set
        End Property
        Public Property d() As Double()
        Get
            Return csobj.d
        End Get
        Set(ByVal Value As Double())
            csobj.d = Value
        End Set
        End Property
        Public Property n() As Integer
        Get
            Return csobj.n
        End Get
        Set(ByVal Value As Integer)
            csobj.n = Value
        End Set
        End Property
        Public Property stp() As Double()
        Get
            Return csobj.stp
        End Get
        Set(ByVal Value As Double())
            csobj.stp = Value
        End Set
        End Property
        Public Property f() As Double()
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double())
            csobj.f = Value
        End Set
        End Property
        Public Property cnt() As Integer
        Get
            Return csobj.cnt
        End Get
        Set(ByVal Value As Integer)
            csobj.cnt = Value
        End Set
        End Property
        Public Property stpidxa() As Integer
        Get
            Return csobj.stpidxa
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxa = Value
        End Set
        End Property
        Public Property stpidxb() As Integer
        Get
            Return csobj.stpidxb
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxb = Value
        End Set
        End Property
        Public Property inneriter() As Integer
        Get
            Return csobj.inneriter
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriter = Value
        End Set
        End Property
        Public Property outeriter() As Integer
        Get
            Return csobj.outeriter
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriter = Value
        End Set
        End Property
        Public csobj As alglib.optguardnonc1test0report
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This  structure  is  used  for  detailed   reporting  about  suspected  C1
    'continuity violation as flagged by C1 test #1 (OptGuard  has several tests
    'for C1 continuity, this report is used by #1).
    '
    '=== WHAT IS TESTED =======================================================
    '
    'C1 test #1 studies individual  components  of  the  gradient  as  recorded
    'during line searches. Upon discovering discontinuity in the gradient  this
    'test records specific component which was suspected (or  one  with  highest
    'indication of discontinuity if multiple components are suspected).
    '
    'When precise analytic gradient is provided this test is more powerful than
    'test #0  which  works  with  function  values  and  ignores  user-provided
    'gradient.  However,  test  #0  becomes  more   powerful   when   numerical
    'differentiation is employed (in such cases test #1 detects  higher  levels
    'of numerical noise and becomes too conservative).
    '
    'This test also tells specific components of the gradient which violate  C1
    'continuity, which makes it more informative than #0, which just tells that
    'continuity is violated.
    '
    '
    '=== WHAT IS REPORTED =====================================================
    '
    'Actually, report retrieval function returns TWO report structures:
    '
    '* one for most suspicious point found so far (one with highest  change  in
    '  the directional derivative), so called "strongest" report
    '* another one for most detailed line search (more function  evaluations  =
    '  easier to understand what's going on) which triggered  test #1 criteria,
    '  so called "longest" report
    '
    'In both cases following fields are returned:
    '
    '* positive - is TRUE  when test flagged suspicious point;  FALSE  if  test
    '  did not notice anything (in the latter cases fields below are empty).
    '* fidx - is an index of the function (0 for  target  function, 1 or higher
    '  for nonlinear constraints) which is suspected of being "non-C1"
    '* vidx - is an index of the variable in [0,N) with nonsmooth derivative
    '* x0[], d[] - arrays of length N which store initial point  and  direction
    '  for line search (d[] can be normalized, but does not have to)
    '* stp[], g[] - arrays of length CNT which store step lengths and  gradient
    '  values at these points; g[i] is evaluated in  x0+stp[i]*d  and  contains
    '  vidx-th component of the gradient.
    '* stpidxa, stpidxb - we  suspect  that  function  violates  C1  continuity
    '  between steps #stpidxa and #stpidxb (usually we have  stpidxb=stpidxa+3,
    '  with  most  likely  position  of  the  violation  between  stpidxa+1 and
    '  stpidxa+2.
    '* inneriter, outeriter - inner and outer iteration indexes (can be -1 if  no
    '  iteration information was specified)
    '
    'You can plot function values stored in stp[]  and  g[]  arrays  and  study
    'behavior of your function by your own eyes, just  to  be  sure  that  test
    'correctly reported C1 violation.
    '
    '  -- ALGLIB --
    '     Copyright 19.11.2018 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class optguardnonc1test1report
        Public Property positive() As Boolean
        Get
            Return csobj.positive
        End Get
        Set(ByVal Value As Boolean)
            csobj.positive = Value
        End Set
        End Property
        Public Property fidx() As Integer
        Get
            Return csobj.fidx
        End Get
        Set(ByVal Value As Integer)
            csobj.fidx = Value
        End Set
        End Property
        Public Property vidx() As Integer
        Get
            Return csobj.vidx
        End Get
        Set(ByVal Value As Integer)
            csobj.vidx = Value
        End Set
        End Property
        Public Property x0() As Double()
        Get
            Return csobj.x0
        End Get
        Set(ByVal Value As Double())
            csobj.x0 = Value
        End Set
        End Property
        Public Property d() As Double()
        Get
            Return csobj.d
        End Get
        Set(ByVal Value As Double())
            csobj.d = Value
        End Set
        End Property
        Public Property n() As Integer
        Get
            Return csobj.n
        End Get
        Set(ByVal Value As Integer)
            csobj.n = Value
        End Set
        End Property
        Public Property stp() As Double()
        Get
            Return csobj.stp
        End Get
        Set(ByVal Value As Double())
            csobj.stp = Value
        End Set
        End Property
        Public Property g() As Double()
        Get
            Return csobj.g
        End Get
        Set(ByVal Value As Double())
            csobj.g = Value
        End Set
        End Property
        Public Property cnt() As Integer
        Get
            Return csobj.cnt
        End Get
        Set(ByVal Value As Integer)
            csobj.cnt = Value
        End Set
        End Property
        Public Property stpidxa() As Integer
        Get
            Return csobj.stpidxa
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxa = Value
        End Set
        End Property
        Public Property stpidxb() As Integer
        Get
            Return csobj.stpidxb
        End Get
        Set(ByVal Value As Integer)
            csobj.stpidxb = Value
        End Set
        End Property
        Public Property inneriter() As Integer
        Get
            Return csobj.inneriter
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriter = Value
        End Set
        End Property
        Public Property outeriter() As Integer
        Get
            Return csobj.outeriter
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriter = Value
        End Set
        End Property
        Public csobj As alglib.optguardnonc1test1report
    End Class


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Matrix inverse report:
    '* terminationtype   completion code:
    '                    *  1 for success
    '                    * -3 for failure due to the matrix being singular or
    '                         nearly-singular
    '* r1                reciprocal of condition number in 1-norm
    '* rinf              reciprocal of condition number in inf-norm
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class matinvreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property r1() As Double
        Get
            Return csobj.r1
        End Get
        Set(ByVal Value As Double)
            csobj.r1 = Value
        End Set
        End Property
        Public Property rinf() As Double
        Get
            Return csobj.rinf
        End Get
        Set(ByVal Value As Double)
            csobj.rinf = Value
        End Set
        End Property
        Public csobj As alglib.matinvreport
    End Class


    Public Sub rmatrixluinverse(ByRef a(,) As Double, ByVal pivots() As Integer, ByVal n As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixluinverse(a, pivots, n, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixluinverse(ByRef a(,) As Double, ByVal pivots() As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixluinverse(a, pivots, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixinverse(ByRef a(,) As Double, ByVal n As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixinverse(a, n, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixinverse(ByRef a(,) As Double, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixinverse(a, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixluinverse(ByRef a(,) As alglib.complex, ByVal pivots() As Integer, ByVal n As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixluinverse(a, pivots, n, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixluinverse(ByRef a(,) As alglib.complex, ByVal pivots() As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixluinverse(a, pivots, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixinverse(ByRef a(,) As alglib.complex, ByVal n As Integer, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixinverse(a, n, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixinverse(ByRef a(,) As alglib.complex, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixinverse(a, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyinverse(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.spdmatrixcholeskyinverse(a, n, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixcholeskyinverse(ByRef a(,) As Double, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.spdmatrixcholeskyinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixinverse(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.spdmatrixinverse(a, n, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spdmatrixinverse(ByRef a(,) As Double, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.spdmatrixinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixcholeskyinverse(ByRef a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.hpdmatrixcholeskyinverse(a, n, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixcholeskyinverse(ByRef a(,) As alglib.complex, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.hpdmatrixcholeskyinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixinverse(ByRef a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.hpdmatrixinverse(a, n, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hpdmatrixinverse(ByRef a(,) As alglib.complex, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.hpdmatrixinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixtrinverse(ByRef a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixtrinverse(a, n, isupper, isunit, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixtrinverse(ByRef a(,) As Double, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.rmatrixtrinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixtrinverse(ByRef a(,) As alglib.complex, ByVal n As Integer, ByVal isupper As Boolean, ByVal isunit As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixtrinverse(a, n, isupper, isunit, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub cmatrixtrinverse(ByRef a(,) As alglib.complex, ByVal isupper As Boolean, ByRef rep As matinvreport)
        Try
            rep = New matinvreport()
            alglib.cmatrixtrinverse(a, isupper, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class minlbfgsstate
        Public csobj As alglib.minlbfgsstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* IterationsCount           total number of inner iterations
    '* NFEV                      number of gradient evaluations
    '* TerminationType           termination type (see below)
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '   1    relative function improvement is no more than EpsF.
    '   2    relative step is no more than EpsX.
    '   4    gradient norm is no more than EpsG
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    terminated    by  user  who  called  minlbfgsrequesttermination().
    '        X contains point which was   "current accepted"  when  termination
    '        request was submitted.
    '
    'Other fields of this structure are not documented and should not be used!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minlbfgsreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.minlbfgsreport
    End Class


    Public Sub minlbfgscreate(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByRef state As minlbfgsstate)
        Try
            state = New minlbfgsstate()
            alglib.minlbfgscreate(n, m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgscreate(ByVal m As Integer, ByVal x() As Double, ByRef state As minlbfgsstate)
        Try
            state = New minlbfgsstate()
            alglib.minlbfgscreate(m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgscreatef(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minlbfgsstate)
        Try
            state = New minlbfgsstate()
            alglib.minlbfgscreatef(n, m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgscreatef(ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minlbfgsstate)
        Try
            state = New minlbfgsstate()
            alglib.minlbfgscreatef(m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetcond(ByRef state As minlbfgsstate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minlbfgssetcond(state.csobj, epsg, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetxrep(ByRef state As minlbfgsstate, ByVal needxrep As Boolean)
        Try
            alglib.minlbfgssetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetstpmax(ByRef state As minlbfgsstate, ByVal stpmax As Double)
        Try
            alglib.minlbfgssetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetscale(ByRef state As minlbfgsstate, ByVal s() As Double)
        Try
            alglib.minlbfgssetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetprecdefault(ByRef state As minlbfgsstate)
        Try
            alglib.minlbfgssetprecdefault(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetpreccholesky(ByRef state As minlbfgsstate, ByVal p(,) As Double, ByVal isupper As Boolean)
        Try
            alglib.minlbfgssetpreccholesky(state.csobj, p, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetprecdiag(ByRef state As minlbfgsstate, ByVal d() As Double)
        Try
            alglib.minlbfgssetprecdiag(state.csobj, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetprecscale(ByRef state As minlbfgsstate)
        Try
            alglib.minlbfgssetprecscale(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minlbfgsiteration(ByRef state As minlbfgsstate) As Boolean
        Try
            minlbfgsiteration = alglib.minlbfgsiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM:
    ' 
    ' The LBFGS optimizer supports parallel numerical differentiation ('callback
    ' parallelism').  This  feature,  which  is  present  in  commercial  ALGLIB
    ' editions,  greatly  accelerates  numerical  differentiation  of  expensive
    ' targets.
    ' 
    ' Callback parallelism is usually beneficial computing a numerical  gradient
    ' requires more than several milliseconds. In this case the job of computing
    ' individual gradient components can be split between multiple threads. Even
    ' inexpensive  targets  can  benefit  from  parallelism, if  you  have  many
    ' variables.
    ' 
    ' ALGLIB Reference Manual, 'Working with commercial  version' section, tells
    ' how to activate callback parallelism for your programming language.
    ' 
    ' CALLBACKS ACCEPTED
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied gradient,  and one which uses function value
    '    only  and  numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    (either MinLBFGSCreate() for analytical gradient  or  MinLBFGSCreateF()
    '    for numerical differentiation) you should choose appropriate variant of
    '    MinLBFGSOptimize() - one  which  accepts  function  AND gradient or one
    '    which accepts function ONLY.
    ' 
    '    Be careful to choose variant of MinLBFGSOptimize() which corresponds to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to MinLBFGSOptimize()  and specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO MinLBFGSOptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinLBFGSCreateF() |     work                FAIL
    '    MinLBFGSCreate()  |     FAIL                work
    ' 
    '    Here "FAIL" denotes inappropriate combinations  of  optimizer  creation
    '    function  and  MinLBFGSOptimize()  version.   Attemps   to   use   such
    '    combination (for example, to create optimizer with MinLBFGSCreateF() and
    '    to pass gradient information to MinCGOptimize()) will lead to exception
    '    being thrown. Either  you  did  not pass gradient when it WAS needed or
    '    you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 20.03.2009 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minlbfgsoptimize(ByVal state As minlbfgsstate, ByVal func As ndimensional_func, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minlbfgsoptimize(state.csobj, New alglib.ndimensional_func(AddressOf func.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptimize(ByVal state As minlbfgsstate, ByVal grad As ndimensional_grad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minlbfgsoptimize(state.csobj, New alglib.ndimensional_grad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minlbfgsoptguardgradient(ByRef state As minlbfgsstate, ByVal teststep As Double)
        Try
            alglib.minlbfgsoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptguardsmoothness(ByRef state As minlbfgsstate, ByVal level As Integer)
        Try
            alglib.minlbfgsoptguardsmoothness(state.csobj, level)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptguardsmoothness(ByRef state As minlbfgsstate)
        Try
            alglib.minlbfgsoptguardsmoothness(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptguardresults(ByRef state As minlbfgsstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.minlbfgsoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptguardnonc1test0results(ByVal state As minlbfgsstate, ByRef strrep As optguardnonc1test0report, ByRef lngrep As optguardnonc1test0report)
        Try
            strrep = New optguardnonc1test0report()
            lngrep = New optguardnonc1test0report()
            alglib.minlbfgsoptguardnonc1test0results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsoptguardnonc1test1results(ByRef state As minlbfgsstate, ByRef strrep As optguardnonc1test1report, ByRef lngrep As optguardnonc1test1report)
        Try
            strrep = New optguardnonc1test1report()
            lngrep = New optguardnonc1test1report()
            alglib.minlbfgsoptguardnonc1test1results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsresults(ByVal state As minlbfgsstate, ByRef x() As Double, ByRef rep As minlbfgsreport)
        Try
            rep = New minlbfgsreport()
            alglib.minlbfgsresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsresultsbuf(ByVal state As minlbfgsstate, ByRef x() As Double, ByRef rep As minlbfgsreport)
        Try
            alglib.minlbfgsresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsrestartfrom(ByRef state As minlbfgsstate, ByVal x() As Double)
        Try
            alglib.minlbfgsrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgsrequesttermination(ByRef state As minlbfgsstate)
        Try
            alglib.minlbfgsrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub








































    Public Class minlpstate
        Public csobj As alglib.minlpstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* f                         target function value
    '* lagbc                     Lagrange coefficients for box constraints
    '* laglc                     Lagrange coefficients for linear constraints
    '* y                         dual variables
    '* stats                     array[N+M], statuses of box (N) and linear (M)
    '                            constraints. This array is filled only by  DSS
    '                            algorithm because IPM always stops at INTERIOR
    '                            point:
    '                            * stats[i]>0  =>  constraint at upper bound
    '                                              (also used for free non-basic
    '                                              variables set to zero)
    '                            * stats[i]<0  =>  constraint at lower bound
    '                            * stats[i]=0  =>  constraint is inactive, basic
    '                                              variable
    '* primalerror               primal feasibility error
    '* dualerror                 dual feasibility error
    '* slackerror                complementary slackness error
    '* iterationscount           iteration count
    '* terminationtype           completion code (see below)
    '
    'COMPLETION CODES
    '
    'Completion codes:
    '* -4    LP problem is primal unbounded (dual infeasible)
    '* -3    LP problem is primal infeasible (dual unbounded)
    '*  1..4 successful completion
    '*  5    MaxIts steps was taken
    '*  7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '
    'LAGRANGE COEFFICIENTS
    '
    'Positive Lagrange coefficient means that constraint is at its upper bound.
    'Negative coefficient means that constraint is at its lower  bound.  It  is
    'expected that at the solution the dual feasibility condition holds:
    '
    '    C + SUM(Ei*LagBC[i],i=0..n-1) + SUM(Ai*LagLC[i],i=0..m-1) ~ 0
    '
    'where
    '* C is a cost vector (linear term)
    '* Ei is a vector with 1.0 at position I and 0 in other positions
    '* Ai is an I-th row of linear constraint matrix
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minlpreport
        Public Property f() As Double
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double)
            csobj.f = Value
        End Set
        End Property
        Public Property lagbc() As Double()
        Get
            Return csobj.lagbc
        End Get
        Set(ByVal Value As Double())
            csobj.lagbc = Value
        End Set
        End Property
        Public Property laglc() As Double()
        Get
            Return csobj.laglc
        End Get
        Set(ByVal Value As Double())
            csobj.laglc = Value
        End Set
        End Property
        Public Property y() As Double()
        Get
            Return csobj.y
        End Get
        Set(ByVal Value As Double())
            csobj.y = Value
        End Set
        End Property
        Public Property stats() As Integer()
        Get
            Return csobj.stats
        End Get
        Set(ByVal Value As Integer())
            csobj.stats = Value
        End Set
        End Property
        Public Property primalerror() As Double
        Get
            Return csobj.primalerror
        End Get
        Set(ByVal Value As Double)
            csobj.primalerror = Value
        End Set
        End Property
        Public Property dualerror() As Double
        Get
            Return csobj.dualerror
        End Get
        Set(ByVal Value As Double)
            csobj.dualerror = Value
        End Set
        End Property
        Public Property slackerror() As Double
        Get
            Return csobj.slackerror
        End Get
        Set(ByVal Value As Double)
            csobj.slackerror = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.minlpreport
    End Class


    Public Sub minlpcreate(ByVal n As Integer, ByRef state As minlpstate)
        Try
            state = New minlpstate()
            alglib.minlpcreate(n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetalgodss(ByRef state As minlpstate, ByVal eps As Double)
        Try
            alglib.minlpsetalgodss(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetalgoipm(ByRef state As minlpstate, ByVal eps As Double)
        Try
            alglib.minlpsetalgoipm(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetalgoipm(ByRef state As minlpstate)
        Try
            alglib.minlpsetalgoipm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetcost(ByRef state As minlpstate, ByVal c() As Double)
        Try
            alglib.minlpsetcost(state.csobj, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetscale(ByRef state As minlpstate, ByVal s() As Double)
        Try
            alglib.minlpsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetbc(ByRef state As minlpstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minlpsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetbcall(ByRef state As minlpstate, ByVal bndl As Double, ByVal bndu As Double)
        Try
            alglib.minlpsetbcall(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetbci(ByRef state As minlpstate, ByVal i As Integer, ByVal bndl As Double, ByVal bndu As Double)
        Try
            alglib.minlpsetbci(state.csobj, i, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetlc(ByRef state As minlpstate, ByVal a(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minlpsetlc(state.csobj, a, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetlc(ByRef state As minlpstate, ByVal a(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minlpsetlc(state.csobj, a, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetlc2dense(ByRef state As minlpstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minlpsetlc2dense(state.csobj, a, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetlc2dense(ByRef state As minlpstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minlpsetlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpsetlc2(ByRef state As minlpstate, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minlpsetlc2(state.csobj, a.csobj, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpaddlc2dense(ByRef state As minlpstate, ByVal a() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minlpaddlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpaddlc2(ByRef state As minlpstate, ByVal idxa() As Integer, ByVal vala() As Double, ByVal nnz As Integer, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minlpaddlc2(state.csobj, idxa, vala, nnz, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpoptimize(ByRef state As minlpstate)
        Try
            alglib.minlpoptimize(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpresults(ByVal state As minlpstate, ByRef x() As Double, ByRef rep As minlpreport)
        Try
            rep = New minlpreport()
            alglib.minlpresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlpresultsbuf(ByVal state As minlpstate, ByRef x() As Double, ByRef rep As minlpreport)
        Try
            alglib.minlpresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class qpxproblem
        Public csobj As alglib.qpxproblem
    End Class
    Public Class lptestproblem
        Public csobj As alglib.lptestproblem
    End Class
    Public Sub lptestproblemserialize(ByVal obj As lptestproblem, ByRef s_out As String)
        Try
            alglib.lptestproblemserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub lptestproblemunserialize(ByVal s_in As String, ByRef obj As lptestproblem)
        Try
            alglib.lptestproblemunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lptestproblemcreate(ByVal n As Integer, ByVal hasknowntarget As Boolean, ByVal targetf As Double, ByRef p As lptestproblem)
        Try
            p = New lptestproblem()
            alglib.lptestproblemcreate(n, hasknowntarget, targetf, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function lptestproblemhasknowntarget(ByRef p As lptestproblem) As Boolean
        Try
            lptestproblemhasknowntarget = alglib.lptestproblemhasknowntarget(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lptestproblemgettargetf(ByRef p As lptestproblem) As Double
        Try
            lptestproblemgettargetf = alglib.lptestproblemgettargetf(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lptestproblemgetn(ByRef p As lptestproblem) As Integer
        Try
            lptestproblemgetn = alglib.lptestproblemgetn(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lptestproblemgetm(ByRef p As lptestproblem) As Integer
        Try
            lptestproblemgetm = alglib.lptestproblemgetm(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub lptestproblemsetscale(ByRef p As lptestproblem, ByVal s() As Double)
        Try
            alglib.lptestproblemsetscale(p.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lptestproblemsetcost(ByRef p As lptestproblem, ByVal c() As Double)
        Try
            alglib.lptestproblemsetcost(p.csobj, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lptestproblemsetbc(ByRef p As lptestproblem, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.lptestproblemsetbc(p.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lptestproblemsetlc2(ByRef p As lptestproblem, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal m As Integer)
        Try
            alglib.lptestproblemsetlc2(p.csobj, a.csobj, al, au, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub xdbgminlpcreatefromtestproblem(ByVal p As lptestproblem, ByRef state As minlpstate)
        Try
            state = New minlpstate()
            alglib.xdbgminlpcreatefromtestproblem(p.csobj, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemcreate(ByVal n As Integer, ByRef p As qpxproblem)
        Try
            p = New qpxproblem()
            alglib.qpxproblemcreate(n, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function qpxproblemisquadraticobjective(ByRef p As qpxproblem) As Boolean
        Try
            qpxproblemisquadraticobjective = alglib.qpxproblemisquadraticobjective(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function qpxproblemgetn(ByRef p As qpxproblem) As Integer
        Try
            qpxproblemgetn = alglib.qpxproblemgetn(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function qpxproblemgetmlc(ByRef p As qpxproblem) As Integer
        Try
            qpxproblemgetmlc = alglib.qpxproblemgetmlc(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function qpxproblemgetmqc(ByRef p As qpxproblem) As Integer
        Try
            qpxproblemgetmqc = alglib.qpxproblemgetmqc(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function qpxproblemgetmcc(ByRef p As qpxproblem) As Integer
        Try
            qpxproblemgetmcc = alglib.qpxproblemgetmcc(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function qpxproblemgettotalconstraints(ByRef p As qpxproblem) As Integer
        Try
            qpxproblemgettotalconstraints = alglib.qpxproblemgettotalconstraints(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub qpxproblemsetinitialpoint(ByRef p As qpxproblem, ByVal x0() As Double)
        Try
            alglib.qpxproblemsetinitialpoint(p.csobj, x0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetinitialpoint(ByRef p As qpxproblem, ByRef x0() As Double)
        Try
            alglib.qpxproblemgetinitialpoint(p.csobj, x0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function qpxproblemhasinitialpoint(ByRef p As qpxproblem) As Boolean
        Try
            qpxproblemhasinitialpoint = alglib.qpxproblemhasinitialpoint(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub qpxproblemsetscale(ByRef p As qpxproblem, ByVal s() As Double)
        Try
            alglib.qpxproblemsetscale(p.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetscale(ByRef p As qpxproblem, ByRef s() As Double)
        Try
            alglib.qpxproblemgetscale(p.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function qpxproblemhasscale(ByRef p As qpxproblem) As Boolean
        Try
            qpxproblemhasscale = alglib.qpxproblemhasscale(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub qpxproblemsetorigin(ByRef p As qpxproblem, ByVal xorigin() As Double)
        Try
            alglib.qpxproblemsetorigin(p.csobj, xorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetorigin(ByRef p As qpxproblem, ByRef xorigin() As Double)
        Try
            alglib.qpxproblemgetorigin(p.csobj, xorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function qpxproblemhasorigin(ByRef p As qpxproblem) As Boolean
        Try
            qpxproblemhasorigin = alglib.qpxproblemhasorigin(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub qpxproblemsetlinearterm(ByRef p As qpxproblem, ByVal c() As Double)
        Try
            alglib.qpxproblemsetlinearterm(p.csobj, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetlinearterm(ByRef p As qpxproblem, ByRef c() As Double)
        Try
            alglib.qpxproblemgetlinearterm(p.csobj, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemsetquadraticterm(ByRef p As qpxproblem, ByVal q As sparsematrix, ByVal isupper As Boolean)
        Try
            alglib.qpxproblemsetquadraticterm(p.csobj, q.csobj, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetquadraticterm(ByRef p As qpxproblem, ByRef q As sparsematrix, ByRef isupper As Boolean)
        Try
            q = New sparsematrix()
            alglib.qpxproblemgetquadraticterm(p.csobj, q.csobj, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function qpxproblemhasquadraticterm(ByRef p As qpxproblem) As Boolean
        Try
            qpxproblemhasquadraticterm = alglib.qpxproblemhasquadraticterm(p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub qpxproblemsetbc(ByRef p As qpxproblem, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.qpxproblemsetbc(p.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetbc(ByRef p As qpxproblem, ByRef bndl() As Double, ByRef bndu() As Double)
        Try
            alglib.qpxproblemgetbc(p.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemsetlc2(ByRef p As qpxproblem, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal m As Integer)
        Try
            alglib.qpxproblemsetlc2(p.csobj, a.csobj, al, au, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetlc2(ByRef p As qpxproblem, ByRef a As sparsematrix, ByRef al() As Double, ByRef au() As Double, ByRef m As Integer)
        Try
            a = New sparsematrix()
            alglib.qpxproblemgetlc2(p.csobj, a.csobj, al, au, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemaddqc2(ByRef p As qpxproblem, ByVal q As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByVal cl As Double, ByVal cu As Double, ByVal applyorigin As Boolean)
        Try
            alglib.qpxproblemaddqc2(p.csobj, q.csobj, isupper, b, cl, cu, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub qpxproblemgetqc2i(ByRef p As qpxproblem, ByVal idx As Integer, ByRef q As sparsematrix, ByRef isupper As Boolean, ByRef b() As Double, ByRef cl As Double, ByRef cu As Double, ByRef applyorigin As Boolean)
        Try
            q = New sparsematrix()
            alglib.qpxproblemgetqc2i(p.csobj, idx, q.csobj, isupper, b, cl, cu, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minqpstate
        Public csobj As alglib.minqpstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* InnerIterationsCount      number of inner iterations
    '* OuterIterationsCount      number of outer iterations
    '* NCholesky                 number of Cholesky decomposition
    '* NMV                       number of matrix-vector products
    '                            (only products calculated as part of iterative
    '                            process are counted)
    '* TerminationType           completion code (see below)
    '* F                         for positive terminationtype stores quadratic
    '                            model value at the solution
    '* LagBC                     Lagrange multipliers for box constraints,
    '                            array[N]
    '* LagLC                     Lagrange multipliers for linear constraints,
    '                            array[MSparse+MDense]
    '* LagQC                     Lagrange multipliers for quadratic constraints
    '
    '=== COMPLETION CODES =====================================================
    '
    'Completion codes:
    '* -9    failure of the automatic scale evaluation:  one  of  the  diagonal
    '        elements of the quadratic term is non-positive.  Specify  variable
    '        scales manually!
    '* -5    inappropriate solver was used:
    '        * QuickQP solver for a problem with general linear constraints (dense/sparse)
    '        * QuickQP/DENSE-AUL/DENSE-IPM/SPARSE-IPM for a problem with
    '          quadratic/conic constraints
    '        * ECQP for a problem with inequality or nonlinear equality constraints
    '* -4    the problem is highly likely to be unbounded; either one of the solvers
    '        found an unconstrained direction of negative curvature, or objective
    '        simply decreased for too much (more than 1E50).
    '* -3    inconsistent constraints (or, maybe, feasible point is
    '        too hard to find). If you are sure that constraints are feasible,
    '        try to restart optimizer with better initial approximation.
    '* -2    IPM solver has difficulty finding primal/dual feasible point.
    '        It is likely that the problem is either infeasible or unbounded,
    '        but it is difficult to determine exact reason for termination.
    '        X contains best point found so far.
    '*  1..4 successful completion
    '*  5    MaxIts steps was taken
    '*  7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '
    '=== LAGRANGE MULTIPLIERS =================================================
    '
    'Some  optimizers  report  values of  Lagrange  multipliers  on  successful
    'completion (positive completion code):
    '* dense and sparse IPM/GENIPM return very precise Lagrange  multipliers as
    '  determined during solution process.
    '* DENSE-AUL-QP returns approximate Lagrange multipliers  (which  are  very
    '  close to "true"  Lagrange  multipliers  except  for  overconstrained  or
    '  degenerate problems)
    '
    'Three arrays of multipliers are returned:
    '* LagBC is array[N] which is loaded with multipliers from box constraints;
    '  LagBC[i]>0 means that I-th constraint is at the  upper bound, LagBC[I]<0
    '  means that I-th constraint is at the lower bound, LagBC[I]=0 means  that
    '  I-th box constraint is inactive.
    '* LagLC is array[MSparse+MDense] which is  loaded  with  multipliers  from
    '  general  linear  constraints  (former  MSparse  elements  corresponds to
    '  sparse part of the constraint matrix, latter MDense are  for  the  dense
    '  constraints, as was specified by user).
    '  LagLC[i]>0 means that I-th constraint at  the  upper  bound,  LagLC[i]<0
    '  means that I-th constraint is at the lower bound, LagLC[i]=0 means  that
    '  I-th linear constraint is inactive.
    '* LagQC is array[MQC]  which stores multipliers for quadratic constraints.
    '  LagQC[i]>0 means that I-th constraint at  the  upper  bound,  LagQC[i]<0
    '  means that I-th constraint is at the lower bound, LagQC[i]=0 means  that
    '  I-th linear constraint is inactive.
    '
    'On failure (or when optimizer does not support Lagrange multipliers) these
    'arrays are zero-filled.
    '
    'It is expected that at solution the dual feasibility condition holds:
    '
    '    C+H*(Xs-X0) + SUM(Ei*LagBC[i],i=0..n-1) + SUM(Ai*LagLC[i],i=0..m-1) + ... ~ 0
    '
    'where
    '* C is a linear term
    '* H is a quadratic term
    '* Xs is a solution, and X0 is an origin term (zero by default)
    '* Ei is a vector with 1.0 at position I and 0 in other positions
    '* Ai is an I-th row of linear constraint matrix
    '
    'NOTE: methods  from  IPM  family  may  also  return  meaningful   Lagrange
    '      multipliers  on  completion   with   code   -2   (infeasibility   or
    '      unboundedness  detected).
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minqpreport
        Public Property inneriterationscount() As Integer
        Get
            Return csobj.inneriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriterationscount = Value
        End Set
        End Property
        Public Property outeriterationscount() As Integer
        Get
            Return csobj.outeriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriterationscount = Value
        End Set
        End Property
        Public Property nmv() As Integer
        Get
            Return csobj.nmv
        End Get
        Set(ByVal Value As Integer)
            csobj.nmv = Value
        End Set
        End Property
        Public Property ncholesky() As Integer
        Get
            Return csobj.ncholesky
        End Get
        Set(ByVal Value As Integer)
            csobj.ncholesky = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property f() As Double
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double)
            csobj.f = Value
        End Set
        End Property
        Public Property lagbc() As Double()
        Get
            Return csobj.lagbc
        End Get
        Set(ByVal Value As Double())
            csobj.lagbc = Value
        End Set
        End Property
        Public Property laglc() As Double()
        Get
            Return csobj.laglc
        End Get
        Set(ByVal Value As Double())
            csobj.laglc = Value
        End Set
        End Property
        Public Property lagqc() As Double()
        Get
            Return csobj.lagqc
        End Get
        Set(ByVal Value As Double())
            csobj.lagqc = Value
        End Set
        End Property
        Public csobj As alglib.minqpreport
    End Class


    Public Sub minqpcreate(ByVal n As Integer, ByRef state As minqpstate)
        Try
            state = New minqpstate()
            alglib.minqpcreate(n, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlinearterm(ByRef state As minqpstate, ByVal b() As Double)
        Try
            alglib.minqpsetlinearterm(state.csobj, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetquadraticterm(ByRef state As minqpstate, ByVal a(,) As Double, ByVal isupper As Boolean)
        Try
            alglib.minqpsetquadraticterm(state.csobj, a, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetquadratictermsparse(ByRef state As minqpstate, ByVal a As sparsematrix, ByVal isupper As Boolean)
        Try
            alglib.minqpsetquadratictermsparse(state.csobj, a.csobj, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetstartingpoint(ByRef state As minqpstate, ByVal x() As Double)
        Try
            alglib.minqpsetstartingpoint(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetorigin(ByRef state As minqpstate, ByVal xorigin() As Double)
        Try
            alglib.minqpsetorigin(state.csobj, xorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetscale(ByRef state As minqpstate, ByVal s() As Double)
        Try
            alglib.minqpsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetscaleautodiag(ByRef state As minqpstate)
        Try
            alglib.minqpsetscaleautodiag(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgodenseaul(ByRef state As minqpstate, ByVal epsx As Double, ByVal rho As Double, ByVal itscnt As Integer)
        Try
            alglib.minqpsetalgodenseaul(state.csobj, epsx, rho, itscnt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgodenseipm(ByRef state As minqpstate, ByVal eps As Double)
        Try
            alglib.minqpsetalgodenseipm(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgosparseipm(ByRef state As minqpstate, ByVal eps As Double)
        Try
            alglib.minqpsetalgosparseipm(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgodensegenipm(ByRef state As minqpstate, ByVal eps As Double)
        Try
            alglib.minqpsetalgodensegenipm(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgosparsegenipm(ByRef state As minqpstate, ByVal eps As Double)
        Try
            alglib.minqpsetalgosparsegenipm(state.csobj, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgosparseecqp(ByRef state As minqpstate)
        Try
            alglib.minqpsetalgosparseecqp(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetalgoquickqp(ByRef state As minqpstate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxouterits As Integer, ByVal usenewton As Boolean)
        Try
            alglib.minqpsetalgoquickqp(state.csobj, epsg, epsf, epsx, maxouterits, usenewton)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetbc(ByRef state As minqpstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minqpsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetbcall(ByRef state As minqpstate, ByVal bndl As Double, ByVal bndu As Double)
        Try
            alglib.minqpsetbcall(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetbci(ByRef state As minqpstate, ByVal i As Integer, ByVal bndl As Double, ByVal bndu As Double)
        Try
            alglib.minqpsetbci(state.csobj, i, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc(ByRef state As minqpstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minqpsetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc(ByRef state As minqpstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minqpsetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlcsparse(ByRef state As minqpstate, ByVal c As sparsematrix, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minqpsetlcsparse(state.csobj, c.csobj, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlcmixed(ByRef state As minqpstate, ByVal sparsec As sparsematrix, ByVal sparsect() As Integer, ByVal sparsek As Integer, ByVal densec(,) As Double, ByVal densect() As Integer, ByVal densek As Integer)
        Try
            alglib.minqpsetlcmixed(state.csobj, sparsec.csobj, sparsect, sparsek, densec, densect, densek)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlcmixedlegacy(ByRef state As minqpstate, ByVal densec(,) As Double, ByVal densect() As Integer, ByVal densek As Integer, ByVal sparsec As sparsematrix, ByVal sparsect() As Integer, ByVal sparsek As Integer)
        Try
            alglib.minqpsetlcmixedlegacy(state.csobj, densec, densect, densek, sparsec.csobj, sparsect, sparsek)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc2dense(ByRef state As minqpstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minqpsetlc2dense(state.csobj, a, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc2dense(ByRef state As minqpstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minqpsetlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc2(ByRef state As minqpstate, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minqpsetlc2(state.csobj, a.csobj, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpsetlc2mixed(ByRef state As minqpstate, ByVal sparsea As sparsematrix, ByVal ksparse As Integer, ByVal densea(,) As Double, ByVal kdense As Integer, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minqpsetlc2mixed(state.csobj, sparsea.csobj, ksparse, densea, kdense, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpaddlc2dense(ByRef state As minqpstate, ByVal a() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minqpaddlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpaddlc2(ByRef state As minqpstate, ByVal idxa() As Integer, ByVal vala() As Double, ByVal nnz As Integer, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minqpaddlc2(state.csobj, idxa, vala, nnz, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpaddlc2sparsefromdense(ByRef state As minqpstate, ByVal da() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minqpaddlc2sparsefromdense(state.csobj, da, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpclearqc(ByRef state As minqpstate)
        Try
            alglib.minqpclearqc(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minqpaddqc2(ByRef state As minqpstate, ByVal q As sparsematrix, ByVal isupper As Boolean, ByVal b() As Double, ByVal cl As Double, ByVal cu As Double, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddqc2 = alglib.minqpaddqc2(state.csobj, q.csobj, isupper, b, cl, cu, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function minqpaddqc2list(ByRef state As minqpstate, ByVal qridx() As Integer, ByVal qcidx() As Integer, ByVal qvals() As Double, ByVal qnnz As Integer, ByVal isupper As Boolean, ByVal bidx() As Integer, ByVal bvals() As Double, ByVal bnnz As Integer, ByVal cl As Double, ByVal cu As Double, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddqc2list = alglib.minqpaddqc2list(state.csobj, qridx, qcidx, qvals, qnnz, isupper, bidx, bvals, bnnz, cl, cu, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function minqpaddqc2dense(ByRef state As minqpstate, ByVal q(,) As Double, ByVal isupper As Boolean, ByVal b() As Double, ByVal cl As Double, ByVal cu As Double, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddqc2dense = alglib.minqpaddqc2dense(state.csobj, q, isupper, b, cl, cu, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub minqpclearcc(ByRef state As minqpstate)
        Try
            alglib.minqpclearcc(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minqpaddsoccprimitive(ByRef state As minqpstate, ByVal range0 As Integer, ByVal range1 As Integer, ByVal axisidx As Integer, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddsoccprimitive = alglib.minqpaddsoccprimitive(state.csobj, range0, range1, axisidx, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function minqpaddsoccorthogonal(ByRef state As minqpstate, ByVal idx() As Integer, ByVal a() As Double, ByVal c() As Double, ByVal k As Integer, ByVal theta As Double, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddsoccorthogonal = alglib.minqpaddsoccorthogonal(state.csobj, idx, a, c, k, theta, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function minqpaddpowccprimitive(ByRef state As minqpstate, ByVal range0 As Integer, ByVal range1 As Integer, ByVal axisidx As Integer, ByVal alpha As Double, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddpowccprimitive = alglib.minqpaddpowccprimitive(state.csobj, range0, range1, axisidx, alpha, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function minqpaddpowccorthogonal(ByRef state As minqpstate, ByVal idx() As Integer, ByVal a() As Double, ByVal c() As Double, ByVal k As Integer, ByVal theta As Double, ByVal alphav() As Double, ByVal kpow As Integer, ByVal applyorigin As Boolean) As Integer
        Try
            minqpaddpowccorthogonal = alglib.minqpaddpowccorthogonal(state.csobj, idx, a, c, k, theta, alphav, kpow, applyorigin)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub minqpoptimize(ByRef state As minqpstate)
        Try
            alglib.minqpoptimize(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpresults(ByVal state As minqpstate, ByRef x() As Double, ByRef rep As minqpreport)
        Try
            rep = New minqpreport()
            alglib.minqpresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpresultsbuf(ByVal state As minqpstate, ByRef x() As Double, ByRef rep As minqpreport)
        Try
            alglib.minqpresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpexport(ByRef state As minqpstate, ByRef p As qpxproblem)
        Try
            p = New qpxproblem()
            alglib.minqpexport(state.csobj, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minqpimport(ByRef p As qpxproblem, ByRef s As minqpstate)
        Try
            s = New minqpstate()
            alglib.minqpimport(p.csobj, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class minlmstate
        Public csobj As alglib.minlmstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Optimization report, filled by MinLMResults() function
    '
    'FIELDS:
    '* TerminationType, completetion code:
    '    * -8    optimizer detected NAN/INF values either in the function itself,
    '            or in its Jacobian
    '    * -5    inappropriate solver was used:
    '            * solver created with minlmcreatefgh() used  on  problem  with
    '              general linear constraints (set with minlmsetlc() call).
    '    * -3    constraints are inconsistent
    '    *  2    relative step is no more than EpsX.
    '    *  5    MaxIts steps was taken
    '    *  7    stopping conditions are too stringent,
    '            further improvement is impossible
    '    *  8    terminated   by  user  who  called  MinLMRequestTermination().
    '            X contains point which was "current accepted" when termination
    '            request was submitted.
    '* F, objective value, SUM(f[i]^2)
    '* IterationsCount, contains iterations count
    '* NFunc, number of function calculations
    '* NJac, number of Jacobi matrix calculations
    '* NGrad, number of gradient calculations
    '* NHess, number of Hessian calculations
    '* NCholesky, number of Cholesky decomposition calculations
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minlmreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property f() As Double
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double)
            csobj.f = Value
        End Set
        End Property
        Public Property nfunc() As Integer
        Get
            Return csobj.nfunc
        End Get
        Set(ByVal Value As Integer)
            csobj.nfunc = Value
        End Set
        End Property
        Public Property njac() As Integer
        Get
            Return csobj.njac
        End Get
        Set(ByVal Value As Integer)
            csobj.njac = Value
        End Set
        End Property
        Public Property ngrad() As Integer
        Get
            Return csobj.ngrad
        End Get
        Set(ByVal Value As Integer)
            csobj.ngrad = Value
        End Set
        End Property
        Public Property nhess() As Integer
        Get
            Return csobj.nhess
        End Get
        Set(ByVal Value As Integer)
            csobj.nhess = Value
        End Set
        End Property
        Public Property ncholesky() As Integer
        Get
            Return csobj.ncholesky
        End Get
        Set(ByVal Value As Integer)
            csobj.ncholesky = Value
        End Set
        End Property
        Public csobj As alglib.minlmreport
    End Class


    Public Sub minlmcreatevj(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByRef state As minlmstate)
        Try
            state = New minlmstate()
            alglib.minlmcreatevj(n, m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmcreatevj(ByVal m As Integer, ByVal x() As Double, ByRef state As minlmstate)
        Try
            state = New minlmstate()
            alglib.minlmcreatevj(m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmcreatev(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minlmstate)
        Try
            state = New minlmstate()
            alglib.minlmcreatev(n, m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmcreatev(ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minlmstate)
        Try
            state = New minlmstate()
            alglib.minlmcreatev(m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetcond(ByRef state As minlmstate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minlmsetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetxrep(ByRef state As minlmstate, ByVal needxrep As Boolean)
        Try
            alglib.minlmsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetstpmax(ByRef state As minlmstate, ByVal stpmax As Double)
        Try
            alglib.minlmsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetscale(ByRef state As minlmstate, ByVal s() As Double)
        Try
            alglib.minlmsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetbc(ByRef state As minlmstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minlmsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetlc(ByRef state As minlmstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minlmsetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetlc(ByRef state As minlmstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minlmsetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetacctype(ByRef state As minlmstate, ByVal acctype As Integer)
        Try
            alglib.minlmsetacctype(state.csobj, acctype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetnonmonotonicsteps(ByRef state As minlmstate, ByVal cnt As Integer)
        Try
            alglib.minlmsetnonmonotonicsteps(state.csobj, cnt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmsetnumdiff(ByRef state As minlmstate, ByVal formulatype As Integer)
        Try
            alglib.minlmsetnumdiff(state.csobj, formulatype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minlmiteration(ByRef state As minlmstate) As Boolean
        Try
            minlmiteration = alglib.minlmiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM
    ' 
    ' The MINLM optimizer supports parallel parallel  numerical  differentiation
    ' ('callback parallelism'). This feature, which  is  present  in  commercial
    ' ALGLIB  editions,  greatly   accelerates   optimization   with   numerical
    ' differentiation of an expensive target functions.
    ' 
    ' Callback parallelism is usually  beneficial  when  computing  a  numerical
    ' gradient requires more than several  milliseconds.  In this case  the  job
    ' of computing individual gradient components can be split between  multiple
    ' threads. Even inexpensive targets can benefit  from  parallelism,  if  you
    ' have many variables.
    ' 
    ' If you solve a curve fitting problem, i.e. the function vector is actually
    ' the same function computed at different points of a data points space,  it
    ' may  be  better  to  use  an LSFIT curve fitting solver, which offers more
    ' fine-grained parallelism due to knowledge of  the  problem  structure.  In
    ' particular, it can accelerate both numerical differentiation  and problems
    ' with user-supplied gradients.
    ' 
    ' ALGLIB Reference Manual, 'Working with commercial  version' section, tells
    ' how to activate callback parallelism for your programming language.
    ' 
    '   -- ALGLIB --
    '      Copyright 03.12.2023 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minlmoptimize(ByVal state As minlmstate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minlmoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minlmoptimize(ByVal state As minlmstate, ByVal fvec As ndimensional_fvec, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minlmoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minlmoptguardgradient(ByRef state As minlmstate, ByVal teststep As Double)
        Try
            alglib.minlmoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmoptguardresults(ByRef state As minlmstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.minlmoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmresults(ByVal state As minlmstate, ByRef x() As Double, ByRef rep As minlmreport)
        Try
            rep = New minlmreport()
            alglib.minlmresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmresultsbuf(ByVal state As minlmstate, ByRef x() As Double, ByRef rep As minlmreport)
        Try
            alglib.minlmresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmrestartfrom(ByRef state As minlmstate, ByVal x() As Double)
        Try
            alglib.minlmrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlmrequesttermination(ByRef state As minlmstate)
        Try
            alglib.minlmrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class mincgstate
        Public csobj As alglib.mincgstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* IterationsCount           total number of inner iterations
    '* NFEV                      number of gradient evaluations
    '* TerminationType           termination type (see below)
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '   1    relative function improvement is no more than EpsF.
    '   2    relative step is no more than EpsX.
    '   4    gradient norm is no more than EpsG
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    terminated by user who called mincgrequesttermination(). X contains
    '        point which was "current accepted" when  termination  request  was
    '        submitted.
    '
    'Other fields of this structure are not documented and should not be used!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mincgreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.mincgreport
    End Class


    Public Sub mincgcreate(ByVal n As Integer, ByVal x() As Double, ByRef state As mincgstate)
        Try
            state = New mincgstate()
            alglib.mincgcreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgcreate(ByVal x() As Double, ByRef state As mincgstate)
        Try
            state = New mincgstate()
            alglib.mincgcreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgcreatef(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As mincgstate)
        Try
            state = New mincgstate()
            alglib.mincgcreatef(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgcreatef(ByVal x() As Double, ByVal diffstep As Double, ByRef state As mincgstate)
        Try
            state = New mincgstate()
            alglib.mincgcreatef(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetcond(ByRef state As mincgstate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.mincgsetcond(state.csobj, epsg, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetscale(ByRef state As mincgstate, ByVal s() As Double)
        Try
            alglib.mincgsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetxrep(ByRef state As mincgstate, ByVal needxrep As Boolean)
        Try
            alglib.mincgsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetcgtype(ByRef state As mincgstate, ByVal cgtype As Integer)
        Try
            alglib.mincgsetcgtype(state.csobj, cgtype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetstpmax(ByRef state As mincgstate, ByVal stpmax As Double)
        Try
            alglib.mincgsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsuggeststep(ByRef state As mincgstate, ByVal stp As Double)
        Try
            alglib.mincgsuggeststep(state.csobj, stp)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetprecdefault(ByRef state As mincgstate)
        Try
            alglib.mincgsetprecdefault(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetprecdiag(ByRef state As mincgstate, ByVal d() As Double)
        Try
            alglib.mincgsetprecdiag(state.csobj, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgsetprecscale(ByRef state As mincgstate)
        Try
            alglib.mincgsetprecscale(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mincgiteration(ByRef state As mincgstate) As Boolean
        Try
            mincgiteration = alglib.mincgiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied  gradient, and one which uses function value
    '    only  and  numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    (either MinCGCreate()  for analytical gradient  or  MinCGCreateF()  for
    '    numerical differentiation) you should  choose  appropriate  variant  of
    '    MinCGOptimize() - one which accepts function AND gradient or one  which
    '    accepts function ONLY.
    ' 
    '    Be careful to choose variant of MinCGOptimize()  which  corresponds  to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed  to  MinCGOptimize()  and  specific
    '    function used to create optimizer.
    ' 
    ' 
    '                   |         USER PASSED TO MinCGOptimize()
    '    CREATED WITH   |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinCGCreateF() |     work                FAIL
    '    MinCGCreate()  |     FAIL                work
    ' 
    '    Here "FAIL" denotes inappropriate combinations  of  optimizer  creation
    '    function and MinCGOptimize() version. Attemps to use  such  combination
    '    (for  example,  to create optimizer with  MinCGCreateF()  and  to  pass
    '    gradient information to MinCGOptimize()) will lead to  exception  being
    '    thrown. Either  you  did  not  pass  gradient when it WAS needed or you
    '    passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 20.04.2009 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub mincgoptimize(ByVal state As mincgstate, ByVal func As ndimensional_func, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.mincgoptimize(state.csobj, New alglib.ndimensional_func(AddressOf func.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub mincgoptimize(ByVal state As mincgstate, ByVal grad As ndimensional_grad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.mincgoptimize(state.csobj, New alglib.ndimensional_grad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub mincgoptguardgradient(ByRef state As mincgstate, ByVal teststep As Double)
        Try
            alglib.mincgoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgoptguardsmoothness(ByRef state As mincgstate, ByVal level As Integer)
        Try
            alglib.mincgoptguardsmoothness(state.csobj, level)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgoptguardsmoothness(ByRef state As mincgstate)
        Try
            alglib.mincgoptguardsmoothness(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgoptguardresults(ByRef state As mincgstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.mincgoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgoptguardnonc1test0results(ByVal state As mincgstate, ByRef strrep As optguardnonc1test0report, ByRef lngrep As optguardnonc1test0report)
        Try
            strrep = New optguardnonc1test0report()
            lngrep = New optguardnonc1test0report()
            alglib.mincgoptguardnonc1test0results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgoptguardnonc1test1results(ByRef state As mincgstate, ByRef strrep As optguardnonc1test1report, ByRef lngrep As optguardnonc1test1report)
        Try
            strrep = New optguardnonc1test1report()
            lngrep = New optguardnonc1test1report()
            alglib.mincgoptguardnonc1test1results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgresults(ByVal state As mincgstate, ByRef x() As Double, ByRef rep As mincgreport)
        Try
            rep = New mincgreport()
            alglib.mincgresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgresultsbuf(ByVal state As mincgstate, ByRef x() As Double, ByRef rep As mincgreport)
        Try
            alglib.mincgresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgrestartfrom(ByRef state As mincgstate, ByVal x() As Double)
        Try
            alglib.mincgrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mincgrequesttermination(ByRef state As mincgstate)
        Try
            alglib.mincgrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub













    Public Class mindfstate
        Public csobj As alglib.mindfstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* f                         objective value at the solution
    '* iterationscount           total number of inner iterations
    '* nfev                      number of gradient evaluations
    '* terminationtype           termination type (see below)
    '* bcerr                     maximum violation of box constraints
    '* lcerr                     maximum violation of linear constraints
    '* nlcerr                    maximum violation of nonlinear constraints
    '
    'If timers were activated, the structure also stores running times:
    '* timesolver                time (in seconds, stored as a floating-point
    '                            value) spent in the solver itself. Time spent
    '                            in the user callback is not included.
    '                            See 'TIMERS' below for more information.
    '* timecallback              time (in seconds, stored as a floating-point
    '                            value) spent in the user callback.
    '                            See 'TIMERS' below for more information.
    '* timetotal                 total time spent during the optimization,
    '                            including both the solver and callbacks.
    '                            See 'TIMERS' below for more information.
    'In  order  to  activate timers, the caller has to  call  mindfusetimers()
    'function.
    '
    'Other fields of this structure are not documented and should not be used!
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '  -3    box constraints are inconsistent
    '  -1    inconsistent parameters were passed:
    '        * penalty parameter is zero, but we have nonlinear constraints
    '          set by mindfsetnlc2()
    '   1    function value has converged within epsf
    '   2    sampling radius decreased below epsx
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    User requested termination via mindfrequesttermination()
    '
    'TIMERS
    '
    'Starting from ALGLIB 4.04, many optimizers report time  spent  in  in  the
    'solver itself and in user  callbacks. The  time  is  reported  in seconds,
    'using floating-point (i.e. fractional-length intervals can  be  reported).
    '
    'In  order  to  activate timers, the caller has to  call  mindfusetimers()
    'function.
    '
    'The accuracy of   the  reported  value depends on the specific programming
    'language and OS being used:
    '* C++, no AE_OS is #defined - the accuracy is that of time() function, i.e.
    '  one second.
    '* C++, AE_OS=AE_WINDOWS is #defined - the accuracy is that of GetTickCount(),
    '  i.e. about 10-20ms
    '* C++, AE_OS=AE_POSIX is #defined - the accuracy is that of gettimeofday()
    '* C#, managed core, any OS - the accuracy is that of Environment.TickCount
    '* C#, HPC core, any OS - the accuracy is that of a corresponding C++ version
    '* any other language - the accuracy is that of a corresponding C++ version
    '
    'Whilst modern operating systems provide more accurate timers, these timers
    'often have significant overhead or backward  compatibility  issues.  Thus,
    'ALGLIB stick to the most basic and efficient functions, even at  the  cost
    'of some accuracy being lost.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mindfreport
        Public Property f() As Double
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double)
            csobj.f = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property bcerr() As Double
        Get
            Return csobj.bcerr
        End Get
        Set(ByVal Value As Double)
            csobj.bcerr = Value
        End Set
        End Property
        Public Property lcerr() As Double
        Get
            Return csobj.lcerr
        End Get
        Set(ByVal Value As Double)
            csobj.lcerr = Value
        End Set
        End Property
        Public Property nlcerr() As Double
        Get
            Return csobj.nlcerr
        End Get
        Set(ByVal Value As Double)
            csobj.nlcerr = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property timetotal() As Double
        Get
            Return csobj.timetotal
        End Get
        Set(ByVal Value As Double)
            csobj.timetotal = Value
        End Set
        End Property
        Public Property timesolver() As Double
        Get
            Return csobj.timesolver
        End Get
        Set(ByVal Value As Double)
            csobj.timesolver = Value
        End Set
        End Property
        Public Property timecallback() As Double
        Get
            Return csobj.timecallback
        End Get
        Set(ByVal Value As Double)
            csobj.timecallback = Value
        End Set
        End Property
        Public csobj As alglib.mindfreport
    End Class


    Public Sub mindfcreate(ByVal n As Integer, ByVal x() As Double, ByRef state As mindfstate)
        Try
            state = New mindfstate()
            alglib.mindfcreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfcreate(ByVal x() As Double, ByRef state As mindfstate)
        Try
            state = New mindfstate()
            alglib.mindfcreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetbc(ByRef state As mindfstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.mindfsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetlc2dense(ByRef state As mindfstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.mindfsetlc2dense(state.csobj, a, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetlc2dense(ByRef state As mindfstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.mindfsetlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetnlc2(ByRef state As mindfstate, ByVal nl() As Double, ByVal nu() As Double, ByVal nnlc As Integer)
        Try
            alglib.mindfsetnlc2(state.csobj, nl, nu, nnlc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetnlc2(ByRef state As mindfstate, ByVal nl() As Double, ByVal nu() As Double)
        Try
            alglib.mindfsetnlc2(state.csobj, nl, nu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetcondfx(ByRef state As mindfstate, ByVal epsf As Double, ByVal epsx As Double)
        Try
            alglib.mindfsetcondfx(state.csobj, epsf, epsx)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfusetimers(ByRef state As mindfstate, ByVal usetimers As Boolean)
        Try
            alglib.mindfusetimers(state.csobj, usetimers)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetcondf(ByRef state As mindfstate, ByVal epsf As Double)
        Try
            alglib.mindfsetcondf(state.csobj, epsf)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetscale(ByRef state As mindfstate, ByVal s() As Double)
        Try
            alglib.mindfsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetxrep(ByRef state As mindfstate, ByVal needxrep As Boolean)
        Try
            alglib.mindfsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfrequesttermination(ByRef state As mindfstate)
        Try
            alglib.mindfrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetseed(ByRef s As mindfstate, ByVal seedval As Integer)
        Try
            alglib.mindfsetseed(s.csobj, seedval)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetalgogdemo(ByRef state As mindfstate, ByVal epochscnt As Integer, ByVal popsize As Integer)
        Try
            alglib.mindfsetalgogdemo(state.csobj, epochscnt, popsize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetalgogdemo(ByRef state As mindfstate, ByVal epochscnt As Integer)
        Try
            alglib.mindfsetalgogdemo(state.csobj, epochscnt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetgdemoprofilerobust(ByRef state As mindfstate)
        Try
            alglib.mindfsetgdemoprofilerobust(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetgdemoprofilequick(ByRef state As mindfstate)
        Try
            alglib.mindfsetgdemoprofilequick(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetgdemopenalty(ByRef state As mindfstate, ByVal rho1 As Double, ByVal rho2 As Double)
        Try
            alglib.mindfsetgdemopenalty(state.csobj, rho1, rho2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfsetalgogdemofixed(ByRef state As mindfstate, ByVal epochscnt As Integer, ByVal strategy As Integer, ByVal crossoverprob As Double, ByVal differentialweight As Double, ByVal popsize As Integer)
        Try
            alglib.mindfsetalgogdemofixed(state.csobj, epochscnt, strategy, crossoverprob, differentialweight, popsize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mindfiteration(ByRef state As mindfstate) As Boolean
        Try
            mindfiteration = alglib.mindfiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM
    ' 
    ' The  MINDF  optimizer  supports   parallel   model  evaluation  ('callback
    ' parallelism').  This  feature,  which  is  present  in  commercial  ALGLIB
    ' editions, greatly accelerates  optimization  when  using  a  solver  which
    ' issues batch requests, i.e. multiple requests  for  target  values,  which
    ' can be computed independently by different threads.
    ' 
    ' Callback parallelism is  usually   beneficial  when   processing  a  batch
    ' request requires more than several  milliseconds.  It  also  requires  the
    ' solver which issues requests in convenient batches, e.g. the  differential
    ' evolution solver.
    ' 
    ' See ALGLIB Reference Manual, 'Working with commercial version' section for
    ' more information.
    ' 
    '   -- ALGLIB --
    '      Copyright 25.07.2023 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub mindfoptimize(ByVal state As mindfstate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.mindfoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub mindfresults(ByVal state As mindfstate, ByRef x() As Double, ByRef rep As mindfreport)
        Try
            rep = New mindfreport()
            alglib.mindfresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mindfresultsbuf(ByVal state As mindfstate, ByRef x() As Double, ByRef rep As mindfreport)
        Try
            alglib.mindfresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class nlsstate
        Public csobj As alglib.nlsstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Optimization report, filled by NLSResults() function
    '
    'FIELDS:
    '* TerminationType, completion code, which is a sum of a BASIC code and  an
    'ADDITIONAL code.
    '
    '  The following basic codes denote failure:
    '    * -8    optimizer detected NAN/INF  either  in  the  function  itself,
    '            or its Jacobian; recovery was impossible, abnormal termination
    '            reported.
    '    * -3    box constraints are inconsistent
    '
    '  The following basic codes denote success:
    '    *  2    relative step is no more than EpsX.
    '    *  5    MaxIts steps was taken
    '    *  7    stopping conditions are too stringent,
    '            further improvement is impossible
    '    *  8    terminated   by  user  who  called  NLSRequestTermination().
    '            X contains point which was "current accepted" when termination
    '            request was submitted.
    '
    '  Additional codes can be set on success, but not on failure:
    '    * +800  if   during   algorithm   execution   the   solver encountered
    '            NAN/INF values in the target or  constraints  but  managed  to
    '            recover by reducing trust region radius,  the  solver  returns
    '            one of SUCCESS codes but adds +800 to the code.
    '
    '* IterationsCount, contains iterations count
    '* NFunc, number of function calculations
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class nlsreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property nfunc() As Integer
        Get
            Return csobj.nfunc
        End Get
        Set(ByVal Value As Integer)
            csobj.nfunc = Value
        End Set
        End Property
        Public csobj As alglib.nlsreport
    End Class


    Public Sub nlscreatedfo(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByRef state As nlsstate)
        Try
            state = New nlsstate()
            alglib.nlscreatedfo(n, m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlscreatedfo(ByVal m As Integer, ByVal x() As Double, ByRef state As nlsstate)
        Try
            state = New nlsstate()
            alglib.nlscreatedfo(m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetalgo2ps(ByRef state As nlsstate, ByVal nnoisyrestarts As Integer)
        Try
            alglib.nlssetalgo2ps(state.csobj, nnoisyrestarts)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetalgo2ps(ByRef state As nlsstate)
        Try
            alglib.nlssetalgo2ps(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetalgodfolsa(ByRef state As nlsstate, ByVal nnoisyrestarts As Integer)
        Try
            alglib.nlssetalgodfolsa(state.csobj, nnoisyrestarts)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetalgodfolsa(ByRef state As nlsstate)
        Try
            alglib.nlssetalgodfolsa(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetcond(ByRef state As nlsstate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.nlssetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetxrep(ByRef state As nlsstate, ByVal needxrep As Boolean)
        Try
            alglib.nlssetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetscale(ByRef state As nlsstate, ByVal s() As Double)
        Try
            alglib.nlssetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlssetbc(ByRef state As nlsstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.nlssetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function nlsiteration(ByRef state As nlsstate) As Boolean
        Try
            nlsiteration = alglib.nlsiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM
    ' 
    ' The  NLS  optimizer  supports   parallel   model   evaluation   ('callback
    ' parallelism').  This  feature,  which  is  present  in  commercial  ALGLIB
    ' editions, greatly accelerates  optimization  when  using  a  solver  which
    ' issues batch requests, i.e. multiple requests  for  target  values,  which
    ' can be computed independently by different threads.
    ' 
    ' Callback parallelism is  usually   beneficial  when   processing  a  batch
    ' request requires more than several  milliseconds.  It  also  requires  the
    ' solver which issues requests in convenient batches, e.g. 2PS solver.
    ' 
    ' See ALGLIB Reference Manual, 'Working with commercial version' section for
    ' more information.
    ' 
    '   -- ALGLIB --
    '      Copyright 15.10.2023 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub nlsoptimize(ByVal state As nlsstate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.nlsoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub nlsoptimize(ByVal state As nlsstate, ByVal fvec As ndimensional_fvec, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.nlsoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub nlsresults(ByVal state As nlsstate, ByRef x() As Double, ByRef rep As nlsreport)
        Try
            rep = New nlsreport()
            alglib.nlsresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlsresultsbuf(ByVal state As nlsstate, ByRef x() As Double, ByRef rep As nlsreport)
        Try
            alglib.nlsresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlsrestartfrom(ByRef state As nlsstate, ByVal x() As Double)
        Try
            alglib.nlsrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nlsrequesttermination(ByRef state As nlsstate)
        Try
            alglib.nlsrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minbleicstate
        Public csobj As alglib.minbleicstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* IterationsCount           number of iterations
    '* NFEV                      number of gradient evaluations
    '* TerminationType           termination type (see below)
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '  -3    inconsistent constraints. Feasible point is
    '        either nonexistent or too hard to find. Try to
    '        restart optimizer with better initial approximation
    '   1    relative function improvement is no more than EpsF.
    '   2    relative step is no more than EpsX.
    '   4    gradient norm is no more than EpsG
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    terminated by user who called minbleicrequesttermination(). X contains
    '        point which was "current accepted" when  termination  request  was
    '        submitted.
    '
    'ADDITIONAL FIELDS
    '
    'There are additional fields which can be used for debugging:
    '* DebugEqErr                error in the equality constraints (2-norm)
    '* DebugFS                   f, calculated at projection of initial point
    '                            to the feasible set
    '* DebugFF                   f, calculated at the final point
    '* DebugDX                   |X_start-X_final|
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minbleicreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property varidx() As Integer
        Get
            Return csobj.varidx
        End Get
        Set(ByVal Value As Integer)
            csobj.varidx = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property debugeqerr() As Double
        Get
            Return csobj.debugeqerr
        End Get
        Set(ByVal Value As Double)
            csobj.debugeqerr = Value
        End Set
        End Property
        Public Property debugfs() As Double
        Get
            Return csobj.debugfs
        End Get
        Set(ByVal Value As Double)
            csobj.debugfs = Value
        End Set
        End Property
        Public Property debugff() As Double
        Get
            Return csobj.debugff
        End Get
        Set(ByVal Value As Double)
            csobj.debugff = Value
        End Set
        End Property
        Public Property debugdx() As Double
        Get
            Return csobj.debugdx
        End Get
        Set(ByVal Value As Double)
            csobj.debugdx = Value
        End Set
        End Property
        Public Property debugfeasqpits() As Integer
        Get
            Return csobj.debugfeasqpits
        End Get
        Set(ByVal Value As Integer)
            csobj.debugfeasqpits = Value
        End Set
        End Property
        Public Property debugfeasgpaits() As Integer
        Get
            Return csobj.debugfeasgpaits
        End Get
        Set(ByVal Value As Integer)
            csobj.debugfeasgpaits = Value
        End Set
        End Property
        Public Property inneriterationscount() As Integer
        Get
            Return csobj.inneriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriterationscount = Value
        End Set
        End Property
        Public Property outeriterationscount() As Integer
        Get
            Return csobj.outeriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriterationscount = Value
        End Set
        End Property
        Public csobj As alglib.minbleicreport
    End Class


    Public Sub minbleiccreate(ByVal n As Integer, ByVal x() As Double, ByRef state As minbleicstate)
        Try
            state = New minbleicstate()
            alglib.minbleiccreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleiccreate(ByVal x() As Double, ByRef state As minbleicstate)
        Try
            state = New minbleicstate()
            alglib.minbleiccreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleiccreatef(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minbleicstate)
        Try
            state = New minbleicstate()
            alglib.minbleiccreatef(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleiccreatef(ByVal x() As Double, ByVal diffstep As Double, ByRef state As minbleicstate)
        Try
            state = New minbleicstate()
            alglib.minbleiccreatef(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetbc(ByRef state As minbleicstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minbleicsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetlc(ByRef state As minbleicstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minbleicsetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetlc(ByRef state As minbleicstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minbleicsetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetcond(ByRef state As minbleicstate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minbleicsetcond(state.csobj, epsg, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetscale(ByRef state As minbleicstate, ByVal s() As Double)
        Try
            alglib.minbleicsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetprecdefault(ByRef state As minbleicstate)
        Try
            alglib.minbleicsetprecdefault(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetprecdiag(ByRef state As minbleicstate, ByVal d() As Double)
        Try
            alglib.minbleicsetprecdiag(state.csobj, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetprecscale(ByRef state As minbleicstate)
        Try
            alglib.minbleicsetprecscale(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetxrep(ByRef state As minbleicstate, ByVal needxrep As Boolean)
        Try
            alglib.minbleicsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetstpmax(ByRef state As minbleicstate, ByVal stpmax As Double)
        Try
            alglib.minbleicsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minbleiciteration(ByRef state As minbleicstate) As Boolean
        Try
            minbleiciteration = alglib.minbleiciteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied gradient,  and one which uses function value
    '    only  and  numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    (either  MinBLEICCreate() for analytical gradient or  MinBLEICCreateF()
    '    for numerical differentiation) you should choose appropriate variant of
    '    MinBLEICOptimize() - one  which  accepts  function  AND gradient or one
    '    which accepts function ONLY.
    ' 
    '    Be careful to choose variant of MinBLEICOptimize() which corresponds to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to MinBLEICOptimize()  and specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO MinBLEICOptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinBLEICCreateF() |     work                FAIL
    '    MinBLEICCreate()  |     FAIL                work
    ' 
    '    Here "FAIL" denotes inappropriate combinations  of  optimizer  creation
    '    function  and  MinBLEICOptimize()  version.   Attemps   to   use   such
    '    combination (for  example,  to  create optimizer with MinBLEICCreateF()
    '    and  to  pass  gradient information to MinBLEICOptimize()) will lead to
    '    exception being thrown. Either  you  did  not pass gradient when it WAS
    '    needed or you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 28.11.2010 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minbleicoptimize(ByVal state As minbleicstate, ByVal func As ndimensional_func, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minbleicoptimize(state.csobj, New alglib.ndimensional_func(AddressOf func.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minbleicoptimize(ByVal state As minbleicstate, ByVal grad As ndimensional_grad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minbleicoptimize(state.csobj, New alglib.ndimensional_grad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minbleicoptguardgradient(ByRef state As minbleicstate, ByVal teststep As Double)
        Try
            alglib.minbleicoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicoptguardsmoothness(ByRef state As minbleicstate, ByVal level As Integer)
        Try
            alglib.minbleicoptguardsmoothness(state.csobj, level)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicoptguardsmoothness(ByRef state As minbleicstate)
        Try
            alglib.minbleicoptguardsmoothness(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicoptguardresults(ByRef state As minbleicstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.minbleicoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicoptguardnonc1test0results(ByVal state As minbleicstate, ByRef strrep As optguardnonc1test0report, ByRef lngrep As optguardnonc1test0report)
        Try
            strrep = New optguardnonc1test0report()
            lngrep = New optguardnonc1test0report()
            alglib.minbleicoptguardnonc1test0results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicoptguardnonc1test1results(ByRef state As minbleicstate, ByRef strrep As optguardnonc1test1report, ByRef lngrep As optguardnonc1test1report)
        Try
            strrep = New optguardnonc1test1report()
            lngrep = New optguardnonc1test1report()
            alglib.minbleicoptguardnonc1test1results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicresults(ByVal state As minbleicstate, ByRef x() As Double, ByRef rep As minbleicreport)
        Try
            rep = New minbleicreport()
            alglib.minbleicresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicresultsbuf(ByVal state As minbleicstate, ByRef x() As Double, ByRef rep As minbleicreport)
        Try
            alglib.minbleicresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicrestartfrom(ByRef state As minbleicstate, ByVal x() As Double)
        Try
            alglib.minbleicrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicrequesttermination(ByRef state As minbleicstate)
        Try
            alglib.minbleicrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minnlcstate
        Public csobj As alglib.minnlcstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'These fields store optimization report:
    '* f                         objective value at the solution
    '* iterationscount           total number of inner iterations
    '* nfev                      number of gradient evaluations
    '* terminationtype           termination type (see below)
    '
    'Scaled constraint violations are reported:
    '* bcerr                     maximum violation of the box constraints
    '* bcidx                     index of the most violated box  constraint (or
    '                            -1, if all box constraints  are  satisfied  or
    '                            there is no box constraint)
    '* lcerr                     maximum violation of the  linear  constraints,
    '                            computed as maximum  scaled  distance  between
    '                            final point and constraint boundary.
    '* lcidx                     index of the most violated  linear  constraint
    '                            (or -1, if all constraints  are  satisfied  or
    '                            there is no general linear constraints)
    '* nlcerr                    maximum violation of the nonlinear constraints
    '* nlcidx                    index of the most violated nonlinear constraint
    '                            (or -1, if all constraints  are  satisfied  or
    '                            there is no nonlinear constraints)
    '
    'Violations of box constraints are scaled on per-component basis  according
    'to  the  scale  vector s[] as specified by minnlcsetscale(). Violations of
    'the general linear  constraints  are  also  computed  using  user-supplied
    'variable scaling. Violations of nonlinear constraints are computed "as is"
    '
    'LAGRANGE COEFFICIENTS
    '
    'SQP solver (one activated by setalgosqp()/setalgosqpbfgs(), but  not  with
    'legacy functions) sets the following fields (other solvers  fill  them  by
    'zeros):
    '
    '* lagbc[]                   array[N],   Lagrange   multipliers   for   box
    '                            constraints. IMPORTANT: COEFFICIENTS FOR FIXED
    '                            VARIABLES ARE SET TO ZERO. See  below  for  an
    '                            explanation.
    '                            This  parameter   stores    the  same  results
    '                            independently of whether analytic gradient  is
    '                            provided or numerical differentiation is used.
    '
    '* lagbcnz[]                 array[N],   Lagrange   multipliers   for   box
    '                            constraints, behaves differently depending  on
    '                            whether  analytic  gradient  is   provided  or
    '                            numerical differentiation is used:
    '                            * for analytic Jacobian,   lagbcnz[]  contains
    '                              correct   coefficients   for  all  kinds  of
    '                              variables - fixed or not.
    '                            * for numerical Jacobian, it is  the  same  as
    '                              lagbc[], i.e. components corresponding  to
    '                              fixed vars are zero.
    '                            See below for an explanation.
    '
    '* laglc[]                   array[Mlin], coeffs for linear constraints
    '
    '* lagnlc[]                  array[Mnlc], coeffs for nonlinear constraints
    '
    '
    'Positive Lagrange coefficient means that constraint is at its upper bound.
    'Negative coefficient means that constraint is at its lower  bound.  It  is
    'expected that at the solution the dual feasibility condition holds:
    '
    '    grad + SUM(Ei*LagBC[i],i=0..n-1) +
    '        SUM(Ai*LagLC[i],i=0..mlin-1) +
    '        SUM(Ni*LagNLC[i],i=0..mnlc-1) ~ 0
    '
    '    (except for fixed variables which are handled specially)
    '
    'where
    '* grad is a gradient at the solution
    '* Ei is a vector with 1.0 at position I and 0 in other positions
    '* Ai is an I-th row of linear constraint matrix
    '* Ni is an gradient of I-th nonlinear constraint
    '
    'Fixed variables have two sets of Lagrange multipliers  for  the  following
    'reasons:
    '* analytic gradient and numerical gradient behave  differently  for  fixed
    '  vars. Numerical differentiation does not violate box  constraints,  thus
    '  gradient components corresponding to fixed vars are zero because we have
    '  no way of  differentiating  for  these  vars   without   violating   box
    '  constraints.
    '  Contrary to that, analytic gradient usually returns correct values  even
    '  for fixed vars.
    '* ideally,  we  would  like  numerical  gradient  to  be an almost perfect
    '  replacement for an analytic one.  Thus,  we  need  Lagrange  multipliers
    '  which do not change when we change the gradient type.
    '* on the other hand, we do not want to  lose  the  possibility  of  having
    '  a full set of Lagrange multipliers for problems with analytic gradient.
    '  Thus, there is a special field lagbcnz[] whose contents depends  on  the
    '  information available to us.
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be either FAILURE
    'code, SUCCESS code, or SUCCESS code + ADDITIONAL code.  The  latter option
    'is used for more detailed reporting.
    '
    '=== FAILURE CODE ===
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient, recovery was impossible.  Abnormal  termination
    '        signaled.
    '  -3    box  constraints  are  infeasible.  Note: infeasibility of non-box
    '        constraints does NOT trigger emergency  completion;  you  have  to
    '        examine  bcerr/lcerr/nlcerr   to  detect   possibly   inconsistent
    '        constraints.
    '
    '=== SUCCESS CODE ===
    '   2    relative step is no more than EpsX.
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    user requested algorithm termination via minnlcrequesttermination(),
    '        last accepted point is returned
    '
    '=== ADDITIONAL CODES ===
    '* +800      if   during   algorithm   execution   the   solver encountered
    '            NAN/INF values in the target or  constraints  but  managed  to
    '            recover by reducing trust region radius,  the  solver  returns
    '            one of SUCCESS codes but adds +800 to the code.
    '
    'Other fields of this structure are not documented and should not be used!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minnlcreport
        Public Property f() As Double
        Get
            Return csobj.f
        End Get
        Set(ByVal Value As Double)
            csobj.f = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property bcerr() As Double
        Get
            Return csobj.bcerr
        End Get
        Set(ByVal Value As Double)
            csobj.bcerr = Value
        End Set
        End Property
        Public Property bcidx() As Integer
        Get
            Return csobj.bcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.bcidx = Value
        End Set
        End Property
        Public Property lcerr() As Double
        Get
            Return csobj.lcerr
        End Get
        Set(ByVal Value As Double)
            csobj.lcerr = Value
        End Set
        End Property
        Public Property lcidx() As Integer
        Get
            Return csobj.lcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.lcidx = Value
        End Set
        End Property
        Public Property nlcerr() As Double
        Get
            Return csobj.nlcerr
        End Get
        Set(ByVal Value As Double)
            csobj.nlcerr = Value
        End Set
        End Property
        Public Property nlcidx() As Integer
        Get
            Return csobj.nlcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.nlcidx = Value
        End Set
        End Property
        Public Property lagbc() As Double()
        Get
            Return csobj.lagbc
        End Get
        Set(ByVal Value As Double())
            csobj.lagbc = Value
        End Set
        End Property
        Public Property lagbcnz() As Double()
        Get
            Return csobj.lagbcnz
        End Get
        Set(ByVal Value As Double())
            csobj.lagbcnz = Value
        End Set
        End Property
        Public Property laglc() As Double()
        Get
            Return csobj.laglc
        End Get
        Set(ByVal Value As Double())
            csobj.laglc = Value
        End Set
        End Property
        Public Property lagnlc() As Double()
        Get
            Return csobj.lagnlc
        End Get
        Set(ByVal Value As Double())
            csobj.lagnlc = Value
        End Set
        End Property
        Public Property dbgphase0its() As Integer
        Get
            Return csobj.dbgphase0its
        End Get
        Set(ByVal Value As Integer)
            csobj.dbgphase0its = Value
        End Set
        End Property
        Public csobj As alglib.minnlcreport
    End Class


    Public Sub minnlccreate(ByVal n As Integer, ByVal x() As Double, ByRef state As minnlcstate)
        Try
            state = New minnlcstate()
            alglib.minnlccreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreate(ByVal x() As Double, ByRef state As minnlcstate)
        Try
            state = New minnlcstate()
            alglib.minnlccreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatebuf(ByVal n As Integer, ByVal x() As Double, ByRef state As minnlcstate)
        Try
            alglib.minnlccreatebuf(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatebuf(ByVal x() As Double, ByRef state As minnlcstate)
        Try
            alglib.minnlccreatebuf(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatef(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnlcstate)
        Try
            state = New minnlcstate()
            alglib.minnlccreatef(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatef(ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnlcstate)
        Try
            state = New minnlcstate()
            alglib.minnlccreatef(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatefbuf(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnlcstate)
        Try
            alglib.minnlccreatefbuf(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlccreatefbuf(ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnlcstate)
        Try
            alglib.minnlccreatefbuf(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetbc(ByRef state As minnlcstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minnlcsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc(ByRef state As minnlcstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minnlcsetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc(ByRef state As minnlcstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minnlcsetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc2dense(ByRef state As minnlcstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minnlcsetlc2dense(state.csobj, a, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc2dense(ByRef state As minnlcstate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minnlcsetlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc2(ByRef state As minnlcstate, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minnlcsetlc2(state.csobj, a.csobj, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetlc2mixed(ByRef state As minnlcstate, ByVal sparsea As sparsematrix, ByVal ksparse As Integer, ByVal densea(,) As Double, ByVal kdense As Integer, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minnlcsetlc2mixed(state.csobj, sparsea.csobj, ksparse, densea, kdense, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcaddlc2dense(ByRef state As minnlcstate, ByVal a() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minnlcaddlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcaddlc2(ByRef state As minnlcstate, ByVal idxa() As Integer, ByVal vala() As Double, ByVal nnz As Integer, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minnlcaddlc2(state.csobj, idxa, vala, nnz, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcaddlc2sparsefromdense(ByRef state As minnlcstate, ByVal da() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minnlcaddlc2sparsefromdense(state.csobj, da, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetnlc(ByRef state As minnlcstate, ByVal nlec As Integer, ByVal nlic As Integer)
        Try
            alglib.minnlcsetnlc(state.csobj, nlec, nlic)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetnlc2(ByRef state As minnlcstate, ByVal nl() As Double, ByVal nu() As Double, ByVal nnlc As Integer)
        Try
            alglib.minnlcsetnlc2(state.csobj, nl, nu, nnlc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetnlc2(ByRef state As minnlcstate, ByVal nl() As Double, ByVal nu() As Double)
        Try
            alglib.minnlcsetnlc2(state.csobj, nl, nu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetnumdiff(ByRef state As minnlcstate, ByVal formulatype As Integer)
        Try
            alglib.minnlcsetnumdiff(state.csobj, formulatype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetcond(ByRef state As minnlcstate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minnlcsetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetcond3(ByRef state As minnlcstate, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minnlcsetcond3(state.csobj, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetscale(ByRef state As minnlcstate, ByVal s() As Double)
        Try
            alglib.minnlcsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetstpmax(ByRef state As minnlcstate, ByVal stpmax As Double)
        Try
            alglib.minnlcsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgoaul2(ByRef state As minnlcstate, ByVal maxouterits As Integer)
        Try
            alglib.minnlcsetalgoaul2(state.csobj, maxouterits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgosl1qp(ByRef state As minnlcstate)
        Try
            alglib.minnlcsetalgosl1qp(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgosl1qpbfgs(ByRef state As minnlcstate)
        Try
            alglib.minnlcsetalgosl1qpbfgs(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgosqp(ByRef state As minnlcstate)
        Try
            alglib.minnlcsetalgosqp(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgoorbit(ByRef state As minnlcstate, ByVal rad0 As Double, ByVal maxnfev As Integer)
        Try
            alglib.minnlcsetalgoorbit(state.csobj, rad0, maxnfev)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetalgosqpbfgs(ByRef state As minnlcstate)
        Try
            alglib.minnlcsetalgosqpbfgs(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcsetxrep(ByRef state As minnlcstate, ByVal needxrep As Boolean)
        Try
            alglib.minnlcsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minnlciteration(ByRef state As minnlcstate) As Boolean
        Try
            minnlciteration = alglib.minnlciteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM
    ' 
    ' The MINNLC optimizer supports parallel parallel  numerical differentiation
    ' ('callback parallelism'). This feature, which  is  present  in  commercial
    ' ALGLIB  editions,  greatly   accelerates   optimization   with   numerical
    ' differentiation of an expensive target functions.
    ' 
    ' Callback parallelism is usually  beneficial  when  computing  a  numerical
    ' gradient requires more than several  milliseconds.  In this case  the  job
    ' of computing individual gradient components can be split between  multiple
    ' threads. Even inexpensive targets can benefit  from  parallelism,  if  you
    ' have many variables.
    ' 
    ' ALGLIB Reference Manual, 'Working with commercial  version' section, tells
    ' how to activate callback parallelism for your programming language.
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied Jacobian, and one which uses  only  function
    '    vector and numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    you should choose appropriate variant of MinNLCOptimize() -  one  which
    '    accepts function AND Jacobian or one which accepts ONLY function.
    ' 
    '    Be careful to choose variant of MinNLCOptimize()  which  corresponds to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to MinNLCOptimize()   and  specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO MinNLCOptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinNLCCreateF()   |     works               FAILS
    '    MinNLCCreate()    |     FAILS               works
    ' 
    '    Here "FAILS" denotes inappropriate combinations  of  optimizer creation
    '    function  and  MinNLCOptimize()  version.   Attemps   to    use    such
    '    combination will lead to exception. Either  you  did  not pass gradient
    '    when it WAS needed or you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 06.06.2014 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minnlcoptimize(ByVal state As minnlcstate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minnlcoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minnlcoptimize(ByVal state As minnlcstate, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minnlcoptimize(state.csobj, New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minnlcoptimize(ByVal state As minnlcstate, ByVal sjac As ndimensional_sjac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing
        Dim sjac_connector As SJacConverter =  New SJacConverter(sjac)

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minnlcoptimize(state.csobj, New alglib.ndimensional_sjac(AddressOf sjac_connector.CsCbk), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minnlcoptguardgradient(ByRef state As minnlcstate, ByVal teststep As Double)
        Try
            alglib.minnlcoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcoptguardsmoothness(ByRef state As minnlcstate, ByVal level As Integer)
        Try
            alglib.minnlcoptguardsmoothness(state.csobj, level)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcoptguardsmoothness(ByRef state As minnlcstate)
        Try
            alglib.minnlcoptguardsmoothness(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcoptguardresults(ByRef state As minnlcstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.minnlcoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcoptguardnonc1test0results(ByVal state As minnlcstate, ByRef strrep As optguardnonc1test0report, ByRef lngrep As optguardnonc1test0report)
        Try
            strrep = New optguardnonc1test0report()
            lngrep = New optguardnonc1test0report()
            alglib.minnlcoptguardnonc1test0results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcoptguardnonc1test1results(ByRef state As minnlcstate, ByRef strrep As optguardnonc1test1report, ByRef lngrep As optguardnonc1test1report)
        Try
            strrep = New optguardnonc1test1report()
            lngrep = New optguardnonc1test1report()
            alglib.minnlcoptguardnonc1test1results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcresults(ByVal state As minnlcstate, ByRef x() As Double, ByRef rep As minnlcreport)
        Try
            rep = New minnlcreport()
            alglib.minnlcresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcresultsbuf(ByVal state As minnlcstate, ByRef x() As Double, ByRef rep As minnlcreport)
        Try
            alglib.minnlcresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcrequesttermination(ByRef state As minnlcstate)
        Try
            alglib.minnlcrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnlcrestartfrom(ByRef state As minnlcstate, ByVal x() As Double)
        Try
            alglib.minnlcrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class minmostate
        Public csobj As alglib.minmostate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'These fields store optimization report:
    '* inneriterationscount      total number of inner iterations
    '* outeriterationscount      number of internal optimization sessions performed
    '* nfev                      number of gradient evaluations
    '* terminationtype           termination type (see below)
    '
    'Scaled constraint violations (maximum over all Pareto points) are reported:
    '* bcerr                     maximum violation of the box constraints
    '* bcidx                     index of the most violated box  constraint (or
    '                            -1, if all box constraints  are  satisfied  or
    '                            there are no box constraint)
    '* lcerr                     maximum violation of the  linear  constraints,
    '                            computed as maximum  scaled  distance  between
    '                            final point and constraint boundary.
    '* lcidx                     index of the most violated  linear  constraint
    '                            (or -1, if all constraints  are  satisfied  or
    '                            there are no general linear constraints)
    '* nlcerr                    maximum violation of the nonlinear constraints
    '* nlcidx                    index of the most violated nonlinear constraint
    '                            (or -1, if all constraints  are  satisfied  or
    '                            there are no nonlinear constraints)
    '
    'Violations  of  the  box  constraints  are  scaled  on per-component basis
    'according to  the  scale  vector s[]  as specified by the minmosetscale().
    'Violations of the general linear  constraints  are  also   computed  using
    'user-supplied variable scaling. Violations of  the  nonlinear  constraints
    'are computed "as is"
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be either:
    '
    '=== FAILURE CODE ===
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signaled.
    '  -3    box  constraints  are  infeasible.  Note: infeasibility of non-box
    '        constraints does NOT trigger emergency  completion;  you  have  to
    '        examine  bcerr/lcerr/nlcerr   to  detect   possibly   inconsistent
    '        constraints.
    '
    '=== SUCCESS CODE ===
    '   2    relative step is no more than EpsX.
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '
    'NOTE: The solver internally performs many optimization sessions:  one  for
    '      each Pareto point, and some  amount  of  preparatory  optimizations.
    '      Different optimization  sessions  may  return  different  completion
    '      codes. If at least one of internal optimizations failed, its failure
    '      code is returned. If none of them failed, the most frequent code  is
    '      returned.
    '
    'Other fields of this structure are not documented and should not be used!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minmoreport
        Public Property inneriterationscount() As Integer
        Get
            Return csobj.inneriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriterationscount = Value
        End Set
        End Property
        Public Property outeriterationscount() As Integer
        Get
            Return csobj.outeriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property bcerr() As Double
        Get
            Return csobj.bcerr
        End Get
        Set(ByVal Value As Double)
            csobj.bcerr = Value
        End Set
        End Property
        Public Property bcidx() As Integer
        Get
            Return csobj.bcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.bcidx = Value
        End Set
        End Property
        Public Property lcerr() As Double
        Get
            Return csobj.lcerr
        End Get
        Set(ByVal Value As Double)
            csobj.lcerr = Value
        End Set
        End Property
        Public Property lcidx() As Integer
        Get
            Return csobj.lcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.lcidx = Value
        End Set
        End Property
        Public Property nlcerr() As Double
        Get
            Return csobj.nlcerr
        End Get
        Set(ByVal Value As Double)
            csobj.nlcerr = Value
        End Set
        End Property
        Public Property nlcidx() As Integer
        Get
            Return csobj.nlcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.nlcidx = Value
        End Set
        End Property
        Public csobj As alglib.minmoreport
    End Class


    Public Sub minmocreate(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByRef state As minmostate)
        Try
            state = New minmostate()
            alglib.minmocreate(n, m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmocreate(ByVal m As Integer, ByVal x() As Double, ByRef state As minmostate)
        Try
            state = New minmostate()
            alglib.minmocreate(m, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmocreatef(ByVal n As Integer, ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minmostate)
        Try
            state = New minmostate()
            alglib.minmocreatef(n, m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmocreatef(ByVal m As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minmostate)
        Try
            state = New minmostate()
            alglib.minmocreatef(m, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetalgonbi(ByRef state As minmostate, ByVal frontsize As Integer, ByVal polishsolutions As Boolean)
        Try
            alglib.minmosetalgonbi(state.csobj, frontsize, polishsolutions)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetbc(ByRef state As minmostate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minmosetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetlc2dense(ByRef state As minmostate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minmosetlc2dense(state.csobj, a, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetlc2dense(ByRef state As minmostate, ByVal a(,) As Double, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minmosetlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetlc2(ByRef state As minmostate, ByVal a As sparsematrix, ByVal al() As Double, ByVal au() As Double, ByVal k As Integer)
        Try
            alglib.minmosetlc2(state.csobj, a.csobj, al, au, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetlc2mixed(ByRef state As minmostate, ByVal sparsea As sparsematrix, ByVal ksparse As Integer, ByVal densea(,) As Double, ByVal kdense As Integer, ByVal al() As Double, ByVal au() As Double)
        Try
            alglib.minmosetlc2mixed(state.csobj, sparsea.csobj, ksparse, densea, kdense, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmoaddlc2dense(ByRef state As minmostate, ByVal a() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minmoaddlc2dense(state.csobj, a, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmoaddlc2(ByRef state As minmostate, ByVal idxa() As Integer, ByVal vala() As Double, ByVal nnz As Integer, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minmoaddlc2(state.csobj, idxa, vala, nnz, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmoaddlc2sparsefromdense(ByRef state As minmostate, ByVal da() As Double, ByVal al As Double, ByVal au As Double)
        Try
            alglib.minmoaddlc2sparsefromdense(state.csobj, da, al, au)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetnlc2(ByRef state As minmostate, ByVal nl() As Double, ByVal nu() As Double, ByVal nnlc As Integer)
        Try
            alglib.minmosetnlc2(state.csobj, nl, nu, nnlc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetcond(ByRef state As minmostate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minmosetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetscale(ByRef state As minmostate, ByVal s() As Double)
        Try
            alglib.minmosetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmosetxrep(ByRef state As minmostate, ByVal needxrep As Boolean)
        Try
            alglib.minmosetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minmoiteration(ByRef state As minmostate) As Boolean
        Try
            minmoiteration = alglib.minmoiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied Jacobian, and one which uses  only  function
    '    vector and numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    you should choose appropriate variant of MinMOOptimize() -  one   which
    '    needs function vector AND Jacobian or one which needs ONLY function.
    ' 
    '    Be careful to choose variant of MinMOOptimize()  which  corresponds  to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to MinMOOptimize()   and   specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO MinMOOptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinMOCreateF()    |     works               FAILS
    '    MinMOCreate()     |     FAILS               works
    ' 
    '    Here "FAILS" denotes inappropriate combinations  of  optimizer creation
    '    function  and  MinMOOptimize()  version.   Attemps   to    use     such
    '    combination will lead to exception. Either  you  did  not pass gradient
    '    when it WAS needed or you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 01.03.2023 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minmooptimize(ByVal state As minmostate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minmooptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minmooptimize(ByVal state As minmostate, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minmooptimize(state.csobj, New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minmoresults(ByVal state As minmostate, ByRef paretofront(,) As Double, ByRef frontsize As Integer, ByRef rep As minmoreport)
        Try
            rep = New minmoreport()
            alglib.minmoresults(state.csobj, paretofront, frontsize, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmorequesttermination(ByRef state As minmostate)
        Try
            alglib.minmorequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minmorestartfrom(ByRef state As minmostate, ByVal x() As Double)
        Try
            alglib.minmorestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minnsstate
        Public csobj As alglib.minnsstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* IterationsCount           total number of inner iterations
    '* NFEV                      number of gradient evaluations
    '* TerminationType           termination type (see below)
    '* CErr                      maximum violation of all types of constraints
    '* LCErr                     maximum violation of linear constraints
    '* NLCErr                    maximum violation of nonlinear constraints
    '
    'TERMINATION CODES
    '
    'TerminationType field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '  -3    box constraints are inconsistent
    '  -1    inconsistent parameters were passed:
    '        * penalty parameter for minnssetalgoags() is zero,
    '          but we have nonlinear constraints set by minnssetnlc()
    '   2    sampling radius decreased below epsx
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    User requested termination via MinNSRequestTermination()
    '
    'Other fields of this structure are not documented and should not be used!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minnsreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property cerr() As Double
        Get
            Return csobj.cerr
        End Get
        Set(ByVal Value As Double)
            csobj.cerr = Value
        End Set
        End Property
        Public Property lcerr() As Double
        Get
            Return csobj.lcerr
        End Get
        Set(ByVal Value As Double)
            csobj.lcerr = Value
        End Set
        End Property
        Public Property nlcerr() As Double
        Get
            Return csobj.nlcerr
        End Get
        Set(ByVal Value As Double)
            csobj.nlcerr = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property varidx() As Integer
        Get
            Return csobj.varidx
        End Get
        Set(ByVal Value As Integer)
            csobj.varidx = Value
        End Set
        End Property
        Public Property funcidx() As Integer
        Get
            Return csobj.funcidx
        End Get
        Set(ByVal Value As Integer)
            csobj.funcidx = Value
        End Set
        End Property
        Public csobj As alglib.minnsreport
    End Class


    Public Sub minnscreate(ByVal n As Integer, ByVal x() As Double, ByRef state As minnsstate)
        Try
            state = New minnsstate()
            alglib.minnscreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnscreate(ByVal x() As Double, ByRef state As minnsstate)
        Try
            state = New minnsstate()
            alglib.minnscreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnscreatef(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnsstate)
        Try
            state = New minnsstate()
            alglib.minnscreatef(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnscreatef(ByVal x() As Double, ByVal diffstep As Double, ByRef state As minnsstate)
        Try
            state = New minnsstate()
            alglib.minnscreatef(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetbc(ByRef state As minnsstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minnssetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetlc(ByRef state As minnsstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.minnssetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetlc(ByRef state As minnsstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.minnssetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetnlc(ByRef state As minnsstate, ByVal nlec As Integer, ByVal nlic As Integer)
        Try
            alglib.minnssetnlc(state.csobj, nlec, nlic)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetcond(ByRef state As minnsstate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minnssetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetscale(ByRef state As minnsstate, ByVal s() As Double)
        Try
            alglib.minnssetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetalgoags(ByRef state As minnsstate, ByVal radius As Double, ByVal penalty As Double)
        Try
            alglib.minnssetalgoags(state.csobj, radius, penalty)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnssetxrep(ByRef state As minnsstate, ByVal needxrep As Boolean)
        Try
            alglib.minnssetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnsrequesttermination(ByRef state As minnsstate)
        Try
            alglib.minnsrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minnsiteration(ByRef state As minnsstate) As Boolean
        Try
            minnsiteration = alglib.minnsiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     fvec    -   callback which calculates function vector fi[]
    '                 at given point x
    '     jac     -   callback which calculates function vector fi[]
    '                 and Jacobian jac at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied Jacobian, and one which uses  only  function
    '    vector and numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    you should choose appropriate variant of  minnsoptimize() -  one  which
    '    accepts function AND Jacobian or one which accepts ONLY function.
    ' 
    '    Be careful to choose variant of minnsoptimize()  which  corresponds  to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to minnsoptimize()    and  specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO minnsoptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    minnscreatef()    |     works               FAILS
    '    minnscreate()     |     FAILS               works
    ' 
    '    Here "FAILS" denotes inappropriate combinations  of  optimizer creation
    '    function  and  minnsoptimize()  version.   Attemps   to    use     such
    '    combination will lead to exception. Either  you  did  not pass gradient
    '    when it WAS needed or you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 18.05.2015 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minnsoptimize(ByVal state As minnsstate, ByVal fvec As ndimensional_fvec, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minnsoptimize(state.csobj, New alglib.ndimensional_fvec(AddressOf fvec.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minnsoptimize(ByVal state As minnsstate, ByVal jac As ndimensional_jac, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minnsoptimize(state.csobj, New alglib.ndimensional_jac(AddressOf jac.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minnsresults(ByVal state As minnsstate, ByRef x() As Double, ByRef rep As minnsreport)
        Try
            rep = New minnsreport()
            alglib.minnsresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnsresultsbuf(ByVal state As minnsstate, ByRef x() As Double, ByRef rep As minnsreport)
        Try
            alglib.minnsresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minnsrestartfrom(ByRef state As minnsstate, ByVal x() As Double)
        Try
            alglib.minnsrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minasastate
        Public csobj As alglib.minasastate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minasareport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property activeconstraints() As Integer
        Get
            Return csobj.activeconstraints
        End Get
        Set(ByVal Value As Integer)
            csobj.activeconstraints = Value
        End Set
        End Property
        Public csobj As alglib.minasareport
    End Class


    Public Sub minlbfgssetdefaultpreconditioner(ByRef state As minlbfgsstate)
        Try
            alglib.minlbfgssetdefaultpreconditioner(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minlbfgssetcholeskypreconditioner(ByRef state As minlbfgsstate, ByVal p(,) As Double, ByVal isupper As Boolean)
        Try
            alglib.minlbfgssetcholeskypreconditioner(state.csobj, p, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetbarrierwidth(ByRef state As minbleicstate, ByVal mu As Double)
        Try
            alglib.minbleicsetbarrierwidth(state.csobj, mu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbleicsetbarrierdecay(ByRef state As minbleicstate, ByVal mudecay As Double)
        Try
            alglib.minbleicsetbarrierdecay(state.csobj, mudecay)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasacreate(ByVal n As Integer, ByVal x() As Double, ByVal bndl() As Double, ByVal bndu() As Double, ByRef state As minasastate)
        Try
            state = New minasastate()
            alglib.minasacreate(n, x, bndl, bndu, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasacreate(ByVal x() As Double, ByVal bndl() As Double, ByVal bndu() As Double, ByRef state As minasastate)
        Try
            state = New minasastate()
            alglib.minasacreate(x, bndl, bndu, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasasetcond(ByRef state As minasastate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minasasetcond(state.csobj, epsg, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasasetxrep(ByRef state As minasastate, ByVal needxrep As Boolean)
        Try
            alglib.minasasetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasasetalgorithm(ByRef state As minasastate, ByVal algotype As Integer)
        Try
            alglib.minasasetalgorithm(state.csobj, algotype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasasetstpmax(ByRef state As minasastate, ByVal stpmax As Double)
        Try
            alglib.minasasetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minasaiteration(ByRef state As minasastate) As Boolean
        Try
            minasaiteration = alglib.minasaiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    '   -- ALGLIB --
    '      Copyright 20.03.2009 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minasaoptimize(ByVal state As minasastate, ByVal grad As ndimensional_grad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minasaoptimize(state.csobj, New alglib.ndimensional_grad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minasaresults(ByVal state As minasastate, ByRef x() As Double, ByRef rep As minasareport)
        Try
            rep = New minasareport()
            alglib.minasaresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasaresultsbuf(ByVal state As minasastate, ByRef x() As Double, ByRef rep As minasareport)
        Try
            alglib.minasaresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minasarestartfrom(ByRef state As minasastate, ByVal x() As Double, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minasarestartfrom(state.csobj, x, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class minbcstate
        Public csobj As alglib.minbcstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure stores optimization report:
    '* iterationscount           number of iterations
    '* nfev                      number of gradient evaluations
    '* terminationtype           termination type (see below)
    '
    'TERMINATION CODES
    '
    'terminationtype field contains completion code, which can be:
    '  -8    internal integrity control detected  infinite  or  NAN  values  in
    '        function/gradient. Abnormal termination signalled.
    '  -3    inconsistent constraints.
    '   1    relative function improvement is no more than EpsF.
    '   2    relative step is no more than EpsX.
    '   4    gradient norm is no more than EpsG
    '   5    MaxIts steps was taken
    '   7    stopping conditions are too stringent,
    '        further improvement is impossible,
    '        X contains best point found so far.
    '   8    terminated by user who called minbcrequesttermination(). X contains
    '        point which was "current accepted" when  termination  request  was
    '        submitted.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class minbcreport
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property varidx() As Integer
        Get
            Return csobj.varidx
        End Get
        Set(ByVal Value As Integer)
            csobj.varidx = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.minbcreport
    End Class


    Public Sub minbccreate(ByVal n As Integer, ByVal x() As Double, ByRef state As minbcstate)
        Try
            state = New minbcstate()
            alglib.minbccreate(n, x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbccreate(ByVal x() As Double, ByRef state As minbcstate)
        Try
            state = New minbcstate()
            alglib.minbccreate(x, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbccreatef(ByVal n As Integer, ByVal x() As Double, ByVal diffstep As Double, ByRef state As minbcstate)
        Try
            state = New minbcstate()
            alglib.minbccreatef(n, x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbccreatef(ByVal x() As Double, ByVal diffstep As Double, ByRef state As minbcstate)
        Try
            state = New minbcstate()
            alglib.minbccreatef(x, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetbc(ByRef state As minbcstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.minbcsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetcond(ByRef state As minbcstate, ByVal epsg As Double, ByVal epsf As Double, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.minbcsetcond(state.csobj, epsg, epsf, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetscale(ByRef state As minbcstate, ByVal s() As Double)
        Try
            alglib.minbcsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetprecdefault(ByRef state As minbcstate)
        Try
            alglib.minbcsetprecdefault(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetprecdiag(ByRef state As minbcstate, ByVal d() As Double)
        Try
            alglib.minbcsetprecdiag(state.csobj, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetprecscale(ByRef state As minbcstate)
        Try
            alglib.minbcsetprecscale(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetxrep(ByRef state As minbcstate, ByVal needxrep As Boolean)
        Try
            alglib.minbcsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcsetstpmax(ByRef state As minbcstate, ByVal stpmax As Double)
        Try
            alglib.minbcsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function minbciteration(ByRef state As minbcstate) As Boolean
        Try
            minbciteration = alglib.minbciteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear optimizer
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' NOTES:
    ' 
    ' 1. This function has two different implementations: one which  uses  exact
    '    (analytical) user-supplied gradient,  and one which uses function value
    '    only  and  numerically  differentiates  function  in  order  to  obtain
    '    gradient.
    ' 
    '    Depending  on  the  specific  function  used to create optimizer object
    '    (either  MinBCCreate() for analytical gradient or  MinBCCreateF()
    '    for numerical differentiation) you should choose appropriate variant of
    '    MinBCOptimize() - one  which  accepts  function  AND gradient or one
    '    which accepts function ONLY.
    ' 
    '    Be careful to choose variant of MinBCOptimize() which corresponds to
    '    your optimization scheme! Table below lists different  combinations  of
    '    callback (function/gradient) passed to MinBCOptimize()  and specific
    '    function used to create optimizer.
    ' 
    ' 
    '                      |         USER PASSED TO MinBCOptimize()
    '    CREATED WITH      |  function only   |  function and gradient
    '    ------------------------------------------------------------
    '    MinBCCreateF()    |     works               FAILS
    '    MinBCCreate()     |     FAILS               works
    ' 
    '    Here "FAIL" denotes inappropriate combinations  of  optimizer  creation
    '    function  and  MinBCOptimize()  version.   Attemps   to   use   such
    '    combination (for  example,  to  create optimizer with MinBCCreateF()
    '    and  to  pass  gradient  information  to  MinCGOptimize()) will lead to
    '    exception being thrown. Either  you  did  not pass gradient when it WAS
    '    needed or you passed gradient when it was NOT needed.
    ' 
    '   -- ALGLIB --
    '      Copyright 28.11.2010 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub minbcoptimize(ByVal state As minbcstate, ByVal func As ndimensional_func, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minbcoptimize(state.csobj, New alglib.ndimensional_func(AddressOf func.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub minbcoptimize(ByVal state As minbcstate, ByVal grad As ndimensional_grad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.minbcoptimize(state.csobj, New alglib.ndimensional_grad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub minbcoptguardgradient(ByRef state As minbcstate, ByVal teststep As Double)
        Try
            alglib.minbcoptguardgradient(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcoptguardsmoothness(ByRef state As minbcstate, ByVal level As Integer)
        Try
            alglib.minbcoptguardsmoothness(state.csobj, level)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcoptguardsmoothness(ByRef state As minbcstate)
        Try
            alglib.minbcoptguardsmoothness(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcoptguardresults(ByRef state As minbcstate, ByRef rep As optguardreport)
        Try
            rep = New optguardreport()
            alglib.minbcoptguardresults(state.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcoptguardnonc1test0results(ByVal state As minbcstate, ByRef strrep As optguardnonc1test0report, ByRef lngrep As optguardnonc1test0report)
        Try
            strrep = New optguardnonc1test0report()
            lngrep = New optguardnonc1test0report()
            alglib.minbcoptguardnonc1test0results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcoptguardnonc1test1results(ByRef state As minbcstate, ByRef strrep As optguardnonc1test1report, ByRef lngrep As optguardnonc1test1report)
        Try
            strrep = New optguardnonc1test1report()
            lngrep = New optguardnonc1test1report()
            alglib.minbcoptguardnonc1test1results(state.csobj, strrep.csobj, lngrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcresults(ByVal state As minbcstate, ByRef x() As Double, ByRef rep As minbcreport)
        Try
            rep = New minbcreport()
            alglib.minbcresults(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcresultsbuf(ByVal state As minbcstate, ByRef x() As Double, ByRef rep As minbcreport)
        Try
            alglib.minbcresultsbuf(state.csobj, x, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcrestartfrom(ByRef state As minbcstate, ByVal x() As Double)
        Try
            alglib.minbcrestartfrom(state.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub minbcrequesttermination(ByRef state As minbcstate)
        Try
            alglib.minbcrequesttermination(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class kdtreerequestbuffer
        Public csobj As alglib.kdtreerequestbuffer
    End Class
    Public Class kdtree
        Public csobj As alglib.kdtree
    End Class
    Public Sub kdtreeserialize(ByVal obj As kdtree, ByRef s_out As String)
        Try
            alglib.kdtreeserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub kdtreeunserialize(ByVal s_in As String, ByRef obj As kdtree)
        Try
            alglib.kdtreeunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreebuild(ByVal xy(,) As Double, ByVal n As Integer, ByVal nx As Integer, ByVal ny As Integer, ByVal normtype As Integer, ByRef kdt As kdtree)
        Try
            kdt = New kdtree()
            alglib.kdtreebuild(xy, n, nx, ny, normtype, kdt.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreebuild(ByVal xy(,) As Double, ByVal nx As Integer, ByVal ny As Integer, ByVal normtype As Integer, ByRef kdt As kdtree)
        Try
            kdt = New kdtree()
            alglib.kdtreebuild(xy, nx, ny, normtype, kdt.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreebuildtagged(ByVal xy(,) As Double, ByVal tags() As Integer, ByVal n As Integer, ByVal nx As Integer, ByVal ny As Integer, ByVal normtype As Integer, ByRef kdt As kdtree)
        Try
            kdt = New kdtree()
            alglib.kdtreebuildtagged(xy, tags, n, nx, ny, normtype, kdt.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreebuildtagged(ByVal xy(,) As Double, ByVal tags() As Integer, ByVal nx As Integer, ByVal ny As Integer, ByVal normtype As Integer, ByRef kdt As kdtree)
        Try
            kdt = New kdtree()
            alglib.kdtreebuildtagged(xy, tags, nx, ny, normtype, kdt.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreecreaterequestbuffer(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer)
        Try
            buf = New kdtreerequestbuffer()
            alglib.kdtreecreaterequestbuffer(kdt.csobj, buf.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function kdtreequeryknn(ByRef kdt As kdtree, ByVal x() As Double, ByVal k As Integer, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreequeryknn = alglib.kdtreequeryknn(kdt.csobj, x, k, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryknn(ByRef kdt As kdtree, ByVal x() As Double, ByVal k As Integer) As Integer
        Try
            kdtreequeryknn = alglib.kdtreequeryknn(kdt.csobj, x, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryknn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal k As Integer, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreetsqueryknn = alglib.kdtreetsqueryknn(kdt.csobj, buf.csobj, x, k, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryknn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal k As Integer) As Integer
        Try
            kdtreetsqueryknn = alglib.kdtreetsqueryknn(kdt.csobj, buf.csobj, x, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryrnn(ByRef kdt As kdtree, ByVal x() As Double, ByVal r As Double, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreequeryrnn = alglib.kdtreequeryrnn(kdt.csobj, x, r, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryrnn(ByRef kdt As kdtree, ByVal x() As Double, ByVal r As Double) As Integer
        Try
            kdtreequeryrnn = alglib.kdtreequeryrnn(kdt.csobj, x, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryrnnu(ByRef kdt As kdtree, ByVal x() As Double, ByVal r As Double, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreequeryrnnu = alglib.kdtreequeryrnnu(kdt.csobj, x, r, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryrnnu(ByRef kdt As kdtree, ByVal x() As Double, ByVal r As Double) As Integer
        Try
            kdtreequeryrnnu = alglib.kdtreequeryrnnu(kdt.csobj, x, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryrnn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal r As Double, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreetsqueryrnn = alglib.kdtreetsqueryrnn(kdt.csobj, buf.csobj, x, r, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryrnn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal r As Double) As Integer
        Try
            kdtreetsqueryrnn = alglib.kdtreetsqueryrnn(kdt.csobj, buf.csobj, x, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryrnnu(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal r As Double, ByVal selfmatch As Boolean) As Integer
        Try
            kdtreetsqueryrnnu = alglib.kdtreetsqueryrnnu(kdt.csobj, buf.csobj, x, r, selfmatch)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryrnnu(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal r As Double) As Integer
        Try
            kdtreetsqueryrnnu = alglib.kdtreetsqueryrnnu(kdt.csobj, buf.csobj, x, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryaknn(ByRef kdt As kdtree, ByVal x() As Double, ByVal k As Integer, ByVal selfmatch As Boolean, ByVal eps As Double) As Integer
        Try
            kdtreequeryaknn = alglib.kdtreequeryaknn(kdt.csobj, x, k, selfmatch, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequeryaknn(ByRef kdt As kdtree, ByVal x() As Double, ByVal k As Integer, ByVal eps As Double) As Integer
        Try
            kdtreequeryaknn = alglib.kdtreequeryaknn(kdt.csobj, x, k, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryaknn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal k As Integer, ByVal selfmatch As Boolean, ByVal eps As Double) As Integer
        Try
            kdtreetsqueryaknn = alglib.kdtreetsqueryaknn(kdt.csobj, buf.csobj, x, k, selfmatch, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsqueryaknn(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal x() As Double, ByVal k As Integer, ByVal eps As Double) As Integer
        Try
            kdtreetsqueryaknn = alglib.kdtreetsqueryaknn(kdt.csobj, buf.csobj, x, k, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreequerybox(ByRef kdt As kdtree, ByVal boxmin() As Double, ByVal boxmax() As Double) As Integer
        Try
            kdtreequerybox = alglib.kdtreequerybox(kdt.csobj, boxmin, boxmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function kdtreetsquerybox(ByVal kdt As kdtree, ByRef buf As kdtreerequestbuffer, ByVal boxmin() As Double, ByVal boxmax() As Double) As Integer
        Try
            kdtreetsquerybox = alglib.kdtreetsquerybox(kdt.csobj, buf.csobj, boxmin, boxmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub kdtreequeryresultsx(ByVal kdt As kdtree, ByRef x(,) As Double)
        Try
            alglib.kdtreequeryresultsx(kdt.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultsxy(ByVal kdt As kdtree, ByRef xy(,) As Double)
        Try
            alglib.kdtreequeryresultsxy(kdt.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultstags(ByVal kdt As kdtree, ByRef tags() As Integer)
        Try
            alglib.kdtreequeryresultstags(kdt.csobj, tags)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultsdistances(ByVal kdt As kdtree, ByRef r() As Double)
        Try
            alglib.kdtreequeryresultsdistances(kdt.csobj, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreetsqueryresultsx(ByVal kdt As kdtree, ByVal buf As kdtreerequestbuffer, ByRef x(,) As Double)
        Try
            alglib.kdtreetsqueryresultsx(kdt.csobj, buf.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreetsqueryresultsxy(ByVal kdt As kdtree, ByVal buf As kdtreerequestbuffer, ByRef xy(,) As Double)
        Try
            alglib.kdtreetsqueryresultsxy(kdt.csobj, buf.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreetsqueryresultstags(ByVal kdt As kdtree, ByVal buf As kdtreerequestbuffer, ByRef tags() As Integer)
        Try
            alglib.kdtreetsqueryresultstags(kdt.csobj, buf.csobj, tags)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreetsqueryresultsdistances(ByVal kdt As kdtree, ByVal buf As kdtreerequestbuffer, ByRef r() As Double)
        Try
            alglib.kdtreetsqueryresultsdistances(kdt.csobj, buf.csobj, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultsxi(ByVal kdt As kdtree, ByRef x(,) As Double)
        Try
            alglib.kdtreequeryresultsxi(kdt.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultsxyi(ByVal kdt As kdtree, ByRef xy(,) As Double)
        Try
            alglib.kdtreequeryresultsxyi(kdt.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultstagsi(ByVal kdt As kdtree, ByRef tags() As Integer)
        Try
            alglib.kdtreequeryresultstagsi(kdt.csobj, tags)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub kdtreequeryresultsdistancesi(ByVal kdt As kdtree, ByRef r() As Double)
        Try
            alglib.kdtreequeryresultsdistancesi(kdt.csobj, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class odesolverstate
        Public csobj As alglib.odesolverstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class odesolverreport
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.odesolverreport
    End Class


    Public Sub odesolverrkck(ByVal y() As Double, ByVal n As Integer, ByVal x() As Double, ByVal m As Integer, ByVal eps As Double, ByVal h As Double, ByRef state As odesolverstate)
        Try
            state = New odesolverstate()
            alglib.odesolverrkck(y, n, x, m, eps, h, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub odesolverrkck(ByVal y() As Double, ByVal x() As Double, ByVal eps As Double, ByVal h As Double, ByRef state As odesolverstate)
        Try
            state = New odesolverstate()
            alglib.odesolverrkck(y, x, eps, h, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function odesolveriteration(ByRef state As odesolverstate) As Boolean
        Try
            odesolveriteration = alglib.odesolveriteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This function is used to launcn iterations of ODE solver
    '
    ' It accepts following parameters:
    '     diff    -   callback which calculates dy/dx for given y and x
    '     obj     -   optional object which is passed to diff; can be NULL
    '
    ' 
    '   -- ALGLIB --
    '      Copyright 01.09.2009 by Bochkanov Sergey
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub odesolversolve(state As odesolverstate, diff As ndimensional_ode_rp, obj As Object)
        If diff Is Nothing Then
            Throw New AlglibException("ALGLIB: error in 'odesolversolve()' (diff is null)")
        End If
        Dim innerobj As alglib.odesolver.odesolverstate = state.csobj.innerobj
        Try
            While alglib.odesolver.odesolveriteration(innerobj, Nothing)
                If innerobj.needdy Then
                    diff(innerobj.y, innerobj.x, innerobj.dy, obj)
                    Continue While
                End If
                Throw New AlglibException("ALGLIB: unexpected error in 'odesolversolve'")
            End While
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub odesolverresults(ByVal state As odesolverstate, ByRef m As Integer, ByRef xtbl() As Double, ByRef ytbl(,) As Double, ByRef rep As odesolverreport)
        Try
            rep = New odesolverreport()
            alglib.odesolverresults(state.csobj, m, xtbl, ytbl, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub rmatrixinvupdatesimple(ByRef inva(,) As Double, ByVal n As Integer, ByVal updrow As Integer, ByVal updcolumn As Integer, ByVal updval As Double)
        Try
            alglib.rmatrixinvupdatesimple(inva, n, updrow, updcolumn, updval)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixinvupdaterow(ByRef inva(,) As Double, ByVal n As Integer, ByVal updrow As Integer, ByVal v() As Double)
        Try
            alglib.rmatrixinvupdaterow(inva, n, updrow, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixinvupdatecolumn(ByRef inva(,) As Double, ByVal n As Integer, ByVal updcolumn As Integer, ByVal u() As Double)
        Try
            alglib.rmatrixinvupdatecolumn(inva, n, updcolumn, u)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rmatrixinvupdateuv(ByRef inva(,) As Double, ByVal n As Integer, ByVal u() As Double, ByVal v() As Double)
        Try
            alglib.rmatrixinvupdateuv(inva, n, u, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function rmatrixschur(ByRef a(,) As Double, ByVal n As Integer, ByRef s(,) As Double) As Boolean
        Try
            rmatrixschur = alglib.rmatrixschur(a, n, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function smatrixgevd(ByVal a(,) As Double, ByVal n As Integer, ByVal isuppera As Boolean, ByVal b(,) As Double, ByVal isupperb As Boolean, ByVal zneeded As Integer, ByVal problemtype As Integer, ByRef d() As Double, ByRef z(,) As Double) As Boolean
        Try
            smatrixgevd = alglib.smatrixgevd(a, n, isuppera, b, isupperb, zneeded, problemtype, d, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function smatrixgevdreduce(ByRef a(,) As Double, ByVal n As Integer, ByVal isuppera As Boolean, ByVal b(,) As Double, ByVal isupperb As Boolean, ByVal problemtype As Integer, ByRef r(,) As Double, ByRef isupperr As Boolean) As Boolean
        Try
            smatrixgevdreduce = alglib.smatrixgevdreduce(a, n, isuppera, b, isupperb, problemtype, r, isupperr)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function rmatrixludet(ByVal a(,) As Double, ByVal pivots() As Integer, ByVal n As Integer) As Double
        Try
            rmatrixludet = alglib.rmatrixludet(a, pivots, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixludet(ByVal a(,) As Double, ByVal pivots() As Integer) As Double
        Try
            rmatrixludet = alglib.rmatrixludet(a, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixdet(ByVal a(,) As Double, ByVal n As Integer) As Double
        Try
            rmatrixdet = alglib.rmatrixdet(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rmatrixdet(ByVal a(,) As Double) As Double
        Try
            rmatrixdet = alglib.rmatrixdet(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixludet(ByVal a(,) As alglib.complex, ByVal pivots() As Integer, ByVal n As Integer) As alglib.complex
        Try
            cmatrixludet = alglib.cmatrixludet(a, pivots, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixludet(ByVal a(,) As alglib.complex, ByVal pivots() As Integer) As alglib.complex
        Try
            cmatrixludet = alglib.cmatrixludet(a, pivots)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixdet(ByVal a(,) As alglib.complex, ByVal n As Integer) As alglib.complex
        Try
            cmatrixdet = alglib.cmatrixdet(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cmatrixdet(ByVal a(,) As alglib.complex) As alglib.complex
        Try
            cmatrixdet = alglib.cmatrixdet(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholeskydet(ByVal a(,) As Double, ByVal n As Integer) As Double
        Try
            spdmatrixcholeskydet = alglib.spdmatrixcholeskydet(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixcholeskydet(ByVal a(,) As Double) As Double
        Try
            spdmatrixcholeskydet = alglib.spdmatrixcholeskydet(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixdet(ByVal a(,) As Double, ByVal n As Integer, ByVal isupper As Boolean) As Double
        Try
            spdmatrixdet = alglib.spdmatrixdet(a, n, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spdmatrixdet(ByVal a(,) As Double, ByVal isupper As Boolean) As Double
        Try
            spdmatrixdet = alglib.spdmatrixdet(a, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function gammafunction(ByVal x As Double) As Double
        Try
            gammafunction = alglib.gammafunction(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lngamma(ByVal x As Double, ByRef sgngam As Double) As Double
        Try
            lngamma = alglib.lngamma(x, sgngam)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub gqgeneraterec(ByVal alpha() As Double, ByVal beta() As Double, ByVal mu0 As Double, ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgeneraterec(alpha, beta, mu0, n, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategausslobattorec(ByVal alpha() As Double, ByVal beta() As Double, ByVal mu0 As Double, ByVal a As Double, ByVal b As Double, ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategausslobattorec(alpha, beta, mu0, a, b, n, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategaussradaurec(ByVal alpha() As Double, ByVal beta() As Double, ByVal mu0 As Double, ByVal a As Double, ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategaussradaurec(alpha, beta, mu0, a, n, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategausslegendre(ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategausslegendre(n, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategaussjacobi(ByVal n As Integer, ByVal alpha As Double, ByVal beta As Double, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategaussjacobi(n, alpha, beta, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategausslaguerre(ByVal n As Integer, ByVal alpha As Double, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategausslaguerre(n, alpha, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gqgenerategausshermite(ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef w() As Double)
        Try
            alglib.gqgenerategausshermite(n, info, x, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub gkqgeneraterec(ByVal alpha() As Double, ByVal beta() As Double, ByVal mu0 As Double, ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef wkronrod() As Double, ByRef wgauss() As Double)
        Try
            alglib.gkqgeneraterec(alpha, beta, mu0, n, info, x, wkronrod, wgauss)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gkqgenerategausslegendre(ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef wkronrod() As Double, ByRef wgauss() As Double)
        Try
            alglib.gkqgenerategausslegendre(n, info, x, wkronrod, wgauss)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gkqgenerategaussjacobi(ByVal n As Integer, ByVal alpha As Double, ByVal beta As Double, ByRef info As Integer, ByRef x() As Double, ByRef wkronrod() As Double, ByRef wgauss() As Double)
        Try
            alglib.gkqgenerategaussjacobi(n, alpha, beta, info, x, wkronrod, wgauss)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gkqlegendrecalc(ByVal n As Integer, ByRef info As Integer, ByRef x() As Double, ByRef wkronrod() As Double, ByRef wgauss() As Double)
        Try
            alglib.gkqlegendrecalc(n, info, x, wkronrod, wgauss)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub gkqlegendretbl(ByVal n As Integer, ByRef x() As Double, ByRef wkronrod() As Double, ByRef wgauss() As Double, ByRef eps As Double)
        Try
            alglib.gkqlegendretbl(n, x, wkronrod, wgauss, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Integration report:
    '* TerminationType = completetion code:
    '    * -5    non-convergence of Gauss-Kronrod nodes
    '            calculation subroutine.
    '    * -1    incorrect parameters were specified
    '    *  1    OK
    '* Rep.NFEV countains number of function calculations
    '* Rep.NIntervals contains number of intervals [a,b]
    '  was partitioned into.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class autogkreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property nintervals() As Integer
        Get
            Return csobj.nintervals
        End Get
        Set(ByVal Value As Integer)
            csobj.nintervals = Value
        End Set
        End Property
        Public csobj As alglib.autogkreport
    End Class
    Public Class autogkstate
        Public csobj As alglib.autogkstate
    End Class


    Public Sub autogksmooth(ByVal a As Double, ByVal b As Double, ByRef state As autogkstate)
        Try
            state = New autogkstate()
            alglib.autogksmooth(a, b, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub autogksmoothw(ByVal a As Double, ByVal b As Double, ByVal xwidth As Double, ByRef state As autogkstate)
        Try
            state = New autogkstate()
            alglib.autogksmoothw(a, b, xwidth, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub autogksingular(ByVal a As Double, ByVal b As Double, ByVal alpha As Double, ByVal beta As Double, ByRef state As autogkstate)
        Try
            state = New autogkstate()
            alglib.autogksingular(a, b, alpha, beta, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function autogkiteration(ByRef state As autogkstate) As Boolean
        Try
            autogkiteration = alglib.autogkiteration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This function is used to launcn iterations of ODE solver
    '
    ' It accepts following parameters:
    '     diff    -   callback which calculates dy/dx for given y and x
    '     obj     -   optional object which is passed to diff; can be NULL
    '
    ' 
    '   -- ALGLIB --
    '      Copyright 07.05.2009 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub autogkintegrate(state As autogkstate, func As integrator1_func, obj As Object)
        If func Is Nothing Then
            Throw New AlglibException("ALGLIB: error in 'autogkintegrate()' (func is null)")
        End If
        Dim innerobj As alglib.autogk.autogkstate = state.csobj.innerobj
        Try
            While alglib.autogk.autogkiteration(innerobj, Nothing)
                If innerobj.needf Then
                    func(innerobj.x, innerobj.xminusa, innerobj.bminusx, innerobj.f, obj)
                    Continue While
                End If
                Throw New AlglibException("ALGLIB: unexpected error in 'autogksolve'")
            End While
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub autogkresults(ByVal state As autogkstate, ByRef v As Double, ByRef rep As autogkreport)
        Try
            rep = New autogkreport()
            alglib.autogkresults(state.csobj, v, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function errorfunction(ByVal x As Double) As Double
        Try
            errorfunction = alglib.errorfunction(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function errorfunctionc(ByVal x As Double) As Double
        Try
            errorfunctionc = alglib.errorfunctionc(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function normaldistribution(ByVal x As Double) As Double
        Try
            normaldistribution = alglib.normaldistribution(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function normalpdf(ByVal x As Double) As Double
        Try
            normalpdf = alglib.normalpdf(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function normalcdf(ByVal x As Double) As Double
        Try
            normalcdf = alglib.normalcdf(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function inverf(ByVal e As Double) As Double
        Try
            inverf = alglib.inverf(e)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invnormaldistribution(ByVal y0 As Double) As Double
        Try
            invnormaldistribution = alglib.invnormaldistribution(y0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invnormalcdf(ByVal y0 As Double) As Double
        Try
            invnormalcdf = alglib.invnormalcdf(y0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function bivariatenormalpdf(ByVal x As Double, ByVal y As Double, ByVal rho As Double) As Double
        Try
            bivariatenormalpdf = alglib.bivariatenormalpdf(x, y, rho)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function bivariatenormalcdf(ByVal x As Double, ByVal y As Double, ByVal rho As Double) As Double
        Try
            bivariatenormalcdf = alglib.bivariatenormalcdf(x, y, rho)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function incompletebeta(ByVal a As Double, ByVal b As Double, ByVal x As Double) As Double
        Try
            incompletebeta = alglib.incompletebeta(a, b, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invincompletebeta(ByVal a As Double, ByVal b As Double, ByVal y As Double) As Double
        Try
            invincompletebeta = alglib.invincompletebeta(a, b, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function studenttdistribution(ByVal k As Integer, ByVal t As Double) As Double
        Try
            studenttdistribution = alglib.studenttdistribution(k, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invstudenttdistribution(ByVal k As Integer, ByVal p As Double) As Double
        Try
            invstudenttdistribution = alglib.invstudenttdistribution(k, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub samplemoments(ByVal x() As Double, ByVal n As Integer, ByRef mean As Double, ByRef variance As Double, ByRef skewness As Double, ByRef kurtosis As Double)
        Try
            alglib.samplemoments(x, n, mean, variance, skewness, kurtosis)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub samplemoments(ByVal x() As Double, ByRef mean As Double, ByRef variance As Double, ByRef skewness As Double, ByRef kurtosis As Double)
        Try
            alglib.samplemoments(x, mean, variance, skewness, kurtosis)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function samplemean(ByVal x() As Double, ByVal n As Integer) As Double
        Try
            samplemean = alglib.samplemean(x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function samplemean(ByVal x() As Double) As Double
        Try
            samplemean = alglib.samplemean(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function samplevariance(ByVal x() As Double, ByVal n As Integer) As Double
        Try
            samplevariance = alglib.samplevariance(x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function samplevariance(ByVal x() As Double) As Double
        Try
            samplevariance = alglib.samplevariance(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sampleskewness(ByVal x() As Double, ByVal n As Integer) As Double
        Try
            sampleskewness = alglib.sampleskewness(x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function sampleskewness(ByVal x() As Double) As Double
        Try
            sampleskewness = alglib.sampleskewness(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function samplekurtosis(ByVal x() As Double, ByVal n As Integer) As Double
        Try
            samplekurtosis = alglib.samplekurtosis(x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function samplekurtosis(ByVal x() As Double) As Double
        Try
            samplekurtosis = alglib.samplekurtosis(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub sampleadev(ByVal x() As Double, ByVal n As Integer, ByRef adev As Double)
        Try
            alglib.sampleadev(x, n, adev)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub sampleadev(ByVal x() As Double, ByRef adev As Double)
        Try
            alglib.sampleadev(x, adev)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub samplemedian(ByVal x() As Double, ByVal n As Integer, ByRef median As Double)
        Try
            alglib.samplemedian(x, n, median)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub samplemedian(ByVal x() As Double, ByRef median As Double)
        Try
            alglib.samplemedian(x, median)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub samplepercentile(ByVal x() As Double, ByVal n As Integer, ByVal p As Double, ByRef v As Double)
        Try
            alglib.samplepercentile(x, n, p, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub samplepercentile(ByVal x() As Double, ByVal p As Double, ByRef v As Double)
        Try
            alglib.samplepercentile(x, p, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function cov2(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As Double
        Try
            cov2 = alglib.cov2(x, y, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function cov2(ByVal x() As Double, ByVal y() As Double) As Double
        Try
            cov2 = alglib.cov2(x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function pearsoncorr2(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As Double
        Try
            pearsoncorr2 = alglib.pearsoncorr2(x, y, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function pearsoncorr2(ByVal x() As Double, ByVal y() As Double) As Double
        Try
            pearsoncorr2 = alglib.pearsoncorr2(x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spearmancorr2(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As Double
        Try
            spearmancorr2 = alglib.spearmancorr2(x, y, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spearmancorr2(ByVal x() As Double, ByVal y() As Double) As Double
        Try
            spearmancorr2 = alglib.spearmancorr2(x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub covm(ByVal x(,) As Double, ByVal n As Integer, ByVal m As Integer, ByRef c(,) As Double)
        Try
            alglib.covm(x, n, m, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub covm(ByVal x(,) As Double, ByRef c(,) As Double)
        Try
            alglib.covm(x, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pearsoncorrm(ByVal x(,) As Double, ByVal n As Integer, ByVal m As Integer, ByRef c(,) As Double)
        Try
            alglib.pearsoncorrm(x, n, m, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pearsoncorrm(ByVal x(,) As Double, ByRef c(,) As Double)
        Try
            alglib.pearsoncorrm(x, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spearmancorrm(ByVal x(,) As Double, ByVal n As Integer, ByVal m As Integer, ByRef c(,) As Double)
        Try
            alglib.spearmancorrm(x, n, m, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spearmancorrm(ByVal x(,) As Double, ByRef c(,) As Double)
        Try
            alglib.spearmancorrm(x, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub covm2(ByVal x(,) As Double, ByVal y(,) As Double, ByVal n As Integer, ByVal m1 As Integer, ByVal m2 As Integer, ByRef c(,) As Double)
        Try
            alglib.covm2(x, y, n, m1, m2, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub covm2(ByVal x(,) As Double, ByVal y(,) As Double, ByRef c(,) As Double)
        Try
            alglib.covm2(x, y, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pearsoncorrm2(ByVal x(,) As Double, ByVal y(,) As Double, ByVal n As Integer, ByVal m1 As Integer, ByVal m2 As Integer, ByRef c(,) As Double)
        Try
            alglib.pearsoncorrm2(x, y, n, m1, m2, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pearsoncorrm2(ByVal x(,) As Double, ByVal y(,) As Double, ByRef c(,) As Double)
        Try
            alglib.pearsoncorrm2(x, y, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spearmancorrm2(ByVal x(,) As Double, ByVal y(,) As Double, ByVal n As Integer, ByVal m1 As Integer, ByVal m2 As Integer, ByRef c(,) As Double)
        Try
            alglib.spearmancorrm2(x, y, n, m1, m2, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spearmancorrm2(ByVal x(,) As Double, ByVal y(,) As Double, ByRef c(,) As Double)
        Try
            alglib.spearmancorrm2(x, y, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rankdata(ByRef xy(,) As Double, ByVal npoints As Integer, ByVal nfeatures As Integer)
        Try
            alglib.rankdata(xy, npoints, nfeatures)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rankdata(ByRef xy(,) As Double)
        Try
            alglib.rankdata(xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rankdatacentered(ByRef xy(,) As Double, ByVal npoints As Integer, ByVal nfeatures As Integer)
        Try
            alglib.rankdatacentered(xy, npoints, nfeatures)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rankdatacentered(ByRef xy(,) As Double)
        Try
            alglib.rankdatacentered(xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function pearsoncorrelation(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As Double
        Try
            pearsoncorrelation = alglib.pearsoncorrelation(x, y, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function spearmanrankcorrelation(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As Double
        Try
            spearmanrankcorrelation = alglib.spearmanrankcorrelation(x, y, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub pearsoncorrelationsignificance(ByVal r As Double, ByVal n As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.pearsoncorrelationsignificance(r, n, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spearmanrankcorrelationsignificance(ByVal r As Double, ByVal n As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.spearmanrankcorrelationsignificance(r, n, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub jarqueberatest(ByVal x() As Double, ByVal n As Integer, ByRef p As Double)
        Try
            alglib.jarqueberatest(x, n, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function fdistribution(ByVal a As Integer, ByVal b As Integer, ByVal x As Double) As Double
        Try
            fdistribution = alglib.fdistribution(a, b, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function fcdistribution(ByVal a As Integer, ByVal b As Integer, ByVal x As Double) As Double
        Try
            fcdistribution = alglib.fcdistribution(a, b, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invfdistribution(ByVal a As Integer, ByVal b As Integer, ByVal y As Double) As Double
        Try
            invfdistribution = alglib.invfdistribution(a, b, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function incompletegamma(ByVal a As Double, ByVal x As Double) As Double
        Try
            incompletegamma = alglib.incompletegamma(a, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function incompletegammac(ByVal a As Double, ByVal x As Double) As Double
        Try
            incompletegammac = alglib.incompletegammac(a, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invincompletegammac(ByVal a As Double, ByVal y0 As Double) As Double
        Try
            invincompletegammac = alglib.invincompletegammac(a, y0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function chisquaredistribution(ByVal v As Double, ByVal x As Double) As Double
        Try
            chisquaredistribution = alglib.chisquaredistribution(v, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function chisquarecdistribution(ByVal v As Double, ByVal x As Double) As Double
        Try
            chisquarecdistribution = alglib.chisquarecdistribution(v, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invchisquaredistribution(ByVal v As Double, ByVal y As Double) As Double
        Try
            invchisquaredistribution = alglib.invchisquaredistribution(v, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub ftest(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.ftest(x, n, y, m, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub onesamplevariancetest(ByVal x() As Double, ByVal n As Integer, ByVal variance As Double, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.onesamplevariancetest(x, n, variance, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub wilcoxonsignedranktest(ByVal x() As Double, ByVal n As Integer, ByVal e As Double, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.wilcoxonsignedranktest(x, n, e, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub mannwhitneyutest(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.mannwhitneyutest(x, n, y, m, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function binomialdistribution(ByVal k As Integer, ByVal n As Integer, ByVal p As Double) As Double
        Try
            binomialdistribution = alglib.binomialdistribution(k, n, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function binomialcdistribution(ByVal k As Integer, ByVal n As Integer, ByVal p As Double) As Double
        Try
            binomialcdistribution = alglib.binomialcdistribution(k, n, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invbinomialdistribution(ByVal k As Integer, ByVal n As Integer, ByVal y As Double) As Double
        Try
            invbinomialdistribution = alglib.invbinomialdistribution(k, n, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub onesamplesigntest(ByVal x() As Double, ByVal n As Integer, ByVal median As Double, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.onesamplesigntest(x, n, median, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub studentttest1(ByVal x() As Double, ByVal n As Integer, ByVal mean As Double, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.studentttest1(x, n, mean, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub studentttest2(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.studentttest2(x, n, y, m, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub unequalvariancettest(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByRef bothtails As Double, ByRef lefttail As Double, ByRef righttail As Double)
        Try
            alglib.unequalvariancettest(x, n, y, m, bothtails, lefttail, righttail)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class barycentricinterpolant
        Public csobj As alglib.barycentricinterpolant
    End Class


    Public Function barycentriccalc(ByVal b As barycentricinterpolant, ByVal t As Double) As Double
        Try
            barycentriccalc = alglib.barycentriccalc(b.csobj, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub barycentricdiff1(ByVal b As barycentricinterpolant, ByVal t As Double, ByRef f As Double, ByRef df As Double)
        Try
            alglib.barycentricdiff1(b.csobj, t, f, df)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricdiff2(ByVal b As barycentricinterpolant, ByVal t As Double, ByRef f As Double, ByRef df As Double, ByRef d2f As Double)
        Try
            alglib.barycentricdiff2(b.csobj, t, f, df, d2f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentriclintransx(ByRef b As barycentricinterpolant, ByVal ca As Double, ByVal cb As Double)
        Try
            alglib.barycentriclintransx(b.csobj, ca, cb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentriclintransy(ByRef b As barycentricinterpolant, ByVal ca As Double, ByVal cb As Double)
        Try
            alglib.barycentriclintransy(b.csobj, ca, cb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricunpack(ByVal b As barycentricinterpolant, ByRef n As Integer, ByRef x() As Double, ByRef y() As Double, ByRef w() As Double)
        Try
            alglib.barycentricunpack(b.csobj, n, x, y, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricbuildxyw(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByRef b As barycentricinterpolant)
        Try
            b = New barycentricinterpolant()
            alglib.barycentricbuildxyw(x, y, w, n, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricbuildfloaterhormann(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal d As Integer, ByRef b As barycentricinterpolant)
        Try
            b = New barycentricinterpolant()
            alglib.barycentricbuildfloaterhormann(x, y, n, d, b.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class idwcalcbuffer
        Public csobj As alglib.idwcalcbuffer
    End Class
    Public Class idwmodel
        Public csobj As alglib.idwmodel
    End Class
    Public Class idwbuilder
        Public csobj As alglib.idwbuilder
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'IDW fitting report:
    '    rmserror        RMS error
    '    avgerror        average error
    '    maxerror        maximum error
    '    r2              coefficient of determination,  R-squared, 1-RSS/TSS
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class idwreport
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public csobj As alglib.idwreport
    End Class
    Public Sub idwserialize(ByVal obj As idwmodel, ByRef s_out As String)
        Try
            alglib.idwserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub idwunserialize(ByVal s_in As String, ByRef obj As idwmodel)
        Try
            alglib.idwunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwcreatecalcbuffer(ByVal s As idwmodel, ByRef buf As idwcalcbuffer)
        Try
            buf = New idwcalcbuffer()
            alglib.idwcreatecalcbuffer(s.csobj, buf.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildercreate(ByVal nx As Integer, ByVal ny As Integer, ByRef state As idwbuilder)
        Try
            state = New idwbuilder()
            alglib.idwbuildercreate(nx, ny, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetnlayers(ByRef state As idwbuilder, ByVal nlayers As Integer)
        Try
            alglib.idwbuildersetnlayers(state.csobj, nlayers)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetpoints(ByRef state As idwbuilder, ByVal xy(,) As Double, ByVal n As Integer)
        Try
            alglib.idwbuildersetpoints(state.csobj, xy, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetpoints(ByRef state As idwbuilder, ByVal xy(,) As Double)
        Try
            alglib.idwbuildersetpoints(state.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetalgomstab(ByRef state As idwbuilder, ByVal srad As Double)
        Try
            alglib.idwbuildersetalgomstab(state.csobj, srad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetalgotextbookshepard(ByRef state As idwbuilder, ByVal p As Double)
        Try
            alglib.idwbuildersetalgotextbookshepard(state.csobj, p)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetalgotextbookmodshepard(ByRef state As idwbuilder, ByVal r As Double)
        Try
            alglib.idwbuildersetalgotextbookmodshepard(state.csobj, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetuserterm(ByRef state As idwbuilder, ByVal v As Double)
        Try
            alglib.idwbuildersetuserterm(state.csobj, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetconstterm(ByRef state As idwbuilder)
        Try
            alglib.idwbuildersetconstterm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwbuildersetzeroterm(ByRef state As idwbuilder)
        Try
            alglib.idwbuildersetzeroterm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function idwcalc1(ByRef s As idwmodel, ByVal x0 As Double) As Double
        Try
            idwcalc1 = alglib.idwcalc1(s.csobj, x0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function idwcalc2(ByRef s As idwmodel, ByVal x0 As Double, ByVal x1 As Double) As Double
        Try
            idwcalc2 = alglib.idwcalc2(s.csobj, x0, x1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function idwcalc3(ByRef s As idwmodel, ByVal x0 As Double, ByVal x1 As Double, ByVal x2 As Double) As Double
        Try
            idwcalc3 = alglib.idwcalc3(s.csobj, x0, x1, x2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub idwcalc(ByRef s As idwmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.idwcalc(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwcalcbuf(ByRef s As idwmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.idwcalcbuf(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwtscalcbuf(ByVal s As idwmodel, ByRef buf As idwcalcbuffer, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.idwtscalcbuf(s.csobj, buf.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwfit(ByRef state As idwbuilder, ByRef model As idwmodel, ByRef rep As idwreport)
        Try
            model = New idwmodel()
            rep = New idwreport()
            alglib.idwfit(state.csobj, model.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function idwpeekprogress(ByVal s As idwbuilder) As Double
        Try
            idwpeekprogress = alglib.idwpeekprogress(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub idwgridcalc2v(ByVal s As idwmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByRef y() As Double)
        Try
            alglib.idwgridcalc2v(s.csobj, x0, n0, x1, n1, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub idwgridcalc2vsubset(ByVal s As idwmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByVal flagy() As Boolean, ByRef y() As Double)
        Try
            alglib.idwgridcalc2vsubset(s.csobj, x0, n0, x1, n1, flagy, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub







    Public Sub polynomialbar2cheb(ByVal p As barycentricinterpolant, ByVal a As Double, ByVal b As Double, ByRef t() As Double)
        Try
            alglib.polynomialbar2cheb(p.csobj, a, b, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialcheb2bar(ByVal t() As Double, ByVal n As Integer, ByVal a As Double, ByVal b As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialcheb2bar(t, n, a, b, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialcheb2bar(ByVal t() As Double, ByVal a As Double, ByVal b As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialcheb2bar(t, a, b, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbar2pow(ByVal p As barycentricinterpolant, ByVal c As Double, ByVal s As Double, ByRef a() As Double)
        Try
            alglib.polynomialbar2pow(p.csobj, c, s, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbar2pow(ByVal p As barycentricinterpolant, ByRef a() As Double)
        Try
            alglib.polynomialbar2pow(p.csobj, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialpow2bar(ByVal a() As Double, ByVal n As Integer, ByVal c As Double, ByVal s As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialpow2bar(a, n, c, s, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialpow2bar(ByVal a() As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialpow2bar(a, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuild(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuild(x, y, n, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuild(ByVal x() As Double, ByVal y() As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuild(x, y, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildeqdist(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByVal n As Integer, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildeqdist(a, b, y, n, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildeqdist(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildeqdist(a, b, y, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildcheb1(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByVal n As Integer, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildcheb1(a, b, y, n, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildcheb1(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildcheb1(a, b, y, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildcheb2(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByVal n As Integer, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildcheb2(a, b, y, n, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialbuildcheb2(ByVal a As Double, ByVal b As Double, ByVal y() As Double, ByRef p As barycentricinterpolant)
        Try
            p = New barycentricinterpolant()
            alglib.polynomialbuildcheb2(a, b, y, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function polynomialcalceqdist(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal n As Integer, ByVal t As Double) As Double
        Try
            polynomialcalceqdist = alglib.polynomialcalceqdist(a, b, f, n, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function polynomialcalceqdist(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal t As Double) As Double
        Try
            polynomialcalceqdist = alglib.polynomialcalceqdist(a, b, f, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function polynomialcalccheb1(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal n As Integer, ByVal t As Double) As Double
        Try
            polynomialcalccheb1 = alglib.polynomialcalccheb1(a, b, f, n, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function polynomialcalccheb1(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal t As Double) As Double
        Try
            polynomialcalccheb1 = alglib.polynomialcalccheb1(a, b, f, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function polynomialcalccheb2(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal n As Integer, ByVal t As Double) As Double
        Try
            polynomialcalccheb2 = alglib.polynomialcalccheb2(a, b, f, n, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function polynomialcalccheb2(ByVal a As Double, ByVal b As Double, ByVal f() As Double, ByVal t As Double) As Double
        Try
            polynomialcalccheb2 = alglib.polynomialcalccheb2(a, b, f, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    Public Class spline1dinterpolant
        Public csobj As alglib.spline1dinterpolant
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Spline fitting report:
    '    TerminationType completion code:
    '                    * >0 for success
    '                    * <0 for failure
    '    RMSError        RMS error
    '    AvgError        average error
    '    AvgRelError     average relative error (for non-zero Y[I])
    '    MaxError        maximum error
    '
    'Fields  below are  filled  by   obsolete    functions   (Spline1DFitCubic,
    'Spline1DFitHermite). Modern fitting functions do NOT fill these fields:
    '    TaskRCond       reciprocal of task's condition number
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class spline1dfitreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property taskrcond() As Double
        Get
            Return csobj.taskrcond
        End Get
        Set(ByVal Value As Double)
            csobj.taskrcond = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public csobj As alglib.spline1dfitreport
    End Class
    Public Sub spline1dserialize(ByVal obj As spline1dinterpolant, ByRef s_out As String)
        Try
            alglib.spline1dserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub spline1dunserialize(ByVal s_in As String, ByRef obj As spline1dinterpolant)
        Try
            alglib.spline1dunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildlinear(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildlinear(x, y, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildlinear(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildlinear(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildlinearbuf(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            alglib.spline1dbuildlinearbuf(x, y, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildlinearbuf(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            alglib.spline1dbuildlinearbuf(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildcubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildcubic(x, y, n, boundltype, boundl, boundrtype, boundr, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildcubic(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildcubic(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dgriddiffcubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByRef d() As Double)
        Try
            alglib.spline1dgriddiffcubic(x, y, n, boundltype, boundl, boundrtype, boundr, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dgriddiffcubic(ByVal x() As Double, ByVal y() As Double, ByRef d() As Double)
        Try
            alglib.spline1dgriddiffcubic(x, y, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dgriddiff2cubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByRef d1() As Double, ByRef d2() As Double)
        Try
            alglib.spline1dgriddiff2cubic(x, y, n, boundltype, boundl, boundrtype, boundr, d1, d2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dgriddiff2cubic(ByVal x() As Double, ByVal y() As Double, ByRef d1() As Double, ByRef d2() As Double)
        Try
            alglib.spline1dgriddiff2cubic(x, y, d1, d2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvcubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByVal x2() As Double, ByVal n2 As Integer, ByRef y2() As Double)
        Try
            alglib.spline1dconvcubic(x, y, n, boundltype, boundl, boundrtype, boundr, x2, n2, y2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvcubic(ByVal x() As Double, ByVal y() As Double, ByVal x2() As Double, ByRef y2() As Double)
        Try
            alglib.spline1dconvcubic(x, y, x2, y2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvdiffcubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByVal x2() As Double, ByVal n2 As Integer, ByRef y2() As Double, ByRef d2() As Double)
        Try
            alglib.spline1dconvdiffcubic(x, y, n, boundltype, boundl, boundrtype, boundr, x2, n2, y2, d2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvdiffcubic(ByVal x() As Double, ByVal y() As Double, ByVal x2() As Double, ByRef y2() As Double, ByRef d2() As Double)
        Try
            alglib.spline1dconvdiffcubic(x, y, x2, y2, d2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvdiff2cubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundltype As Integer, ByVal boundl As Double, ByVal boundrtype As Integer, ByVal boundr As Double, ByVal x2() As Double, ByVal n2 As Integer, ByRef y2() As Double, ByRef d2() As Double, ByRef dd2() As Double)
        Try
            alglib.spline1dconvdiff2cubic(x, y, n, boundltype, boundl, boundrtype, boundr, x2, n2, y2, d2, dd2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dconvdiff2cubic(ByVal x() As Double, ByVal y() As Double, ByVal x2() As Double, ByRef y2() As Double, ByRef d2() As Double, ByRef dd2() As Double)
        Try
            alglib.spline1dconvdiff2cubic(x, y, x2, y2, d2, dd2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildcatmullrom(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal boundtype As Integer, ByVal tension As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildcatmullrom(x, y, n, boundtype, tension, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildcatmullrom(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildcatmullrom(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildhermite(ByVal x() As Double, ByVal y() As Double, ByVal d() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildhermite(x, y, d, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildhermite(ByVal x() As Double, ByVal y() As Double, ByVal d() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildhermite(x, y, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildhermitebuf(ByVal x() As Double, ByVal y() As Double, ByVal d() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            alglib.spline1dbuildhermitebuf(x, y, d, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildhermitebuf(ByVal x() As Double, ByVal y() As Double, ByVal d() As Double, ByRef c As spline1dinterpolant)
        Try
            alglib.spline1dbuildhermitebuf(x, y, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildakima(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildakima(x, y, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildakima(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildakima(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildakimamod(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildakimamod(x, y, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildakimamod(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildakimamod(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spline1dcalc(ByVal c As spline1dinterpolant, ByVal x As Double) As Double
        Try
            spline1dcalc = alglib.spline1dcalc(c.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spline1ddiff(ByVal c As spline1dinterpolant, ByVal x As Double, ByRef s As Double, ByRef ds As Double, ByRef d2s As Double)
        Try
            alglib.spline1ddiff(c.csobj, x, s, ds, d2s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dunpack(ByVal c As spline1dinterpolant, ByRef n As Integer, ByRef tbl(,) As Double)
        Try
            alglib.spline1dunpack(c.csobj, n, tbl)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dlintransx(ByRef c As spline1dinterpolant, ByVal a As Double, ByVal b As Double)
        Try
            alglib.spline1dlintransx(c.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dlintransy(ByRef c As spline1dinterpolant, ByVal a As Double, ByVal b As Double)
        Try
            alglib.spline1dlintransy(c.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spline1dintegrate(ByVal c As spline1dinterpolant, ByVal x As Double) As Double
        Try
            spline1dintegrate = alglib.spline1dintegrate(c.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spline1dfit(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByVal lambdans As Double, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfit(x, y, n, m, lambdans, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfit(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByVal lambdans As Double, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfit(x, y, m, lambdans, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildmonotone(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildmonotone(x, y, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dbuildmonotone(ByVal x() As Double, ByVal y() As Double, ByRef c As spline1dinterpolant)
        Try
            c = New spline1dinterpolant()
            alglib.spline1dbuildmonotone(x, y, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Polynomial fitting report:
    '    TerminationType completion code: >0 for success, <0 for failure
    '    TaskRCond       reciprocal of task's condition number
    '    RMSError        RMS error
    '    AvgError        average error
    '    AvgRelError     average relative error (for non-zero Y[I])
    '    MaxError        maximum error
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class polynomialfitreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property taskrcond() As Double
        Get
            Return csobj.taskrcond
        End Get
        Set(ByVal Value As Double)
            csobj.taskrcond = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public csobj As alglib.polynomialfitreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Barycentric fitting report:
    '    TerminationType completion code: >0 for success, <0 for failure
    '    RMSError        RMS error
    '    AvgError        average error
    '    AvgRelError     average relative error (for non-zero Y[I])
    '    MaxError        maximum error
    '    TaskRCond       reciprocal of task's condition number
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class barycentricfitreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property taskrcond() As Double
        Get
            Return csobj.taskrcond
        End Get
        Set(ByVal Value As Double)
            csobj.taskrcond = Value
        End Set
        End Property
        Public Property dbest() As Integer
        Get
            Return csobj.dbest
        End Get
        Set(ByVal Value As Integer)
            csobj.dbest = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public csobj As alglib.barycentricfitreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Least squares fitting report. This structure contains informational fields
    'which are set by fitting functions provided by this unit.
    '
    'Different functions initialize different sets of  fields,  so  you  should
    'read documentation on specific function you used in order  to  know  which
    'fields are initialized.
    '
    '    TerminationType filled by all solvers:
    '                    * positive values, usually 1, denote success
    '                    * negative values denote various failure scenarios
    '
    '    TaskRCond       reciprocal of task's condition number
    '    IterationsCount number of internal iterations
    '
    '    VarIdx          if user-supplied gradient contains errors  which  were
    '                    detected by nonlinear fitter, this  field  is  set  to
    '                    index  of  the  first  component  of gradient which is
    '                    suspected to be spoiled by bugs.
    '
    '    RMSError        RMS error
    '    AvgError        average error
    '    AvgRelError     average relative error (for non-zero Y[I])
    '    MaxError        maximum error
    '
    '    WRMSError       weighted RMS error
    '
    '    CovPar          covariance matrix for parameters, filled by some solvers
    '    ErrPar          vector of errors in parameters, filled by some solvers
    '    ErrCurve        vector of fit errors -  variability  of  the  best-fit
    '                    curve, filled by some solvers.
    '    Noise           vector of per-point noise estimates, filled by
    '                    some solvers.
    '    R2              coefficient of determination (non-weighted, non-adjusted),
    '                    filled by some solvers.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class lsfitreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property taskrcond() As Double
        Get
            Return csobj.taskrcond
        End Get
        Set(ByVal Value As Double)
            csobj.taskrcond = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property varidx() As Integer
        Get
            Return csobj.varidx
        End Get
        Set(ByVal Value As Integer)
            csobj.varidx = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public Property wrmserror() As Double
        Get
            Return csobj.wrmserror
        End Get
        Set(ByVal Value As Double)
            csobj.wrmserror = Value
        End Set
        End Property
        Public Property covpar() As Double(,)
        Get
            Return csobj.covpar
        End Get
        Set(ByVal Value As Double(,))
            csobj.covpar = Value
        End Set
        End Property
        Public Property errpar() As Double()
        Get
            Return csobj.errpar
        End Get
        Set(ByVal Value As Double())
            csobj.errpar = Value
        End Set
        End Property
        Public Property errcurve() As Double()
        Get
            Return csobj.errcurve
        End Get
        Set(ByVal Value As Double())
            csobj.errcurve = Value
        End Set
        End Property
        Public Property noise() As Double()
        Get
            Return csobj.noise
        End Get
        Set(ByVal Value As Double())
            csobj.noise = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public csobj As alglib.lsfitreport
    End Class
    Public Class lsfitstate
        Public csobj As alglib.lsfitstate
    End Class


    Public Sub lstfitpiecewiselinearrdpfixed(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef x2() As Double, ByRef y2() As Double, ByRef nsections As Integer)
        Try
            alglib.lstfitpiecewiselinearrdpfixed(x, y, n, m, x2, y2, nsections)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lstfitpiecewiselinearrdp(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal eps As Double, ByRef x2() As Double, ByRef y2() As Double, ByRef nsections As Integer)
        Try
            alglib.lstfitpiecewiselinearrdp(x, y, n, eps, x2, y2, nsections)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialfit(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef p As barycentricinterpolant, ByRef rep As polynomialfitreport)
        Try
            p = New barycentricinterpolant()
            rep = New polynomialfitreport()
            alglib.polynomialfit(x, y, n, m, p.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialfit(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByRef p As barycentricinterpolant, ByRef rep As polynomialfitreport)
        Try
            p = New barycentricinterpolant()
            rep = New polynomialfitreport()
            alglib.polynomialfit(x, y, m, p.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialfitwc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal k As Integer, ByVal m As Integer, ByRef p As barycentricinterpolant, ByRef rep As polynomialfitreport)
        Try
            p = New barycentricinterpolant()
            rep = New polynomialfitreport()
            alglib.polynomialfitwc(x, y, w, n, xc, yc, dc, k, m, p.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub polynomialfitwc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal m As Integer, ByRef p As barycentricinterpolant, ByRef rep As polynomialfitreport)
        Try
            p = New barycentricinterpolant()
            rep = New polynomialfitreport()
            alglib.polynomialfitwc(x, y, w, xc, yc, dc, m, p.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function logisticcalc4(ByVal x As Double, ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal d As Double) As Double
        Try
            logisticcalc4 = alglib.logisticcalc4(x, a, b, c, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function logisticcalc5(ByVal x As Double, ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal d As Double, ByVal g As Double) As Double
        Try
            logisticcalc5 = alglib.logisticcalc5(x, a, b, c, d, g)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub logisticfit4(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef d As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.logisticfit4(x, y, n, a, b, c, d, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub logisticfit4ec(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal cnstrleft As Double, ByVal cnstrright As Double, ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef d As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.logisticfit4ec(x, y, n, cnstrleft, cnstrright, a, b, c, d, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub logisticfit5(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef d As Double, ByRef g As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.logisticfit5(x, y, n, a, b, c, d, g, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub logisticfit5ec(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal cnstrleft As Double, ByVal cnstrright As Double, ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef d As Double, ByRef g As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.logisticfit5ec(x, y, n, cnstrleft, cnstrright, a, b, c, d, g, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub logisticfit45x(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal cnstrleft As Double, ByVal cnstrright As Double, ByVal is4pl As Boolean, ByVal lambdav As Double, ByVal epsx As Double, ByVal rscnt As Integer, ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef d As Double, ByRef g As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.logisticfit45x(x, y, n, cnstrleft, cnstrright, is4pl, lambdav, epsx, rscnt, a, b, c, d, g, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricfitfloaterhormannwc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal k As Integer, ByVal m As Integer, ByRef b As barycentricinterpolant, ByRef rep As barycentricfitreport)
        Try
            b = New barycentricinterpolant()
            rep = New barycentricfitreport()
            alglib.barycentricfitfloaterhormannwc(x, y, w, n, xc, yc, dc, k, m, b.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub barycentricfitfloaterhormann(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef b As barycentricinterpolant, ByRef rep As barycentricfitreport)
        Try
            b = New barycentricinterpolant()
            rep = New barycentricfitreport()
            alglib.barycentricfitfloaterhormann(x, y, n, m, b.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitcubicwc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal k As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitcubicwc(x, y, w, n, xc, yc, dc, k, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitcubicwc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitcubicwc(x, y, w, xc, yc, dc, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermitewc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal k As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermitewc(x, y, w, n, xc, yc, dc, k, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermitewc(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal xc() As Double, ByVal yc() As Double, ByVal dc() As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermitewc(x, y, w, xc, yc, dc, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermitedeprecated(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermitedeprecated(x, y, n, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermitedeprecated(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermitedeprecated(x, y, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearw(ByVal y() As Double, ByVal w() As Double, ByVal fmatrix(,) As Double, ByVal n As Integer, ByVal m As Integer, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearw(y, w, fmatrix, n, m, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearw(ByVal y() As Double, ByVal w() As Double, ByVal fmatrix(,) As Double, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearw(y, w, fmatrix, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearwc(ByVal y() As Double, ByVal w() As Double, ByVal fmatrix(,) As Double, ByVal cmatrix(,) As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearwc(y, w, fmatrix, cmatrix, n, m, k, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearwc(ByVal y() As Double, ByVal w() As Double, ByVal fmatrix(,) As Double, ByVal cmatrix(,) As Double, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearwc(y, w, fmatrix, cmatrix, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinear(ByVal y() As Double, ByVal fmatrix(,) As Double, ByVal n As Integer, ByVal m As Integer, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinear(y, fmatrix, n, m, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinear(ByVal y() As Double, ByVal fmatrix(,) As Double, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinear(y, fmatrix, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearc(ByVal y() As Double, ByVal fmatrix(,) As Double, ByVal cmatrix(,) As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearc(y, fmatrix, cmatrix, n, m, k, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitlinearc(ByVal y() As Double, ByVal fmatrix(,) As Double, ByVal cmatrix(,) As Double, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitlinearc(y, fmatrix, cmatrix, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatewf(ByVal x(,) As Double, ByVal y() As Double, ByVal w() As Double, ByVal c() As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByVal diffstep As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatewf(x, y, w, c, n, m, k, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatewf(ByVal x(,) As Double, ByVal y() As Double, ByVal w() As Double, ByVal c() As Double, ByVal diffstep As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatewf(x, y, w, c, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatef(ByVal x(,) As Double, ByVal y() As Double, ByVal c() As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByVal diffstep As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatef(x, y, c, n, m, k, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatef(ByVal x(,) As Double, ByVal y() As Double, ByVal c() As Double, ByVal diffstep As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatef(x, y, c, diffstep, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatewfg(ByVal x(,) As Double, ByVal y() As Double, ByVal w() As Double, ByVal c() As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatewfg(x, y, w, c, n, m, k, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatewfg(ByVal x(,) As Double, ByVal y() As Double, ByVal w() As Double, ByVal c() As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatewfg(x, y, w, c, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatefg(ByVal x(,) As Double, ByVal y() As Double, ByVal c() As Double, ByVal n As Integer, ByVal m As Integer, ByVal k As Integer, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatefg(x, y, c, n, m, k, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitcreatefg(ByVal x(,) As Double, ByVal y() As Double, ByVal c() As Double, ByRef state As lsfitstate)
        Try
            state = New lsfitstate()
            alglib.lsfitcreatefg(x, y, c, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetnonmonotonicsteps(ByRef state As lsfitstate, ByVal cnt As Integer)
        Try
            alglib.lsfitsetnonmonotonicsteps(state.csobj, cnt)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetnumdiff(ByRef state As lsfitstate, ByVal formulatype As Integer)
        Try
            alglib.lsfitsetnumdiff(state.csobj, formulatype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetcond(ByRef state As lsfitstate, ByVal epsx As Double, ByVal maxits As Integer)
        Try
            alglib.lsfitsetcond(state.csobj, epsx, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetstpmax(ByRef state As lsfitstate, ByVal stpmax As Double)
        Try
            alglib.lsfitsetstpmax(state.csobj, stpmax)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetxrep(ByRef state As lsfitstate, ByVal needxrep As Boolean)
        Try
            alglib.lsfitsetxrep(state.csobj, needxrep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetscale(ByRef state As lsfitstate, ByVal s() As Double)
        Try
            alglib.lsfitsetscale(state.csobj, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetbc(ByRef state As lsfitstate, ByVal bndl() As Double, ByVal bndu() As Double)
        Try
            alglib.lsfitsetbc(state.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetlc(ByRef state As lsfitstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.lsfitsetlc(state.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetlc(ByRef state As lsfitstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.lsfitsetlc(state.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function lsfititeration(ByRef state As lsfitstate) As Boolean
        Try
            lsfititeration = alglib.lsfititeration(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' This family of functions is used to start iterations of nonlinear fitter
    ' 
    ' These functions accept following parameters:
    '     func    -   callback which calculates function (or merit function)
    '                 value func at given point x
    '     grad    -   callback which calculates function (or merit function)
    '                 value func and gradient grad at given point x
    '     rep     -   optional callback which is called after each iteration
    '                 can be null
    '     obj     -   optional object which is passed to func/grad/hess/jac/rep
    '                 can be null
    ' 
    ' 
    ' 
    ' CALLBACK PARALLELISM:
    ' 
    ' The  LSFIT  optimizer  supports  parallel  model  evaluation  and parallel
    ' numerical differentiation ('callback parallelism'). This feature, which is
    ' present in commercial ALGLIB editions, greatly accelerates fits with large
    ' datasets and/or expensive target functions.
    ' 
    ' Callback parallelism is usually beneficial when a  single  pass  over  the
    ' entire dataset requires more than several milliseconds. In this  case  the
    ' job of computing model values at  dataset  points  can  be  split  between
    ' multiple threads.
    ' 
    ' If you employ a numerical differentiation scheme, you can also parallelize
    ' computation of different components of a numerical gradient. Generally, the
    ' mode computationally demanding your problem is (many points, numerical differentiation,
    ' expensive model), the more you can get for multithreading.
    ' 
    ' ALGLIB Reference Manual, 'Working with commercial  version' section,
    ' describes how to activate callback parallelism for your programming language.
    ' 
    ' CALLBACK ARGUMENTS
    ' 
    ' This algorithm is somewhat unusual because  it  works  with  parameterized
    ' function f(C,X), where X is  a  function  argument (we  have  many  points
    ' which  are  characterized  by different  argument  values),  and  C  is  a
    ' parameter to fit.
    ' 
    ' For example, if we want to do linear  fit  by  f(c0,c1,x) = c0*x+c1,  then
    ' x will be argument, and {c0,c1} will be parameters.
    ' 
    ' It is important to understand that this algorithm finds   minimum  in  the
    ' space of function PARAMETERS (not  arguments),  so  it  needs  derivatives
    ' of f() with respect to C, not X.
    ' 
    ' In the example above it will need f=c0*x+c1 and {df/dc0,df/dc1} = {x,1}
    ' instead of {df/dx} = {c0}.
    ' 
    '   -- ALGLIB --
    '      Copyright 17.12.2023 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub lsfitfit(ByVal state As lsfitstate, ByVal func As ndimensional_pfunc, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.lsfitfit(state.csobj, New alglib.ndimensional_pfunc(AddressOf func.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub


    Public Sub lsfitfit(ByVal state As lsfitstate, ByVal func As ndimensional_pfunc, ByVal grad As ndimensional_pgrad, ByVal rep As ndimensional_rep, ByVal obj As Object)
        Dim arep As alglib.ndimensional_rep = Nothing

        If rep Isnot Nothing Then
            arep = New alglib.ndimensional_rep(AddressOf rep.Invoke)
        End If
        Try
            alglib.lsfitfit(state.csobj, New alglib.ndimensional_pfunc(AddressOf func.Invoke), New alglib.ndimensional_pgrad(AddressOf grad.Invoke), arep, obj)
        Catch E As alglib.alglibexception
            Throw New AlglibException(E.Msg)
        End Try
    End Sub




    Public Sub lsfitresults(ByVal state As lsfitstate, ByRef c() As Double, ByRef rep As lsfitreport)
        Try
            rep = New lsfitreport()
            alglib.lsfitresults(state.csobj, c, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lsfitsetgradientcheck(ByRef state As lsfitstate, ByVal teststep As Double)
        Try
            alglib.lsfitsetgradientcheck(state.csobj, teststep)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub fitspherels(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef r As Double)
        Try
            alglib.fitspherels(xy, npoints, nx, cx, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fitspheremc(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rhi As Double)
        Try
            alglib.fitspheremc(xy, npoints, nx, cx, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fitspheremi(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rlo As Double)
        Try
            alglib.fitspheremi(xy, npoints, nx, cx, rlo)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fitspheremz(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rlo As Double, ByRef rhi As Double)
        Try
            alglib.fitspheremz(xy, npoints, nx, cx, rlo, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fitspherex(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByVal problemtype As Integer, ByVal epsx As Double, ByVal aulits As Integer, ByRef cx() As Double, ByRef rlo As Double, ByRef rhi As Double)
        Try
            alglib.fitspherex(xy, npoints, nx, problemtype, epsx, aulits, cx, rlo, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class pspline2interpolant
        Public csobj As alglib.pspline2interpolant
    End Class
    Public Class pspline3interpolant
        Public csobj As alglib.pspline3interpolant
    End Class


    Public Sub pspline2build(ByVal xy(,) As Double, ByVal n As Integer, ByVal st As Integer, ByVal pt As Integer, ByRef p As pspline2interpolant)
        Try
            p = New pspline2interpolant()
            alglib.pspline2build(xy, n, st, pt, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3build(ByVal xy(,) As Double, ByVal n As Integer, ByVal st As Integer, ByVal pt As Integer, ByRef p As pspline3interpolant)
        Try
            p = New pspline3interpolant()
            alglib.pspline3build(xy, n, st, pt, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2buildperiodic(ByVal xy(,) As Double, ByVal n As Integer, ByVal st As Integer, ByVal pt As Integer, ByRef p As pspline2interpolant)
        Try
            p = New pspline2interpolant()
            alglib.pspline2buildperiodic(xy, n, st, pt, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3buildperiodic(ByVal xy(,) As Double, ByVal n As Integer, ByVal st As Integer, ByVal pt As Integer, ByRef p As pspline3interpolant)
        Try
            p = New pspline3interpolant()
            alglib.pspline3buildperiodic(xy, n, st, pt, p.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2parametervalues(ByVal p As pspline2interpolant, ByRef n As Integer, ByRef t() As Double)
        Try
            alglib.pspline2parametervalues(p.csobj, n, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3parametervalues(ByVal p As pspline3interpolant, ByRef n As Integer, ByRef t() As Double)
        Try
            alglib.pspline3parametervalues(p.csobj, n, t)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2calc(ByVal p As pspline2interpolant, ByVal t As Double, ByRef x As Double, ByRef y As Double)
        Try
            alglib.pspline2calc(p.csobj, t, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3calc(ByVal p As pspline3interpolant, ByVal t As Double, ByRef x As Double, ByRef y As Double, ByRef z As Double)
        Try
            alglib.pspline3calc(p.csobj, t, x, y, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2tangent(ByVal p As pspline2interpolant, ByVal t As Double, ByRef x As Double, ByRef y As Double)
        Try
            alglib.pspline2tangent(p.csobj, t, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3tangent(ByVal p As pspline3interpolant, ByVal t As Double, ByRef x As Double, ByRef y As Double, ByRef z As Double)
        Try
            alglib.pspline3tangent(p.csobj, t, x, y, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2diff(ByVal p As pspline2interpolant, ByVal t As Double, ByRef x As Double, ByRef dx As Double, ByRef y As Double, ByRef dy As Double)
        Try
            alglib.pspline2diff(p.csobj, t, x, dx, y, dy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3diff(ByVal p As pspline3interpolant, ByVal t As Double, ByRef x As Double, ByRef dx As Double, ByRef y As Double, ByRef dy As Double, ByRef z As Double, ByRef dz As Double)
        Try
            alglib.pspline3diff(p.csobj, t, x, dx, y, dy, z, dz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline2diff2(ByVal p As pspline2interpolant, ByVal t As Double, ByRef x As Double, ByRef dx As Double, ByRef d2x As Double, ByRef y As Double, ByRef dy As Double, ByRef d2y As Double)
        Try
            alglib.pspline2diff2(p.csobj, t, x, dx, d2x, y, dy, d2y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pspline3diff2(ByVal p As pspline3interpolant, ByVal t As Double, ByRef x As Double, ByRef dx As Double, ByRef d2x As Double, ByRef y As Double, ByRef dy As Double, ByRef d2y As Double, ByRef z As Double, ByRef dz As Double, ByRef d2z As Double)
        Try
            alglib.pspline3diff2(p.csobj, t, x, dx, d2x, y, dy, d2y, z, dz, d2z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function pspline2arclength(ByVal p As pspline2interpolant, ByVal a As Double, ByVal b As Double) As Double
        Try
            pspline2arclength = alglib.pspline2arclength(p.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function pspline3arclength(ByVal p As pspline3interpolant, ByVal a As Double, ByVal b As Double) As Double
        Try
            pspline3arclength = alglib.pspline3arclength(p.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub parametricrdpfixed(ByVal x(,) As Double, ByVal n As Integer, ByVal d As Integer, ByVal stopm As Integer, ByVal stopeps As Double, ByRef x2(,) As Double, ByRef idx2() As Integer, ByRef nsections As Integer)
        Try
            alglib.parametricrdpfixed(x, n, d, stopm, stopeps, x2, idx2, nsections)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub










    Public Class spline2dinterpolant
        Public csobj As alglib.spline2dinterpolant
    End Class
    Public Class spline2dbuilder
        Public csobj As alglib.spline2dbuilder
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Spline 2D fitting report:
    '    rmserror        RMS error
    '    avgerror        average error
    '    maxerror        maximum error
    '    r2              coefficient of determination,  R-squared, 1-RSS/TSS
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class spline2dfitreport
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public Property r2() As Double
        Get
            Return csobj.r2
        End Get
        Set(ByVal Value As Double)
            csobj.r2 = Value
        End Set
        End Property
        Public csobj As alglib.spline2dfitreport
    End Class
    Public Sub spline2dserialize(ByVal obj As spline2dinterpolant, ByRef s_out As String)
        Try
            alglib.spline2dserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub spline2dunserialize(ByVal s_in As String, ByRef obj As spline2dinterpolant)
        Try
            alglib.spline2dunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spline2dcalc(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double) As Double
        Try
            spline2dcalc = alglib.spline2dcalc(c.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spline2ddiff(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByRef f As Double, ByRef fx As Double, ByRef fy As Double)
        Try
            alglib.spline2ddiff(c.csobj, x, y, f, fx, fy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2ddiff2(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByRef f As Double, ByRef fx As Double, ByRef fy As Double, ByRef fxx As Double, ByRef fxy As Double, ByRef fyy As Double)
        Try
            alglib.spline2ddiff2(c.csobj, x, y, f, fx, fy, fxx, fxy, fyy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dcalcvbuf(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByRef f() As Double)
        Try
            alglib.spline2dcalcvbuf(c.csobj, x, y, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function spline2dcalcvi(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByVal i As Integer) As Double
        Try
            spline2dcalcvi = alglib.spline2dcalcvi(c.csobj, x, y, i)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spline2dcalcv(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByRef f() As Double)
        Try
            alglib.spline2dcalcv(c.csobj, x, y, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2ddiffvi(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByVal i As Integer, ByRef f As Double, ByRef fx As Double, ByRef fy As Double)
        Try
            alglib.spline2ddiffvi(c.csobj, x, y, i, f, fx, fy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2ddiff2vi(ByVal c As spline2dinterpolant, ByVal x As Double, ByVal y As Double, ByVal i As Integer, ByRef f As Double, ByRef fx As Double, ByRef fy As Double, ByRef fxx As Double, ByRef fxy As Double, ByRef fyy As Double)
        Try
            alglib.spline2ddiff2vi(c.csobj, x, y, i, f, fx, fy, fxx, fxy, fyy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dlintransxy(ByRef c As spline2dinterpolant, ByVal ax As Double, ByVal bx As Double, ByVal ay As Double, ByVal by As Double)
        Try
            alglib.spline2dlintransxy(c.csobj, ax, bx, ay, by)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dlintransf(ByRef c As spline2dinterpolant, ByVal a As Double, ByVal b As Double)
        Try
            alglib.spline2dlintransf(c.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dcopy(ByVal c As spline2dinterpolant, ByRef cc As spline2dinterpolant)
        Try
            cc = New spline2dinterpolant()
            alglib.spline2dcopy(c.csobj, cc.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dresamplebicubic(ByVal a(,) As Double, ByVal oldheight As Integer, ByVal oldwidth As Integer, ByRef b(,) As Double, ByVal newheight As Integer, ByVal newwidth As Integer)
        Try
            alglib.spline2dresamplebicubic(a, oldheight, oldwidth, b, newheight, newwidth)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dresamplebilinear(ByVal a(,) As Double, ByVal oldheight As Integer, ByVal oldwidth As Integer, ByRef b(,) As Double, ByVal newheight As Integer, ByVal newwidth As Integer)
        Try
            alglib.spline2dresamplebilinear(a, oldheight, oldwidth, b, newheight, newwidth)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbilinearv(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbilinearv(x, n, y, m, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbilinearvbuf(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            alglib.spline2dbuildbilinearvbuf(x, n, y, m, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbilinearmissing(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal missing() As Boolean, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbilinearmissing(x, n, y, m, f, missing, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbilinearmissingbuf(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal missing() As Boolean, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            alglib.spline2dbuildbilinearmissingbuf(x, n, y, m, f, missing, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubicv(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbicubicv(x, n, y, m, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubicv(ByVal x() As Double, ByVal y() As Double, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbicubicv(x, y, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildclampedv(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal bndbtm() As Double, ByVal bndtypebtm As Integer, ByVal bndtop() As Double, ByVal bndtypetop As Integer, ByVal bndlft() As Double, ByVal bndtypelft As Integer, ByVal bndrgt() As Double, ByVal bndtypergt As Integer, ByVal mixedd() As Double, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildclampedv(x, n, y, m, bndbtm, bndtypebtm, bndtop, bndtypetop, bndlft, bndtypelft, bndrgt, bndtypergt, mixedd, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildclampedv(ByVal x() As Double, ByVal y() As Double, ByVal bndbtm() As Double, ByVal bndtypebtm As Integer, ByVal bndtop() As Double, ByVal bndtypetop As Integer, ByVal bndlft() As Double, ByVal bndtypelft As Integer, ByVal bndrgt() As Double, ByVal bndtypergt As Integer, ByVal mixedd() As Double, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildclampedv(x, y, bndbtm, bndtypebtm, bndtop, bndtypetop, bndlft, bndtypelft, bndrgt, bndtypergt, mixedd, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildhermitev(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal dfdx() As Double, ByVal dfdy() As Double, ByVal d2fdxdy() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildhermitev(x, n, y, m, f, dfdx, dfdy, d2fdxdy, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildhermitev(ByVal x() As Double, ByVal y() As Double, ByVal f() As Double, ByVal dfdx() As Double, ByVal dfdy() As Double, ByVal d2fdxdy() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildhermitev(x, y, f, dfdx, dfdy, d2fdxdy, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubicvbuf(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            alglib.spline2dbuildbicubicvbuf(x, n, y, m, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubicmissing(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal missing() As Boolean, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbicubicmissing(x, n, y, m, f, missing, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubicmissingbuf(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal f() As Double, ByVal missing() As Boolean, ByVal d As Integer, ByRef c As spline2dinterpolant)
        Try
            alglib.spline2dbuildbicubicmissingbuf(x, n, y, m, f, missing, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dunpackv(ByVal c As spline2dinterpolant, ByRef m As Integer, ByRef n As Integer, ByRef d As Integer, ByRef tbl(,) As Double)
        Try
            alglib.spline2dunpackv(c.csobj, m, n, d, tbl)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbilinear(ByVal x() As Double, ByVal y() As Double, ByVal f(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbilinear(x, y, f, m, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildbicubic(ByVal x() As Double, ByVal y() As Double, ByVal f(,) As Double, ByVal m As Integer, ByVal n As Integer, ByRef c As spline2dinterpolant)
        Try
            c = New spline2dinterpolant()
            alglib.spline2dbuildbicubic(x, y, f, m, n, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dunpack(ByVal c As spline2dinterpolant, ByRef m As Integer, ByRef n As Integer, ByRef tbl(,) As Double)
        Try
            alglib.spline2dunpack(c.csobj, m, n, tbl)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildercreate(ByVal d As Integer, ByRef state As spline2dbuilder)
        Try
            state = New spline2dbuilder()
            alglib.spline2dbuildercreate(d, state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetuserterm(ByRef state As spline2dbuilder, ByVal v As Double)
        Try
            alglib.spline2dbuildersetuserterm(state.csobj, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetlinterm(ByRef state As spline2dbuilder)
        Try
            alglib.spline2dbuildersetlinterm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetconstterm(ByRef state As spline2dbuilder)
        Try
            alglib.spline2dbuildersetconstterm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetzeroterm(ByRef state As spline2dbuilder)
        Try
            alglib.spline2dbuildersetzeroterm(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetpoints(ByRef state As spline2dbuilder, ByVal xy(,) As Double, ByVal n As Integer)
        Try
            alglib.spline2dbuildersetpoints(state.csobj, xy, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetareaauto(ByRef state As spline2dbuilder)
        Try
            alglib.spline2dbuildersetareaauto(state.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetarea(ByRef state As spline2dbuilder, ByVal xa As Double, ByVal xb As Double, ByVal ya As Double, ByVal yb As Double)
        Try
            alglib.spline2dbuildersetarea(state.csobj, xa, xb, ya, yb)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetgrid(ByRef state As spline2dbuilder, ByVal kx As Integer, ByVal ky As Integer)
        Try
            alglib.spline2dbuildersetgrid(state.csobj, kx, ky)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetalgofastddm(ByRef state As spline2dbuilder, ByVal nlayers As Integer, ByVal lambdav As Double)
        Try
            alglib.spline2dbuildersetalgofastddm(state.csobj, nlayers, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetalgoblocklls(ByRef state As spline2dbuilder, ByVal lambdans As Double)
        Try
            alglib.spline2dbuildersetalgoblocklls(state.csobj, lambdans)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dbuildersetalgonaivells(ByRef state As spline2dbuilder, ByVal lambdans As Double)
        Try
            alglib.spline2dbuildersetalgonaivells(state.csobj, lambdans)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline2dfit(ByRef state As spline2dbuilder, ByRef s As spline2dinterpolant, ByRef rep As spline2dfitreport)
        Try
            s = New spline2dinterpolant()
            rep = New spline2dfitreport()
            alglib.spline2dfit(state.csobj, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Class spline3dinterpolant
        Public csobj As alglib.spline3dinterpolant
    End Class


    Public Function spline3dcalc(ByVal c As spline3dinterpolant, ByVal x As Double, ByVal y As Double, ByVal z As Double) As Double
        Try
            spline3dcalc = alglib.spline3dcalc(c.csobj, x, y, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub spline3dlintransxyz(ByRef c As spline3dinterpolant, ByVal ax As Double, ByVal bx As Double, ByVal ay As Double, ByVal by As Double, ByVal az As Double, ByVal bz As Double)
        Try
            alglib.spline3dlintransxyz(c.csobj, ax, bx, ay, by, az, bz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dlintransf(ByRef c As spline3dinterpolant, ByVal a As Double, ByVal b As Double)
        Try
            alglib.spline3dlintransf(c.csobj, a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dresampletrilinear(ByVal a() As Double, ByVal oldzcount As Integer, ByVal oldycount As Integer, ByVal oldxcount As Integer, ByVal newzcount As Integer, ByVal newycount As Integer, ByVal newxcount As Integer, ByRef b() As Double)
        Try
            alglib.spline3dresampletrilinear(a, oldzcount, oldycount, oldxcount, newzcount, newycount, newxcount, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dbuildtrilinearv(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal z() As Double, ByVal l As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline3dinterpolant)
        Try
            c = New spline3dinterpolant()
            alglib.spline3dbuildtrilinearv(x, n, y, m, z, l, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dbuildtrilinearvbuf(ByVal x() As Double, ByVal n As Integer, ByVal y() As Double, ByVal m As Integer, ByVal z() As Double, ByVal l As Integer, ByVal f() As Double, ByVal d As Integer, ByRef c As spline3dinterpolant)
        Try
            alglib.spline3dbuildtrilinearvbuf(x, n, y, m, z, l, f, d, c.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dcalcvbuf(ByVal c As spline3dinterpolant, ByVal x As Double, ByVal y As Double, ByVal z As Double, ByRef f() As Double)
        Try
            alglib.spline3dcalcvbuf(c.csobj, x, y, z, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dcalcv(ByVal c As spline3dinterpolant, ByVal x As Double, ByVal y As Double, ByVal z As Double, ByRef f() As Double)
        Try
            alglib.spline3dcalcv(c.csobj, x, y, z, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline3dunpackv(ByVal c As spline3dinterpolant, ByRef n As Integer, ByRef m As Integer, ByRef l As Integer, ByRef d As Integer, ByRef stype As Integer, ByRef tbl(,) As Double)
        Try
            alglib.spline3dunpackv(c.csobj, n, m, l, d, stype, tbl)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub nsfitspheremcc(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rhi As Double)
        Try
            alglib.nsfitspheremcc(xy, npoints, nx, cx, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nsfitspheremic(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rlo As Double)
        Try
            alglib.nsfitspheremic(xy, npoints, nx, cx, rlo)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nsfitspheremzc(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByRef cx() As Double, ByRef rlo As Double, ByRef rhi As Double)
        Try
            alglib.nsfitspheremzc(xy, npoints, nx, cx, rlo, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub nsfitspherex(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nx As Integer, ByVal problemtype As Integer, ByVal epsx As Double, ByVal aulits As Integer, ByVal penalty As Double, ByRef cx() As Double, ByRef rlo As Double, ByRef rhi As Double)
        Try
            alglib.nsfitspherex(xy, npoints, nx, problemtype, epsx, aulits, penalty, cx, rlo, rhi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitpenalized(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByVal rho As Double, ByRef info As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitpenalized(x, y, n, m, rho, info, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitpenalized(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByVal rho As Double, ByRef info As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitpenalized(x, y, m, rho, info, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitpenalizedw(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal n As Integer, ByVal m As Integer, ByVal rho As Double, ByRef info As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitpenalizedw(x, y, w, n, m, rho, info, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitpenalizedw(ByVal x() As Double, ByVal y() As Double, ByVal w() As Double, ByVal m As Integer, ByVal rho As Double, ByRef info As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitpenalizedw(x, y, w, m, rho, info, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitcubic(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitcubic(x, y, n, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfitcubic(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfitcubic(x, y, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermite(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermite(x, y, n, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub spline1dfithermite(ByVal x() As Double, ByVal y() As Double, ByVal m As Integer, ByRef s As spline1dinterpolant, ByRef rep As spline1dfitreport)
        Try
            s = New spline1dinterpolant()
            rep = New spline1dfitreport()
            alglib.spline1dfithermite(x, y, m, s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class rbfcalcbuffer
        Public csobj As alglib.rbfcalcbuffer
    End Class
    Public Class rbfmodel
        Public csobj As alglib.rbfmodel
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'RBF solution report:
    '* TerminationType   -   termination type, positive values - success,
    '                        non-positive - failure.
    '
    'Fields which are set by modern RBF solvers (hierarchical):
    '* RMSError          -   root-mean-square error; NAN for old solvers (ML, QNN)
    '* MaxError          -   maximum error; NAN for old solvers (ML, QNN)
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class rbfreport
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property maxerror() As Double
        Get
            Return csobj.maxerror
        End Get
        Set(ByVal Value As Double)
            csobj.maxerror = Value
        End Set
        End Property
        Public Property arows() As Integer
        Get
            Return csobj.arows
        End Get
        Set(ByVal Value As Integer)
            csobj.arows = Value
        End Set
        End Property
        Public Property acols() As Integer
        Get
            Return csobj.acols
        End Get
        Set(ByVal Value As Integer)
            csobj.acols = Value
        End Set
        End Property
        Public Property annz() As Integer
        Get
            Return csobj.annz
        End Get
        Set(ByVal Value As Integer)
            csobj.annz = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property nmv() As Integer
        Get
            Return csobj.nmv
        End Get
        Set(ByVal Value As Integer)
            csobj.nmv = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.rbfreport
    End Class
    Public Sub rbfserialize(ByVal obj As rbfmodel, ByRef s_out As String)
        Try
            alglib.rbfserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub rbfunserialize(ByVal s_in As String, ByRef obj As rbfmodel)
        Try
            alglib.rbfunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfcreate(ByVal nx As Integer, ByVal ny As Integer, ByRef s As rbfmodel)
        Try
            s = New rbfmodel()
            alglib.rbfcreate(nx, ny, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfcreatecalcbuffer(ByVal s As rbfmodel, ByRef buf As rbfcalcbuffer)
        Try
            buf = New rbfcalcbuffer()
            alglib.rbfcreatecalcbuffer(s.csobj, buf.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetpoints(ByRef s As rbfmodel, ByVal xy(,) As Double, ByVal n As Integer)
        Try
            alglib.rbfsetpoints(s.csobj, xy, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetpoints(ByRef s As rbfmodel, ByVal xy(,) As Double)
        Try
            alglib.rbfsetpoints(s.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetpointsandscales(ByRef r As rbfmodel, ByVal xy(,) As Double, ByVal n As Integer, ByVal s() As Double)
        Try
            alglib.rbfsetpointsandscales(r.csobj, xy, n, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetpointsandscales(ByRef r As rbfmodel, ByVal xy(,) As Double, ByVal s() As Double)
        Try
            alglib.rbfsetpointsandscales(r.csobj, xy, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgoqnn(ByRef s As rbfmodel, ByVal q As Double, ByVal z As Double)
        Try
            alglib.rbfsetalgoqnn(s.csobj, q, z)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgoqnn(ByRef s As rbfmodel)
        Try
            alglib.rbfsetalgoqnn(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultilayer(ByRef s As rbfmodel, ByVal rbase As Double, ByVal nlayers As Integer, ByVal lambdav As Double)
        Try
            alglib.rbfsetalgomultilayer(s.csobj, rbase, nlayers, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultilayer(ByRef s As rbfmodel, ByVal rbase As Double, ByVal nlayers As Integer)
        Try
            alglib.rbfsetalgomultilayer(s.csobj, rbase, nlayers)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgohierarchical(ByRef s As rbfmodel, ByVal rbase As Double, ByVal nlayers As Integer, ByVal lambdans As Double)
        Try
            alglib.rbfsetalgohierarchical(s.csobj, rbase, nlayers, lambdans)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgothinplatespline(ByRef s As rbfmodel, ByVal lambdav As Double)
        Try
            alglib.rbfsetalgothinplatespline(s.csobj, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgothinplatespline(ByRef s As rbfmodel)
        Try
            alglib.rbfsetalgothinplatespline(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultiquadricmanual(ByRef s As rbfmodel, ByVal alpha As Double, ByVal lambdav As Double)
        Try
            alglib.rbfsetalgomultiquadricmanual(s.csobj, alpha, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultiquadricmanual(ByRef s As rbfmodel, ByVal alpha As Double)
        Try
            alglib.rbfsetalgomultiquadricmanual(s.csobj, alpha)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultiquadricauto(ByRef s As rbfmodel, ByVal lambdav As Double)
        Try
            alglib.rbfsetalgomultiquadricauto(s.csobj, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgomultiquadricauto(ByRef s As rbfmodel)
        Try
            alglib.rbfsetalgomultiquadricauto(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgobiharmonic(ByRef s As rbfmodel, ByVal lambdav As Double)
        Try
            alglib.rbfsetalgobiharmonic(s.csobj, lambdav)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetalgobiharmonic(ByRef s As rbfmodel)
        Try
            alglib.rbfsetalgobiharmonic(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetlinterm(ByRef s As rbfmodel)
        Try
            alglib.rbfsetlinterm(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetconstterm(ByRef s As rbfmodel)
        Try
            alglib.rbfsetconstterm(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetzeroterm(ByRef s As rbfmodel)
        Try
            alglib.rbfsetzeroterm(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetv2bf(ByRef s As rbfmodel, ByVal bf As Integer)
        Try
            alglib.rbfsetv2bf(s.csobj, bf)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetv2its(ByRef s As rbfmodel, ByVal maxits As Integer)
        Try
            alglib.rbfsetv2its(s.csobj, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetv2supportr(ByRef s As rbfmodel, ByVal r As Double)
        Try
            alglib.rbfsetv2supportr(s.csobj, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetv3tol(ByRef s As rbfmodel, ByVal tol As Double)
        Try
            alglib.rbfsetv3tol(s.csobj, tol)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfbuildmodel(ByRef s As rbfmodel, ByRef rep As rbfreport)
        Try
            rep = New rbfreport()
            alglib.rbfbuildmodel(s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rbfcalc1(ByRef s As rbfmodel, ByVal x0 As Double) As Double
        Try
            rbfcalc1 = alglib.rbfcalc1(s.csobj, x0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rbfcalc2(ByRef s As rbfmodel, ByVal x0 As Double, ByVal x1 As Double) As Double
        Try
            rbfcalc2 = alglib.rbfcalc2(s.csobj, x0, x1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rbfcalc3(ByRef s As rbfmodel, ByVal x0 As Double, ByVal x1 As Double, ByVal x2 As Double) As Double
        Try
            rbfcalc3 = alglib.rbfcalc3(s.csobj, x0, x1, x2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rbfdiff1(ByRef s As rbfmodel, ByVal x0 As Double, ByRef y As Double, ByRef dy0 As Double)
        Try
            alglib.rbfdiff1(s.csobj, x0, y, dy0)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfdiff2(ByRef s As rbfmodel, ByVal x0 As Double, ByVal x1 As Double, ByRef y As Double, ByRef dy0 As Double, ByRef dy1 As Double)
        Try
            alglib.rbfdiff2(s.csobj, x0, x1, y, dy0, dy1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfdiff3(ByRef s As rbfmodel, ByVal x0 As Double, ByVal x1 As Double, ByVal x2 As Double, ByRef y As Double, ByRef dy0 As Double, ByRef dy1 As Double, ByRef dy2 As Double)
        Try
            alglib.rbfdiff3(s.csobj, x0, x1, x2, y, dy0, dy1, dy2)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfsetfastevaltol(ByRef s As rbfmodel, ByVal tol As Double)
        Try
            alglib.rbfsetfastevaltol(s.csobj, tol)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbffastcalc(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.rbffastcalc(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfcalc(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.rbfcalc(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfdiff(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double)
        Try
            alglib.rbfdiff(s.csobj, x, y, dy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfhess(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double, ByRef d2y() As Double)
        Try
            alglib.rbfhess(s.csobj, x, y, dy, d2y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfcalcbuf(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.rbfcalcbuf(s.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfdiffbuf(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double)
        Try
            alglib.rbfdiffbuf(s.csobj, x, y, dy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfhessbuf(ByRef s As rbfmodel, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double, ByRef d2y() As Double)
        Try
            alglib.rbfhessbuf(s.csobj, x, y, dy, d2y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbftscalcbuf(ByVal s As rbfmodel, ByRef buf As rbfcalcbuffer, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.rbftscalcbuf(s.csobj, buf.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbftsdiffbuf(ByVal s As rbfmodel, ByRef buf As rbfcalcbuffer, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double)
        Try
            alglib.rbftsdiffbuf(s.csobj, buf.csobj, x, y, dy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbftshessbuf(ByVal s As rbfmodel, ByRef buf As rbfcalcbuffer, ByVal x() As Double, ByRef y() As Double, ByRef dy() As Double, ByRef d2y() As Double)
        Try
            alglib.rbftshessbuf(s.csobj, buf.csobj, x, y, dy, d2y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfgridcalc2(ByRef s As rbfmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByRef y(,) As Double)
        Try
            alglib.rbfgridcalc2(s.csobj, x0, n0, x1, n1, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfgridcalc2v(ByVal s As rbfmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByRef y() As Double)
        Try
            alglib.rbfgridcalc2v(s.csobj, x0, n0, x1, n1, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfgridcalc2vsubset(ByVal s As rbfmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByVal flagy() As Boolean, ByRef y() As Double)
        Try
            alglib.rbfgridcalc2vsubset(s.csobj, x0, n0, x1, n1, flagy, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfgridcalc3v(ByVal s As rbfmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByVal x2() As Double, ByVal n2 As Integer, ByRef y() As Double)
        Try
            alglib.rbfgridcalc3v(s.csobj, x0, n0, x1, n1, x2, n2, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfgridcalc3vsubset(ByVal s As rbfmodel, ByVal x0() As Double, ByVal n0 As Integer, ByVal x1() As Double, ByVal n1 As Integer, ByVal x2() As Double, ByVal n2 As Integer, ByVal flagy() As Boolean, ByRef y() As Double)
        Try
            alglib.rbfgridcalc3vsubset(s.csobj, x0, n0, x1, n1, x2, n2, flagy, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub rbfunpack(ByRef s As rbfmodel, ByRef nx As Integer, ByRef ny As Integer, ByRef xwr(,) As Double, ByRef nc As Integer, ByRef v(,) As Double, ByRef modelversion As Integer)
        Try
            alglib.rbfunpack(s.csobj, nx, ny, xwr, nc, v, modelversion)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function rbfgetmodelversion(ByRef s As rbfmodel) As Integer
        Try
            rbfgetmodelversion = alglib.rbfgetmodelversion(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function rbfpeekprogress(ByVal s As rbfmodel) As Double
        Try
            rbfpeekprogress = alglib.rbfpeekprogress(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub rbfrequesttermination(ByRef s As rbfmodel)
        Try
            alglib.rbfrequesttermination(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub fftc1d(ByRef a() As alglib.complex, ByVal n As Integer)
        Try
            alglib.fftc1d(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftc1d(ByRef a() As alglib.complex)
        Try
            alglib.fftc1d(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftc1dinv(ByRef a() As alglib.complex, ByVal n As Integer)
        Try
            alglib.fftc1dinv(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftc1dinv(ByRef a() As alglib.complex)
        Try
            alglib.fftc1dinv(a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1d(ByVal a() As Double, ByVal n As Integer, ByRef f() As alglib.complex)
        Try
            alglib.fftr1d(a, n, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1d(ByVal a() As Double, ByRef f() As alglib.complex)
        Try
            alglib.fftr1d(a, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dbuf(ByVal a() As Double, ByVal n As Integer, ByRef f() As alglib.complex)
        Try
            alglib.fftr1dbuf(a, n, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dbuf(ByVal a() As Double, ByRef f() As alglib.complex)
        Try
            alglib.fftr1dbuf(a, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dinv(ByVal f() As alglib.complex, ByVal n As Integer, ByRef a() As Double)
        Try
            alglib.fftr1dinv(f, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dinv(ByVal f() As alglib.complex, ByRef a() As Double)
        Try
            alglib.fftr1dinv(f, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dinvbuf(ByVal f() As alglib.complex, ByVal n As Integer, ByRef a() As Double)
        Try
            alglib.fftr1dinvbuf(f, n, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fftr1dinvbuf(ByVal f() As alglib.complex, ByRef a() As Double)
        Try
            alglib.fftr1dinvbuf(f, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub fhtr1d(ByRef a() As Double, ByVal n As Integer)
        Try
            alglib.fhtr1d(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fhtr1dinv(ByRef a() As Double, ByVal n As Integer)
        Try
            alglib.fhtr1dinv(a, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub convc1d(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1d(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dbuf(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1dbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dinv(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1dinv(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dinvbuf(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1dinvbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dcircular(ByVal s() As alglib.complex, ByVal m As Integer, ByVal r() As alglib.complex, ByVal n As Integer, ByRef c() As alglib.complex)
        Try
            alglib.convc1dcircular(s, m, r, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dcircularbuf(ByVal s() As alglib.complex, ByVal m As Integer, ByVal r() As alglib.complex, ByVal n As Integer, ByRef c() As alglib.complex)
        Try
            alglib.convc1dcircularbuf(s, m, r, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dcircularinv(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1dcircularinv(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convc1dcircularinvbuf(ByVal a() As alglib.complex, ByVal m As Integer, ByVal b() As alglib.complex, ByVal n As Integer, ByRef r() As alglib.complex)
        Try
            alglib.convc1dcircularinvbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1d(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1d(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dbuf(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1dbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dinv(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1dinv(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dinvbuf(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1dinvbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dcircular(ByVal s() As Double, ByVal m As Integer, ByVal r() As Double, ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.convr1dcircular(s, m, r, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dcircularbuf(ByVal s() As Double, ByVal m As Integer, ByVal r() As Double, ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.convr1dcircularbuf(s, m, r, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dcircularinv(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1dcircularinv(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub convr1dcircularinvbuf(ByVal a() As Double, ByVal m As Integer, ByVal b() As Double, ByVal n As Integer, ByRef r() As Double)
        Try
            alglib.convr1dcircularinvbuf(a, m, b, n, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub corrc1d(ByVal signal() As alglib.complex, ByVal n As Integer, ByVal pattern() As alglib.complex, ByVal m As Integer, ByRef r() As alglib.complex)
        Try
            alglib.corrc1d(signal, n, pattern, m, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrc1dbuf(ByVal signal() As alglib.complex, ByVal n As Integer, ByVal pattern() As alglib.complex, ByVal m As Integer, ByRef r() As alglib.complex)
        Try
            alglib.corrc1dbuf(signal, n, pattern, m, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrc1dcircular(ByVal signal() As alglib.complex, ByVal m As Integer, ByVal pattern() As alglib.complex, ByVal n As Integer, ByRef c() As alglib.complex)
        Try
            alglib.corrc1dcircular(signal, m, pattern, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrc1dcircularbuf(ByVal signal() As alglib.complex, ByVal m As Integer, ByVal pattern() As alglib.complex, ByVal n As Integer, ByRef c() As alglib.complex)
        Try
            alglib.corrc1dcircularbuf(signal, m, pattern, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrr1d(ByVal signal() As Double, ByVal n As Integer, ByVal pattern() As Double, ByVal m As Integer, ByRef r() As Double)
        Try
            alglib.corrr1d(signal, n, pattern, m, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrr1dbuf(ByVal signal() As Double, ByVal n As Integer, ByVal pattern() As Double, ByVal m As Integer, ByRef r() As Double)
        Try
            alglib.corrr1dbuf(signal, n, pattern, m, r)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrr1dcircular(ByVal signal() As Double, ByVal m As Integer, ByVal pattern() As Double, ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.corrr1dcircular(signal, m, pattern, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub corrr1dcircularbuf(ByVal signal() As Double, ByVal m As Integer, ByVal pattern() As Double, ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.corrr1dcircularbuf(signal, m, pattern, n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function exponentialintegralei(ByVal x As Double) As Double
        Try
            exponentialintegralei = alglib.exponentialintegralei(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function exponentialintegralen(ByVal x As Double, ByVal n As Integer) As Double
        Try
            exponentialintegralen = alglib.exponentialintegralen(x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub jacobianellipticfunctions(ByVal u As Double, ByVal m As Double, ByRef sn As Double, ByRef cn As Double, ByRef dn As Double, ByRef ph As Double)
        Try
            alglib.jacobianellipticfunctions(u, m, sn, cn, dn, ph)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub sinecosineintegrals(ByVal x As Double, ByRef si As Double, ByRef ci As Double)
        Try
            alglib.sinecosineintegrals(x, si, ci)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub hyperbolicsinecosineintegrals(ByVal x As Double, ByRef shi As Double, ByRef chi As Double)
        Try
            alglib.hyperbolicsinecosineintegrals(x, shi, chi)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function chebyshevcalculate(ByVal r As Integer, ByVal n As Integer, ByVal x As Double) As Double
        Try
            chebyshevcalculate = alglib.chebyshevcalculate(r, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function chebyshevsum(ByVal c() As Double, ByVal r As Integer, ByVal n As Integer, ByVal x As Double) As Double
        Try
            chebyshevsum = alglib.chebyshevsum(c, r, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub chebyshevcoefficients(ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.chebyshevcoefficients(n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fromchebyshev(ByVal a() As Double, ByVal n As Integer, ByRef b() As Double)
        Try
            alglib.fromchebyshev(a, n, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function poissondistribution(ByVal k As Integer, ByVal m As Double) As Double
        Try
            poissondistribution = alglib.poissondistribution(k, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function poissoncdistribution(ByVal k As Integer, ByVal m As Double) As Double
        Try
            poissoncdistribution = alglib.poissoncdistribution(k, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function invpoissondistribution(ByVal k As Integer, ByVal y As Double) As Double
        Try
            invpoissondistribution = alglib.invpoissondistribution(k, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function beta(ByVal a As Double, ByVal b As Double) As Double
        Try
            beta = alglib.beta(a, b)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub fresnelintegral(ByVal x As Double, ByRef c As Double, ByRef s As Double)
        Try
            alglib.fresnelintegral(x, c, s)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function psi(ByVal x As Double) As Double
        Try
            psi = alglib.psi(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub airy(ByVal x As Double, ByRef ai As Double, ByRef aip As Double, ByRef bi As Double, ByRef bip As Double)
        Try
            alglib.airy(x, ai, aip, bi, bip)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function dawsonintegral(ByVal x As Double) As Double
        Try
            dawsonintegral = alglib.dawsonintegral(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function hermitecalculate(ByVal n As Integer, ByVal x As Double) As Double
        Try
            hermitecalculate = alglib.hermitecalculate(n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function hermitesum(ByVal c() As Double, ByVal n As Integer, ByVal x As Double) As Double
        Try
            hermitesum = alglib.hermitesum(c, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub hermitecoefficients(ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.hermitecoefficients(n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function legendrecalculate(ByVal n As Integer, ByVal x As Double) As Double
        Try
            legendrecalculate = alglib.legendrecalculate(n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function legendresum(ByVal c() As Double, ByVal n As Integer, ByVal x As Double) As Double
        Try
            legendresum = alglib.legendresum(c, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub legendrecoefficients(ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.legendrecoefficients(n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function besselj0(ByVal x As Double) As Double
        Try
            besselj0 = alglib.besselj0(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besselj1(ByVal x As Double) As Double
        Try
            besselj1 = alglib.besselj1(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besseljn(ByVal n As Integer, ByVal x As Double) As Double
        Try
            besseljn = alglib.besseljn(n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function bessely0(ByVal x As Double) As Double
        Try
            bessely0 = alglib.bessely0(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function bessely1(ByVal x As Double) As Double
        Try
            bessely1 = alglib.bessely1(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besselyn(ByVal n As Integer, ByVal x As Double) As Double
        Try
            besselyn = alglib.besselyn(n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besseli0(ByVal x As Double) As Double
        Try
            besseli0 = alglib.besseli0(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besseli1(ByVal x As Double) As Double
        Try
            besseli1 = alglib.besseli1(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besselk0(ByVal x As Double) As Double
        Try
            besselk0 = alglib.besselk0(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besselk1(ByVal x As Double) As Double
        Try
            besselk1 = alglib.besselk1(x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function besselkn(ByVal nn As Integer, ByVal x As Double) As Double
        Try
            besselkn = alglib.besselkn(nn, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Function laguerrecalculate(ByVal n As Integer, ByVal x As Double) As Double
        Try
            laguerrecalculate = alglib.laguerrecalculate(n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function laguerresum(ByVal c() As Double, ByVal n As Integer, ByVal x As Double) As Double
        Try
            laguerresum = alglib.laguerresum(c, n, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub laguerrecoefficients(ByVal n As Integer, ByRef c() As Double)
        Try
            alglib.laguerrecoefficients(n, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Function ellipticintegralk(ByVal m As Double) As Double
        Try
            ellipticintegralk = alglib.ellipticintegralk(m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function ellipticintegralkhighprecision(ByVal m1 As Double) As Double
        Try
            ellipticintegralkhighprecision = alglib.ellipticintegralkhighprecision(m1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function incompleteellipticintegralk(ByVal phi As Double, ByVal m As Double) As Double
        Try
            incompleteellipticintegralk = alglib.incompleteellipticintegralk(phi, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function ellipticintegrale(ByVal m As Double) As Double
        Try
            ellipticintegrale = alglib.ellipticintegrale(m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function incompleteellipticintegrale(ByVal phi As Double, ByVal m As Double) As Double
        Try
            incompleteellipticintegrale = alglib.incompleteellipticintegrale(phi, m)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub pcabuildbasis(ByVal x(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByRef s2() As Double, ByRef v(,) As Double)
        Try
            alglib.pcabuildbasis(x, npoints, nvars, s2, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pcabuildbasis(ByVal x(,) As Double, ByRef s2() As Double, ByRef v(,) As Double)
        Try
            alglib.pcabuildbasis(x, s2, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pcatruncatedsubspace(ByVal x(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nneeded As Integer, ByVal eps As Double, ByVal maxits As Integer, ByRef s2() As Double, ByRef v(,) As Double)
        Try
            alglib.pcatruncatedsubspace(x, npoints, nvars, nneeded, eps, maxits, s2, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pcatruncatedsubspace(ByVal x(,) As Double, ByVal nneeded As Integer, ByVal eps As Double, ByVal maxits As Integer, ByRef s2() As Double, ByRef v(,) As Double)
        Try
            alglib.pcatruncatedsubspace(x, nneeded, eps, maxits, s2, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub pcatruncatedsubspacesparse(ByVal x As sparsematrix, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nneeded As Integer, ByVal eps As Double, ByVal maxits As Integer, ByRef s2() As Double, ByRef v(,) As Double)
        Try
            alglib.pcatruncatedsubspacesparse(x.csobj, npoints, nvars, nneeded, eps, maxits, s2, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub dsoptimalsplit2(ByVal a() As Double, ByVal c() As Integer, ByVal n As Integer, ByRef info As Integer, ByRef threshold As Double, ByRef pal As Double, ByRef pbl As Double, ByRef par As Double, ByRef pbr As Double, ByRef cve As Double)
        Try
            alglib.dsoptimalsplit2(a, c, n, info, threshold, pal, pbl, par, pbr, cve)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dsoptimalsplit2fast(ByRef a() As Double, ByRef c() As Integer, ByRef tiesbuf() As Integer, ByRef cntbuf() As Integer, ByRef bufr() As Double, ByRef bufi() As Integer, ByVal n As Integer, ByVal nc As Integer, ByVal alpha As Double, ByRef info As Integer, ByRef threshold As Double, ByRef rms As Double, ByRef cvrms As Double)
        Try
            alglib.dsoptimalsplit2fast(a, c, tiesbuf, cntbuf, bufr, bufi, n, nc, alpha, info, threshold, rms, cvrms)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Model's errors:
    '    * RelCLSError   -   fraction of misclassified cases.
    '    * AvgCE         -   acerage cross-entropy
    '    * RMSError      -   root-mean-square error
    '    * AvgError      -   average error
    '    * AvgRelError   -   average relative error
    '
    'NOTE 1: RelCLSError/AvgCE are zero on regression problems.
    '
    'NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
    '        errors in prediction of posterior probabilities
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class modelerrors
        Public Property relclserror() As Double
        Get
            Return csobj.relclserror
        End Get
        Set(ByVal Value As Double)
            csobj.relclserror = Value
        End Set
        End Property
        Public Property avgce() As Double
        Get
            Return csobj.avgce
        End Get
        Set(ByVal Value As Double)
            csobj.avgce = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public csobj As alglib.modelerrors
    End Class
    Public Class multilayerperceptron
        Public csobj As alglib.multilayerperceptron
    End Class
    Public Sub mlpserialize(ByVal obj As multilayerperceptron, ByRef s_out As String)
        Try
            alglib.mlpserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub mlpunserialize(ByVal s_in As String, ByRef obj As multilayerperceptron)
        Try
            alglib.mlpunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreate0(ByVal nin As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreate0(nin, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreate1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreate1(nin, nhid, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreate2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreate2(nin, nhid1, nhid2, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreateb0(ByVal nin As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreateb0(nin, nout, b, d, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreateb1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreateb1(nin, nhid, nout, b, d, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreateb2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreateb2(nin, nhid1, nhid2, nout, b, d, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreater0(ByVal nin As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreater0(nin, nout, a, b, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreater1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreater1(nin, nhid, nout, a, b, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreater2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreater2(nin, nhid1, nhid2, nout, a, b, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreatec0(ByVal nin As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreatec0(nin, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreatec1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreatec1(nin, nhid, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreatec2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByRef network As multilayerperceptron)
        Try
            network = New multilayerperceptron()
            alglib.mlpcreatec2(nin, nhid1, nhid2, nout, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcopy(ByVal network1 As multilayerperceptron, ByRef network2 As multilayerperceptron)
        Try
            network2 = New multilayerperceptron()
            alglib.mlpcopy(network1.csobj, network2.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcopytunableparameters(ByVal network1 As multilayerperceptron, ByRef network2 As multilayerperceptron)
        Try
            alglib.mlpcopytunableparameters(network1.csobj, network2.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlprandomize(ByRef network As multilayerperceptron)
        Try
            alglib.mlprandomize(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlprandomizefull(ByRef network As multilayerperceptron)
        Try
            alglib.mlprandomizefull(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpinitpreprocessor(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer)
        Try
            alglib.mlpinitpreprocessor(network.csobj, xy, ssize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpproperties(ByVal network As multilayerperceptron, ByRef nin As Integer, ByRef nout As Integer, ByRef wcount As Integer)
        Try
            alglib.mlpproperties(network.csobj, nin, nout, wcount)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlpgetinputscount(ByVal network As multilayerperceptron) As Integer
        Try
            mlpgetinputscount = alglib.mlpgetinputscount(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpgetoutputscount(ByVal network As multilayerperceptron) As Integer
        Try
            mlpgetoutputscount = alglib.mlpgetoutputscount(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpgetweightscount(ByVal network As multilayerperceptron) As Integer
        Try
            mlpgetweightscount = alglib.mlpgetweightscount(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpissoftmax(ByVal network As multilayerperceptron) As Boolean
        Try
            mlpissoftmax = alglib.mlpissoftmax(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpgetlayerscount(ByVal network As multilayerperceptron) As Integer
        Try
            mlpgetlayerscount = alglib.mlpgetlayerscount(network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpgetlayersize(ByVal network As multilayerperceptron, ByVal k As Integer) As Integer
        Try
            mlpgetlayersize = alglib.mlpgetlayersize(network.csobj, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub mlpgetinputscaling(ByVal network As multilayerperceptron, ByVal i As Integer, ByRef mean As Double, ByRef sigma As Double)
        Try
            alglib.mlpgetinputscaling(network.csobj, i, mean, sigma)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgetoutputscaling(ByVal network As multilayerperceptron, ByVal i As Integer, ByRef mean As Double, ByRef sigma As Double)
        Try
            alglib.mlpgetoutputscaling(network.csobj, i, mean, sigma)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgetneuroninfo(ByRef network As multilayerperceptron, ByVal k As Integer, ByVal i As Integer, ByRef fkind As Integer, ByRef threshold As Double)
        Try
            alglib.mlpgetneuroninfo(network.csobj, k, i, fkind, threshold)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlpgetweight(ByRef network As multilayerperceptron, ByVal k0 As Integer, ByVal i0 As Integer, ByVal k1 As Integer, ByVal i1 As Integer) As Double
        Try
            mlpgetweight = alglib.mlpgetweight(network.csobj, k0, i0, k1, i1)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub mlpsetinputscaling(ByRef network As multilayerperceptron, ByVal i As Integer, ByVal mean As Double, ByVal sigma As Double)
        Try
            alglib.mlpsetinputscaling(network.csobj, i, mean, sigma)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetoutputscaling(ByRef network As multilayerperceptron, ByVal i As Integer, ByVal mean As Double, ByVal sigma As Double)
        Try
            alglib.mlpsetoutputscaling(network.csobj, i, mean, sigma)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetneuroninfo(ByRef network As multilayerperceptron, ByVal k As Integer, ByVal i As Integer, ByVal fkind As Integer, ByVal threshold As Double)
        Try
            alglib.mlpsetneuroninfo(network.csobj, k, i, fkind, threshold)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetweight(ByRef network As multilayerperceptron, ByVal k0 As Integer, ByVal i0 As Integer, ByVal k1 As Integer, ByVal i1 As Integer, ByVal w As Double)
        Try
            alglib.mlpsetweight(network.csobj, k0, i0, k1, i1, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpactivationfunction(ByVal net As Double, ByVal k As Integer, ByRef f As Double, ByRef df As Double, ByRef d2f As Double)
        Try
            alglib.mlpactivationfunction(net, k, f, df, d2f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpprocess(ByRef network As multilayerperceptron, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mlpprocess(network.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpprocessi(ByRef network As multilayerperceptron, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mlpprocessi(network.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlperror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlperror = alglib.mlperror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlperrorsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlperrorsparse = alglib.mlperrorsparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlperrorn(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer) As Double
        Try
            mlperrorn = alglib.mlperrorn(network.csobj, xy, ssize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpclserror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Integer
        Try
            mlpclserror = alglib.mlpclserror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlprelclserror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlprelclserror = alglib.mlprelclserror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlprelclserrorsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlprelclserrorsparse = alglib.mlprelclserrorsparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgce(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpavgce = alglib.mlpavgce(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgcesparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlpavgcesparse = alglib.mlpavgcesparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlprmserror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlprmserror = alglib.mlprmserror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlprmserrorsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlprmserrorsparse = alglib.mlprmserrorsparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgerror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpavgerror = alglib.mlpavgerror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgerrorsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlpavgerrorsparse = alglib.mlpavgerrorsparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgrelerror(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpavgrelerror = alglib.mlpavgrelerror(network.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpavgrelerrorsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal npoints As Integer) As Double
        Try
            mlpavgrelerrorsparse = alglib.mlpavgrelerrorsparse(network.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub mlpgrad(ByRef network As multilayerperceptron, ByVal x() As Double, ByVal desiredy() As Double, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgrad(network.csobj, x, desiredy, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradn(ByRef network As multilayerperceptron, ByVal x() As Double, ByVal desiredy() As Double, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradn(network.csobj, x, desiredy, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradbatch(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradbatch(network.csobj, xy, ssize, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradbatchsparse(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal ssize As Integer, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradbatchsparse(network.csobj, xy.csobj, ssize, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradbatchsubset(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal setsize As Integer, ByVal idx() As Integer, ByVal subsetsize As Integer, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradbatchsubset(network.csobj, xy, setsize, idx, subsetsize, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradbatchsparsesubset(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal setsize As Integer, ByVal idx() As Integer, ByVal subsetsize As Integer, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradbatchsparsesubset(network.csobj, xy.csobj, setsize, idx, subsetsize, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpgradnbatch(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer, ByRef e As Double, ByRef grad() As Double)
        Try
            alglib.mlpgradnbatch(network.csobj, xy, ssize, e, grad)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlphessiannbatch(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer, ByRef e As Double, ByRef grad() As Double, ByRef h(,) As Double)
        Try
            alglib.mlphessiannbatch(network.csobj, xy, ssize, e, grad, h)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlphessianbatch(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal ssize As Integer, ByRef e As Double, ByRef grad() As Double, ByRef h(,) As Double)
        Try
            alglib.mlphessianbatch(network.csobj, xy, ssize, e, grad, h)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpallerrorssubset(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal setsize As Integer, ByVal subset() As Integer, ByVal subsetsize As Integer, ByRef rep As modelerrors)
        Try
            rep = New modelerrors()
            alglib.mlpallerrorssubset(network.csobj, xy, setsize, subset, subsetsize, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpallerrorssparsesubset(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal setsize As Integer, ByVal subset() As Integer, ByVal subsetsize As Integer, ByRef rep As modelerrors)
        Try
            rep = New modelerrors()
            alglib.mlpallerrorssparsesubset(network.csobj, xy.csobj, setsize, subset, subsetsize, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlperrorsubset(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal setsize As Integer, ByVal subset() As Integer, ByVal subsetsize As Integer) As Double
        Try
            mlperrorsubset = alglib.mlperrorsubset(network.csobj, xy, setsize, subset, subsetsize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlperrorsparsesubset(ByRef network As multilayerperceptron, ByVal xy As sparsematrix, ByVal setsize As Integer, ByVal subset() As Integer, ByVal subsetsize As Integer) As Double
        Try
            mlperrorsparsesubset = alglib.mlperrorsparsesubset(network.csobj, xy.csobj, setsize, subset, subsetsize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    Public Class mlpensemble
        Public csobj As alglib.mlpensemble
    End Class
    Public Sub mlpeserialize(ByVal obj As mlpensemble, ByRef s_out As String)
        Try
            alglib.mlpeserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub mlpeunserialize(ByVal s_in As String, ByRef obj As mlpensemble)
        Try
            alglib.mlpeunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreate0(ByVal nin As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreate0(nin, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreate1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreate1(nin, nhid, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreate2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreate2(nin, nhid1, nhid2, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreateb0(ByVal nin As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreateb0(nin, nout, b, d, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreateb1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreateb1(nin, nhid, nout, b, d, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreateb2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal b As Double, ByVal d As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreateb2(nin, nhid1, nhid2, nout, b, d, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreater0(ByVal nin As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreater0(nin, nout, a, b, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreater1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreater1(nin, nhid, nout, a, b, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreater2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal a As Double, ByVal b As Double, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreater2(nin, nhid1, nhid2, nout, a, b, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreatec0(ByVal nin As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreatec0(nin, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreatec1(ByVal nin As Integer, ByVal nhid As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreatec1(nin, nhid, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreatec2(ByVal nin As Integer, ByVal nhid1 As Integer, ByVal nhid2 As Integer, ByVal nout As Integer, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreatec2(nin, nhid1, nhid2, nout, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpecreatefromnetwork(ByVal network As multilayerperceptron, ByVal ensemblesize As Integer, ByRef ensemble As mlpensemble)
        Try
            ensemble = New mlpensemble()
            alglib.mlpecreatefromnetwork(network.csobj, ensemblesize, ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlperandomize(ByRef ensemble As mlpensemble)
        Try
            alglib.mlperandomize(ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpeproperties(ByVal ensemble As mlpensemble, ByRef nin As Integer, ByRef nout As Integer)
        Try
            alglib.mlpeproperties(ensemble.csobj, nin, nout)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlpeissoftmax(ByVal ensemble As mlpensemble) As Boolean
        Try
            mlpeissoftmax = alglib.mlpeissoftmax(ensemble.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub mlpeprocess(ByRef ensemble As mlpensemble, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mlpeprocess(ensemble.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpeprocessi(ByRef ensemble As mlpensemble, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mlpeprocessi(ensemble.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlperelclserror(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlperelclserror = alglib.mlperelclserror(ensemble.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpeavgce(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpeavgce = alglib.mlpeavgce(ensemble.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpermserror(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpermserror = alglib.mlpermserror(ensemble.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpeavgerror(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpeavgerror = alglib.mlpeavgerror(ensemble.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mlpeavgrelerror(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mlpeavgrelerror = alglib.mlpeavgrelerror(ensemble.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    Public Class clusterizerstate
        Public csobj As alglib.clusterizerstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure  is used to store results of the agglomerative hierarchical
    'clustering (AHC).
    '
    'Following information is returned:
    '
    '* TerminationType - completion code:
    '  * 1   for successful completion of algorithm
    '  * -5  inappropriate combination of  clustering  algorithm  and  distance
    '        function was used. As for now, it  is  possible  only when  Ward's
    '        method is called for dataset with non-Euclidean distance function.
    '  In case negative completion code is returned,  other  fields  of  report
    '  structure are invalid and should not be used.
    '
    '* NPoints contains number of points in the original dataset
    '
    '* Z contains information about merges performed  (see below).  Z  contains
    '  indexes from the original (unsorted) dataset and it can be used when you
    '  need to know what points were merged. However, it is not convenient when
    '  you want to build a dendrograd (see below).
    '
    '* if  you  want  to  build  dendrogram, you  can use Z, but it is not good
    '  option, because Z contains  indexes from  unsorted  dataset.  Dendrogram
    '  built from such dataset is likely to have intersections. So, you have to
    '  reorder you points before building dendrogram.
    '  Permutation which reorders point is returned in P. Another representation
    '  of  merges,  which  is  more  convenient for dendorgram construction, is
    '  returned in PM.
    '
    '* more information on format of Z, P and PM can be found below and in the
    '  examples from ALGLIB Reference Manual.
    '
    'FORMAL DESCRIPTION OF FIELDS:
    '    NPoints         number of points
    '    Z               array[NPoints-1,2],  contains   indexes   of  clusters
    '                    linked in pairs to  form  clustering  tree.  I-th  row
    '                    corresponds to I-th merge:
    '                    * Z[I,0] - index of the first cluster to merge
    '                    * Z[I,1] - index of the second cluster to merge
    '                    * Z[I,0]<Z[I,1]
    '                    * clusters are  numbered  from 0 to 2*NPoints-2,  with
    '                      indexes from 0 to NPoints-1 corresponding to  points
    '                      of the original dataset, and indexes from NPoints to
    '                      2*NPoints-2  correspond  to  clusters  generated  by
    '                      subsequent  merges  (I-th  row  of Z creates cluster
    '                      with index NPoints+I).
    '
    '                    IMPORTANT: indexes in Z[] are indexes in the ORIGINAL,
    '                    unsorted dataset. In addition to  Z algorithm  outputs
    '                    permutation which rearranges points in such  way  that
    '                    subsequent merges are  performed  on  adjacent  points
    '                    (such order is needed if you want to build dendrogram).
    '                    However,  indexes  in  Z  are  related  to   original,
    '                    unrearranged sequence of points.
    '
    '    P               array[NPoints], permutation which reorders points  for
    '                    dendrogram  construction.  P[i] contains  index of the
    '                    position  where  we  should  move  I-th  point  of the
    '                    original dataset in order to apply merges PZ/PM.
    '
    '    PZ              same as Z, but for permutation of points given  by  P.
    '                    The  only  thing  which  changed  are  indexes  of the
    '                    original points; indexes of clusters remained same.
    '
    '    MergeDist       array[NPoints-1], contains distances between  clusters
    '                    being merged (MergeDist[i] correspond to merge  stored
    '                    in Z[i,...]):
    '                    * CLINK, SLINK and  average  linkage algorithms report
    '                      "raw", unmodified distance metric.
    '                    * Ward's   method   reports   weighted   intra-cluster
    '                      variance, which is equal to ||Ca-Cb||^2 * Sa*Sb/(Sa+Sb).
    '                      Here  A  and  B  are  clusters being merged, Ca is a
    '                      center of A, Cb is a center of B, Sa is a size of A,
    '                      Sb is a size of B.
    '
    '    PM              array[NPoints-1,6], another representation of  merges,
    '                    which is suited for dendrogram construction. It  deals
    '                    with rearranged points (permutation P is applied)  and
    '                    represents merges in a form which different  from  one
    '                    used by Z.
    '                    For each I from 0 to NPoints-2, I-th row of PM represents
    '                    merge performed on two clusters C0 and C1. Here:
    '                    * C0 contains points with indexes PM[I,0]...PM[I,1]
    '                    * C1 contains points with indexes PM[I,2]...PM[I,3]
    '                    * indexes stored in PM are given for dataset sorted
    '                      according to permutation P
    '                    * PM[I,1]=PM[I,2]-1 (only adjacent clusters are merged)
    '                    * PM[I,0]<=PM[I,1], PM[I,2]<=PM[I,3], i.e. both
    '                      clusters contain at least one point
    '                    * heights of "subdendrograms" corresponding  to  C0/C1
    '                      are stored in PM[I,4]  and  PM[I,5].  Subdendrograms
    '                      corresponding   to   single-point   clusters    have
    '                      height=0. Dendrogram of the merge result has  height
    '                      H=max(H0,H1)+1.
    '
    'NOTE: there is one-to-one correspondence between merges described by Z and
    '      PM. I-th row of Z describes same merge of clusters as I-th row of PM,
    '      with "left" cluster from Z corresponding to the "left" one from PM.
    '
    '  -- ALGLIB --
    '     Copyright 10.07.2012 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class ahcreport
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property npoints() As Integer
        Get
            Return csobj.npoints
        End Get
        Set(ByVal Value As Integer)
            csobj.npoints = Value
        End Set
        End Property
        Public Property p() As Integer()
        Get
            Return csobj.p
        End Get
        Set(ByVal Value As Integer())
            csobj.p = Value
        End Set
        End Property
        Public Property z() As Integer(,)
        Get
            Return csobj.z
        End Get
        Set(ByVal Value As Integer(,))
            csobj.z = Value
        End Set
        End Property
        Public Property pz() As Integer(,)
        Get
            Return csobj.pz
        End Get
        Set(ByVal Value As Integer(,))
            csobj.pz = Value
        End Set
        End Property
        Public Property pm() As Integer(,)
        Get
            Return csobj.pm
        End Get
        Set(ByVal Value As Integer(,))
            csobj.pm = Value
        End Set
        End Property
        Public Property mergedist() As Double()
        Get
            Return csobj.mergedist
        End Get
        Set(ByVal Value As Double())
            csobj.mergedist = Value
        End Set
        End Property
        Public csobj As alglib.ahcreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This  structure   is  used  to  store  results of the  k-means  clustering
    'algorithm.
    '
    'Following information is always returned:
    '* NPoints contains number of points in the original dataset
    '* TerminationType contains completion code, negative on failure, positive
    '  on success
    '* K contains number of clusters
    '
    'For positive TerminationType we return:
    '* NFeatures contains number of variables in the original dataset
    '* C, which contains centers found by algorithm
    '* CIdx, which maps points of the original dataset to clusters
    '
    'FORMAL DESCRIPTION OF FIELDS:
    '    NPoints         number of points, >=0
    '    NFeatures       number of variables, >=1
    '    TerminationType completion code:
    '                    * -5 if  distance  type  is  anything  different  from
    '                         Euclidean metric
    '                    * -3 for degenerate dataset: a) less  than  K  distinct
    '                         points, b) K=0 for non-empty dataset.
    '                    * +1 for successful completion
    '    K               number of clusters
    '    C               array[K,NFeatures], rows of the array store centers
    '    CIdx            array[NPoints], which contains cluster indexes
    '    IterationsCount actual number of iterations performed by clusterizer.
    '                    If algorithm performed more than one random restart,
    '                    total number of iterations is returned.
    '    Energy          merit function, "energy", sum  of  squared  deviations
    '                    from cluster centers
    '
    '  -- ALGLIB --
    '     Copyright 27.11.2012 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class kmeansreport
        Public Property npoints() As Integer
        Get
            Return csobj.npoints
        End Get
        Set(ByVal Value As Integer)
            csobj.npoints = Value
        End Set
        End Property
        Public Property nfeatures() As Integer
        Get
            Return csobj.nfeatures
        End Get
        Set(ByVal Value As Integer)
            csobj.nfeatures = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public Property iterationscount() As Integer
        Get
            Return csobj.iterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.iterationscount = Value
        End Set
        End Property
        Public Property energy() As Double
        Get
            Return csobj.energy
        End Get
        Set(ByVal Value As Double)
            csobj.energy = Value
        End Set
        End Property
        Public Property k() As Integer
        Get
            Return csobj.k
        End Get
        Set(ByVal Value As Integer)
            csobj.k = Value
        End Set
        End Property
        Public Property c() As Double(,)
        Get
            Return csobj.c
        End Get
        Set(ByVal Value As Double(,))
            csobj.c = Value
        End Set
        End Property
        Public Property cidx() As Integer()
        Get
            Return csobj.cidx
        End Get
        Set(ByVal Value As Integer())
            csobj.cidx = Value
        End Set
        End Property
        Public csobj As alglib.kmeansreport
    End Class


    Public Sub clusterizercreate(ByRef s As clusterizerstate)
        Try
            s = New clusterizerstate()
            alglib.clusterizercreate(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetpoints(ByRef s As clusterizerstate, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nfeatures As Integer, ByVal disttype As Integer)
        Try
            alglib.clusterizersetpoints(s.csobj, xy, npoints, nfeatures, disttype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetpoints(ByRef s As clusterizerstate, ByVal xy(,) As Double, ByVal disttype As Integer)
        Try
            alglib.clusterizersetpoints(s.csobj, xy, disttype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetdistances(ByRef s As clusterizerstate, ByVal d(,) As Double, ByVal npoints As Integer, ByVal isupper As Boolean)
        Try
            alglib.clusterizersetdistances(s.csobj, d, npoints, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetdistances(ByRef s As clusterizerstate, ByVal d(,) As Double, ByVal isupper As Boolean)
        Try
            alglib.clusterizersetdistances(s.csobj, d, isupper)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetahcalgo(ByRef s As clusterizerstate, ByVal algo As Integer)
        Try
            alglib.clusterizersetahcalgo(s.csobj, algo)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetkmeanslimits(ByRef s As clusterizerstate, ByVal restarts As Integer, ByVal maxits As Integer)
        Try
            alglib.clusterizersetkmeanslimits(s.csobj, restarts, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetkmeansinit(ByRef s As clusterizerstate, ByVal initalgo As Integer)
        Try
            alglib.clusterizersetkmeansinit(s.csobj, initalgo)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizersetseed(ByRef s As clusterizerstate, ByVal seed As Integer)
        Try
            alglib.clusterizersetseed(s.csobj, seed)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizerrunahc(ByRef s As clusterizerstate, ByRef rep As ahcreport)
        Try
            rep = New ahcreport()
            alglib.clusterizerrunahc(s.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizerrunkmeans(ByRef s As clusterizerstate, ByVal k As Integer, ByRef rep As kmeansreport)
        Try
            rep = New kmeansreport()
            alglib.clusterizerrunkmeans(s.csobj, k, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizergetdistances(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nfeatures As Integer, ByVal disttype As Integer, ByRef d(,) As Double)
        Try
            alglib.clusterizergetdistances(xy, npoints, nfeatures, disttype, d)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizergetkclusters(ByVal rep As ahcreport, ByVal k As Integer, ByRef cidx() As Integer, ByRef cz() As Integer)
        Try
            alglib.clusterizergetkclusters(rep.csobj, k, cidx, cz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizerseparatedbydist(ByVal rep As ahcreport, ByVal r As Double, ByRef k As Integer, ByRef cidx() As Integer, ByRef cz() As Integer)
        Try
            alglib.clusterizerseparatedbydist(rep.csobj, r, k, cidx, cz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub clusterizerseparatedbycorr(ByVal rep As ahcreport, ByVal r As Double, ByRef k As Integer, ByRef cidx() As Integer, ByRef cz() As Integer)
        Try
            alglib.clusterizerseparatedbycorr(rep.csobj, r, k, cidx, cz)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class decisionforestbuilder
        Public csobj As alglib.decisionforestbuilder
    End Class
    Public Class decisionforestbuffer
        Public csobj As alglib.decisionforestbuffer
    End Class
    Public Class decisionforest
        Public csobj As alglib.decisionforest
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Decision forest training report.
    '
    '=== training/oob errors ==================================================
    '
    'Following fields store training set errors:
    '* relclserror           -   fraction of misclassified cases, [0,1]
    '* avgce                 -   average cross-entropy in bits per symbol
    '* rmserror              -   root-mean-square error
    '* avgerror              -   average error
    '* avgrelerror           -   average relative error
    '
    'Out-of-bag estimates are stored in fields with same names, but "oob" prefix.
    '
    'For classification problems:
    '* RMS, AVG and AVGREL errors are calculated for posterior probabilities
    '
    'For regression problems:
    '* RELCLS and AVGCE errors are zero
    '
    '=== variable importance ==================================================
    '
    'Following fields are used to store variable importance information:
    '
    '* topvars               -   variables ordered from the most  important  to
    '                            less  important  ones  (according  to  current
    '                            choice of importance raiting).
    '                            For example, topvars[0] contains index of  the
    '                            most important variable, and topvars[0:2]  are
    '                            indexes of 3 most important ones and so on.
    '
    '* varimportances        -   array[nvars], ratings (the  larger,  the  more
    '                            important the variable  is,  always  in  [0,1]
    '                            range).
    '                            By default, filled  by  zeros  (no  importance
    '                            ratings are  provided  unless  you  explicitly
    '                            request them).
    '                            Zero rating means that variable is not important,
    '                            however you will rarely encounter such a thing,
    '                            in many cases  unimportant  variables  produce
    '                            nearly-zero (but nonzero) ratings.
    '
    'Variable importance report must be EXPLICITLY requested by calling:
    '* dfbuildersetimportancegini() function, if you need out-of-bag Gini-based
    '  importance rating also known as MDI  (fast to  calculate,  resistant  to
    '  overfitting  issues,   but   has   some   bias  towards  continuous  and
    '  high-cardinality categorical variables)
    '* dfbuildersetimportancetrngini() function, if you need training set Gini-
    '  -based importance rating (what other packages typically report).
    '* dfbuildersetimportancepermutation() function, if you  need  permutation-
    '  based importance rating also known as MDA (slower to calculate, but less
    '  biased)
    '* dfbuildersetimportancenone() function,  if  you  do  not  need  importance
    '  ratings - ratings will be zero, topvars[] will be [0,1,2,...]
    '
    'Different importance ratings (Gini or permutation) produce  non-comparable
    'values. Although in all cases rating values lie in [0,1] range, there  are
    'exist differences:
    '* informally speaking, Gini importance rating tends to divide "unit amount
    '  of importance"  between  several  important  variables, i.e. it produces
    '  estimates which roughly sum to 1.0 (or less than 1.0, if your  task  can
    '  not be solved exactly). If all variables  are  equally  important,  they
    '  will have same rating,  roughly  1/NVars,  even  if  every  variable  is
    '  critically important.
    '* from the other side, permutation importance tells us what percentage  of
    '  the model predictive power will be ruined  by  permuting  this  specific
    '  variable. It does not produce estimates which  sum  to  one.  Critically
    '  important variable will have rating close  to  1.0,  and  you  may  have
    '  multiple variables with such a rating.
    '
    'More information on variable importance ratings can be found  in  comments
    'on the dfbuildersetimportancegini() and dfbuildersetimportancepermutation()
    'functions.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class dfreport
        Public Property relclserror() As Double
        Get
            Return csobj.relclserror
        End Get
        Set(ByVal Value As Double)
            csobj.relclserror = Value
        End Set
        End Property
        Public Property avgce() As Double
        Get
            Return csobj.avgce
        End Get
        Set(ByVal Value As Double)
            csobj.avgce = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property oobrelclserror() As Double
        Get
            Return csobj.oobrelclserror
        End Get
        Set(ByVal Value As Double)
            csobj.oobrelclserror = Value
        End Set
        End Property
        Public Property oobavgce() As Double
        Get
            Return csobj.oobavgce
        End Get
        Set(ByVal Value As Double)
            csobj.oobavgce = Value
        End Set
        End Property
        Public Property oobrmserror() As Double
        Get
            Return csobj.oobrmserror
        End Get
        Set(ByVal Value As Double)
            csobj.oobrmserror = Value
        End Set
        End Property
        Public Property oobavgerror() As Double
        Get
            Return csobj.oobavgerror
        End Get
        Set(ByVal Value As Double)
            csobj.oobavgerror = Value
        End Set
        End Property
        Public Property oobavgrelerror() As Double
        Get
            Return csobj.oobavgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.oobavgrelerror = Value
        End Set
        End Property
        Public Property topvars() As Integer()
        Get
            Return csobj.topvars
        End Get
        Set(ByVal Value As Integer())
            csobj.topvars = Value
        End Set
        End Property
        Public Property varimportances() As Double()
        Get
            Return csobj.varimportances
        End Get
        Set(ByVal Value As Double())
            csobj.varimportances = Value
        End Set
        End Property
        Public csobj As alglib.dfreport
    End Class
    Public Sub dfserialize(ByVal obj As decisionforest, ByRef s_out As String)
        Try
            alglib.dfserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub dfunserialize(ByVal s_in As String, ByRef obj As decisionforest)
        Try
            alglib.dfunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfcreatebuffer(ByVal model As decisionforest, ByRef buf As decisionforestbuffer)
        Try
            buf = New decisionforestbuffer()
            alglib.dfcreatebuffer(model.csobj, buf.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildercreate(ByRef s As decisionforestbuilder)
        Try
            s = New decisionforestbuilder()
            alglib.dfbuildercreate(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetdataset(ByRef s As decisionforestbuilder, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer)
        Try
            alglib.dfbuildersetdataset(s.csobj, xy, npoints, nvars, nclasses)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetrndvars(ByRef s As decisionforestbuilder, ByVal rndvars As Integer)
        Try
            alglib.dfbuildersetrndvars(s.csobj, rndvars)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetrndvarsratio(ByRef s As decisionforestbuilder, ByVal f As Double)
        Try
            alglib.dfbuildersetrndvarsratio(s.csobj, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetrndvarsauto(ByRef s As decisionforestbuilder)
        Try
            alglib.dfbuildersetrndvarsauto(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetsubsampleratio(ByRef s As decisionforestbuilder, ByVal f As Double)
        Try
            alglib.dfbuildersetsubsampleratio(s.csobj, f)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetseed(ByRef s As decisionforestbuilder, ByVal seedval As Integer)
        Try
            alglib.dfbuildersetseed(s.csobj, seedval)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetrdfalgo(ByRef s As decisionforestbuilder, ByVal algotype As Integer)
        Try
            alglib.dfbuildersetrdfalgo(s.csobj, algotype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetrdfsplitstrength(ByRef s As decisionforestbuilder, ByVal splitstrength As Integer)
        Try
            alglib.dfbuildersetrdfsplitstrength(s.csobj, splitstrength)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetimportancetrngini(ByRef s As decisionforestbuilder)
        Try
            alglib.dfbuildersetimportancetrngini(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetimportanceoobgini(ByRef s As decisionforestbuilder)
        Try
            alglib.dfbuildersetimportanceoobgini(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetimportancepermutation(ByRef s As decisionforestbuilder)
        Try
            alglib.dfbuildersetimportancepermutation(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildersetimportancenone(ByRef s As decisionforestbuilder)
        Try
            alglib.dfbuildersetimportancenone(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function dfbuildergetprogress(ByVal s As decisionforestbuilder) As Double
        Try
            dfbuildergetprogress = alglib.dfbuildergetprogress(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfbuilderpeekprogress(ByVal s As decisionforestbuilder) As Double
        Try
            dfbuilderpeekprogress = alglib.dfbuilderpeekprogress(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub dfbuilderbuildrandomforest(ByRef s As decisionforestbuilder, ByVal ntrees As Integer, ByRef df As decisionforest, ByRef rep As dfreport)
        Try
            df = New decisionforest()
            rep = New dfreport()
            alglib.dfbuilderbuildrandomforest(s.csobj, ntrees, df.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function dfbinarycompression(ByRef df As decisionforest) As Double
        Try
            dfbinarycompression = alglib.dfbinarycompression(df.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub dfprocess(ByVal df As decisionforest, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.dfprocess(df.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfprocessi(ByVal df As decisionforest, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.dfprocessi(df.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function dfprocess0(ByRef model As decisionforest, ByVal x() As Double) As Double
        Try
            dfprocess0 = alglib.dfprocess0(model.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfclassify(ByRef model As decisionforest, ByVal x() As Double) As Integer
        Try
            dfclassify = alglib.dfclassify(model.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub dftsprocess(ByVal df As decisionforest, ByRef buf As decisionforestbuffer, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.dftsprocess(df.csobj, buf.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function dfrelclserror(ByVal df As decisionforest, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            dfrelclserror = alglib.dfrelclserror(df.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfavgce(ByVal df As decisionforest, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            dfavgce = alglib.dfavgce(df.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfrmserror(ByVal df As decisionforest, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            dfrmserror = alglib.dfrmserror(df.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfavgerror(ByVal df As decisionforest, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            dfavgerror = alglib.dfavgerror(df.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function dfavgrelerror(ByVal df As decisionforest, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            dfavgrelerror = alglib.dfavgrelerror(df.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub dfbuildrandomdecisionforest(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer, ByVal ntrees As Integer, ByVal r As Double, ByRef info As Integer, ByRef df As decisionforest, ByRef rep As dfreport)
        Try
            df = New decisionforest()
            rep = New dfreport()
            alglib.dfbuildrandomdecisionforest(xy, npoints, nvars, nclasses, ntrees, r, info, df.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub dfbuildrandomdecisionforestx1(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer, ByVal ntrees As Integer, ByVal nrndvars As Integer, ByVal r As Double, ByRef info As Integer, ByRef df As decisionforest, ByRef rep As dfreport)
        Try
            df = New decisionforest()
            rep = New dfreport()
            alglib.dfbuildrandomdecisionforestx1(xy, npoints, nvars, nclasses, ntrees, nrndvars, r, info, df.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class linearmodel
        Public csobj As alglib.linearmodel
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'LRReport structure contains additional information about linear model:
    '* C             -   covariation matrix,  array[0..NVars,0..NVars].
    '                    C[i,j] = Cov(A[i],A[j])
    '* RMSError      -   root mean square error on a training set
    '* AvgError      -   average error on a training set
    '* AvgRelError   -   average relative error on a training set (excluding
    '                    observations with zero function value).
    '* CVRMSError    -   leave-one-out cross-validation estimate of
    '                    generalization error. Calculated using fast algorithm
    '                    with O(NVars*NPoints) complexity.
    '* CVAvgError    -   cross-validation estimate of average error
    '* CVAvgRelError -   cross-validation estimate of average relative error
    '
    'All other fields of the structure are intended for internal use and should
    'not be used outside ALGLIB.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class lrreport
        Public Property c() As Double(,)
        Get
            Return csobj.c
        End Get
        Set(ByVal Value As Double(,))
            csobj.c = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property cvrmserror() As Double
        Get
            Return csobj.cvrmserror
        End Get
        Set(ByVal Value As Double)
            csobj.cvrmserror = Value
        End Set
        End Property
        Public Property cvavgerror() As Double
        Get
            Return csobj.cvavgerror
        End Get
        Set(ByVal Value As Double)
            csobj.cvavgerror = Value
        End Set
        End Property
        Public Property cvavgrelerror() As Double
        Get
            Return csobj.cvavgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.cvavgrelerror = Value
        End Set
        End Property
        Public Property ncvdefects() As Integer
        Get
            Return csobj.ncvdefects
        End Get
        Set(ByVal Value As Integer)
            csobj.ncvdefects = Value
        End Set
        End Property
        Public Property cvdefects() As Integer()
        Get
            Return csobj.cvdefects
        End Get
        Set(ByVal Value As Integer())
            csobj.cvdefects = Value
        End Set
        End Property
        Public csobj As alglib.lrreport
    End Class


    Public Sub lrbuild(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuild(xy, npoints, nvars, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuild(ByVal xy(,) As Double, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuild(xy, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuilds(ByVal xy(,) As Double, ByVal s() As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuilds(xy, s, npoints, nvars, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuilds(ByVal xy(,) As Double, ByVal s() As Double, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuilds(xy, s, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuildzs(ByVal xy(,) As Double, ByVal s() As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuildzs(xy, s, npoints, nvars, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuildzs(ByVal xy(,) As Double, ByVal s() As Double, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuildzs(xy, s, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuildz(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuildz(xy, npoints, nvars, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrbuildz(ByVal xy(,) As Double, ByRef lm As linearmodel, ByRef rep As lrreport)
        Try
            lm = New linearmodel()
            rep = New lrreport()
            alglib.lrbuildz(xy, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrunpack(ByVal lm As linearmodel, ByRef v() As Double, ByRef nvars As Integer)
        Try
            alglib.lrunpack(lm.csobj, v, nvars)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrpack(ByVal v() As Double, ByVal nvars As Integer, ByRef lm As linearmodel)
        Try
            lm = New linearmodel()
            alglib.lrpack(v, nvars, lm.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub lrpack(ByVal v() As Double, ByRef lm As linearmodel)
        Try
            lm = New linearmodel()
            alglib.lrpack(v, lm.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function lrprocess(ByVal lm As linearmodel, ByVal x() As Double) As Double
        Try
            lrprocess = alglib.lrprocess(lm.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lrrmserror(ByVal lm As linearmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            lrrmserror = alglib.lrrmserror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lravgerror(ByVal lm As linearmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            lravgerror = alglib.lravgerror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function lravgrelerror(ByVal lm As linearmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            lravgrelerror = alglib.lravgrelerror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function




    Public Sub filtersma(ByRef x() As Double, ByVal n As Integer, ByVal k As Integer)
        Try
            alglib.filtersma(x, n, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub filtersma(ByRef x() As Double, ByVal k As Integer)
        Try
            alglib.filtersma(x, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub filterema(ByRef x() As Double, ByVal n As Integer, ByVal alpha As Double)
        Try
            alglib.filterema(x, n, alpha)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub filterema(ByRef x() As Double, ByVal alpha As Double)
        Try
            alglib.filterema(x, alpha)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub filterlrma(ByRef x() As Double, ByVal n As Integer, ByVal k As Integer)
        Try
            alglib.filterlrma(x, n, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub filterlrma(ByRef x() As Double, ByVal k As Integer)
        Try
            alglib.filterlrma(x, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class ssamodel
        Public csobj As alglib.ssamodel
    End Class


    Public Sub ssacreate(ByRef s As ssamodel)
        Try
            s = New ssamodel()
            alglib.ssacreate(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetwindow(ByRef s As ssamodel, ByVal windowwidth As Integer)
        Try
            alglib.ssasetwindow(s.csobj, windowwidth)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetseed(ByRef s As ssamodel, ByVal seed As Integer)
        Try
            alglib.ssasetseed(s.csobj, seed)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetpoweruplength(ByRef s As ssamodel, ByVal pwlen As Integer)
        Try
            alglib.ssasetpoweruplength(s.csobj, pwlen)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetmemorylimit(ByRef s As ssamodel, ByVal memlimit As Integer)
        Try
            alglib.ssasetmemorylimit(s.csobj, memlimit)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaaddsequence(ByRef s As ssamodel, ByVal x() As Double, ByVal n As Integer)
        Try
            alglib.ssaaddsequence(s.csobj, x, n)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaaddsequence(ByRef s As ssamodel, ByVal x() As Double)
        Try
            alglib.ssaaddsequence(s.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaappendpointandupdate(ByRef s As ssamodel, ByVal x As Double, ByVal updateits As Double)
        Try
            alglib.ssaappendpointandupdate(s.csobj, x, updateits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaappendsequenceandupdate(ByRef s As ssamodel, ByVal x() As Double, ByVal nticks As Integer, ByVal updateits As Double)
        Try
            alglib.ssaappendsequenceandupdate(s.csobj, x, nticks, updateits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaappendsequenceandupdate(ByRef s As ssamodel, ByVal x() As Double, ByVal updateits As Double)
        Try
            alglib.ssaappendsequenceandupdate(s.csobj, x, updateits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetalgoprecomputed(ByRef s As ssamodel, ByVal a(,) As Double, ByVal windowwidth As Integer, ByVal nbasis As Integer)
        Try
            alglib.ssasetalgoprecomputed(s.csobj, a, windowwidth, nbasis)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetalgoprecomputed(ByRef s As ssamodel, ByVal a(,) As Double)
        Try
            alglib.ssasetalgoprecomputed(s.csobj, a)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetalgotopkdirect(ByRef s As ssamodel, ByVal topk As Integer)
        Try
            alglib.ssasetalgotopkdirect(s.csobj, topk)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssasetalgotopkrealtime(ByRef s As ssamodel, ByVal topk As Integer)
        Try
            alglib.ssasetalgotopkrealtime(s.csobj, topk)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssacleardata(ByRef s As ssamodel)
        Try
            alglib.ssacleardata(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssagetbasis(ByRef s As ssamodel, ByRef a(,) As Double, ByRef sv() As Double, ByRef windowwidth As Integer, ByRef nbasis As Integer)
        Try
            alglib.ssagetbasis(s.csobj, a, sv, windowwidth, nbasis)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssagetlrr(ByRef s As ssamodel, ByRef a() As Double, ByRef windowwidth As Integer)
        Try
            alglib.ssagetlrr(s.csobj, a, windowwidth)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaanalyzelastwindow(ByRef s As ssamodel, ByRef trend() As Double, ByRef noise() As Double, ByRef nticks As Integer)
        Try
            alglib.ssaanalyzelastwindow(s.csobj, trend, noise, nticks)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaanalyzelast(ByRef s As ssamodel, ByVal nticks As Integer, ByRef trend() As Double, ByRef noise() As Double)
        Try
            alglib.ssaanalyzelast(s.csobj, nticks, trend, noise)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaanalyzesequence(ByRef s As ssamodel, ByVal data() As Double, ByVal nticks As Integer, ByRef trend() As Double, ByRef noise() As Double)
        Try
            alglib.ssaanalyzesequence(s.csobj, data, nticks, trend, noise)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaanalyzesequence(ByRef s As ssamodel, ByVal data() As Double, ByRef trend() As Double, ByRef noise() As Double)
        Try
            alglib.ssaanalyzesequence(s.csobj, data, trend, noise)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastlast(ByRef s As ssamodel, ByVal nticks As Integer, ByRef trend() As Double)
        Try
            alglib.ssaforecastlast(s.csobj, nticks, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastsequence(ByRef s As ssamodel, ByVal data() As Double, ByVal datalen As Integer, ByVal forecastlen As Integer, ByVal applysmoothing As Boolean, ByRef trend() As Double)
        Try
            alglib.ssaforecastsequence(s.csobj, data, datalen, forecastlen, applysmoothing, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastsequence(ByRef s As ssamodel, ByVal data() As Double, ByVal forecastlen As Integer, ByRef trend() As Double)
        Try
            alglib.ssaforecastsequence(s.csobj, data, forecastlen, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastavglast(ByRef s As ssamodel, ByVal m As Integer, ByVal nticks As Integer, ByRef trend() As Double)
        Try
            alglib.ssaforecastavglast(s.csobj, m, nticks, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastavgsequence(ByRef s As ssamodel, ByVal data() As Double, ByVal datalen As Integer, ByVal m As Integer, ByVal forecastlen As Integer, ByVal applysmoothing As Boolean, ByRef trend() As Double)
        Try
            alglib.ssaforecastavgsequence(s.csobj, data, datalen, m, forecastlen, applysmoothing, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub ssaforecastavgsequence(ByRef s As ssamodel, ByVal data() As Double, ByVal m As Integer, ByVal forecastlen As Integer, ByRef trend() As Double)
        Try
            alglib.ssaforecastavgsequence(s.csobj, data, m, forecastlen, trend)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub fisherlda(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer, ByRef w() As Double)
        Try
            alglib.fisherlda(xy, npoints, nvars, nclasses, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fisherlda(ByVal xy(,) As Double, ByVal nclasses As Integer, ByRef w() As Double)
        Try
            alglib.fisherlda(xy, nclasses, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fisherldan(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer, ByRef w(,) As Double)
        Try
            alglib.fisherldan(xy, npoints, nvars, nclasses, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub fisherldan(ByVal xy(,) As Double, ByVal nclasses As Integer, ByRef w(,) As Double)
        Try
            alglib.fisherldan(xy, nclasses, w)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class mcpdstate
        Public csobj As alglib.mcpdstate
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This structure is a MCPD training report:
    '    InnerIterationsCount    -   number of inner iterations of the
    '                                underlying optimization algorithm
    '    OuterIterationsCount    -   number of outer iterations of the
    '                                underlying optimization algorithm
    '    NFEV                    -   number of merit function evaluations
    '    TerminationType         -   termination type
    '                                (same as for MinBLEIC optimizer, positive
    '                                values denote success, negative ones -
    '                                failure)
    '
    '  -- ALGLIB --
    '     Copyright 23.05.2010 by Bochkanov Sergey
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mcpdreport
        Public Property inneriterationscount() As Integer
        Get
            Return csobj.inneriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.inneriterationscount = Value
        End Set
        End Property
        Public Property outeriterationscount() As Integer
        Get
            Return csobj.outeriterationscount
        End Get
        Set(ByVal Value As Integer)
            csobj.outeriterationscount = Value
        End Set
        End Property
        Public Property nfev() As Integer
        Get
            Return csobj.nfev
        End Get
        Set(ByVal Value As Integer)
            csobj.nfev = Value
        End Set
        End Property
        Public Property terminationtype() As Integer
        Get
            Return csobj.terminationtype
        End Get
        Set(ByVal Value As Integer)
            csobj.terminationtype = Value
        End Set
        End Property
        Public csobj As alglib.mcpdreport
    End Class


    Public Sub mcpdcreate(ByVal n As Integer, ByRef s As mcpdstate)
        Try
            s = New mcpdstate()
            alglib.mcpdcreate(n, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdcreateentry(ByVal n As Integer, ByVal entrystate As Integer, ByRef s As mcpdstate)
        Try
            s = New mcpdstate()
            alglib.mcpdcreateentry(n, entrystate, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdcreateexit(ByVal n As Integer, ByVal exitstate As Integer, ByRef s As mcpdstate)
        Try
            s = New mcpdstate()
            alglib.mcpdcreateexit(n, exitstate, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdcreateentryexit(ByVal n As Integer, ByVal entrystate As Integer, ByVal exitstate As Integer, ByRef s As mcpdstate)
        Try
            s = New mcpdstate()
            alglib.mcpdcreateentryexit(n, entrystate, exitstate, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdaddtrack(ByRef s As mcpdstate, ByVal xy(,) As Double, ByVal k As Integer)
        Try
            alglib.mcpdaddtrack(s.csobj, xy, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdaddtrack(ByRef s As mcpdstate, ByVal xy(,) As Double)
        Try
            alglib.mcpdaddtrack(s.csobj, xy)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetec(ByRef s As mcpdstate, ByVal ec(,) As Double)
        Try
            alglib.mcpdsetec(s.csobj, ec)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdaddec(ByRef s As mcpdstate, ByVal i As Integer, ByVal j As Integer, ByVal c As Double)
        Try
            alglib.mcpdaddec(s.csobj, i, j, c)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetbc(ByRef s As mcpdstate, ByVal bndl(,) As Double, ByVal bndu(,) As Double)
        Try
            alglib.mcpdsetbc(s.csobj, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdaddbc(ByRef s As mcpdstate, ByVal i As Integer, ByVal j As Integer, ByVal bndl As Double, ByVal bndu As Double)
        Try
            alglib.mcpdaddbc(s.csobj, i, j, bndl, bndu)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetlc(ByRef s As mcpdstate, ByVal c(,) As Double, ByVal ct() As Integer, ByVal k As Integer)
        Try
            alglib.mcpdsetlc(s.csobj, c, ct, k)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetlc(ByRef s As mcpdstate, ByVal c(,) As Double, ByVal ct() As Integer)
        Try
            alglib.mcpdsetlc(s.csobj, c, ct)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsettikhonovregularizer(ByRef s As mcpdstate, ByVal v As Double)
        Try
            alglib.mcpdsettikhonovregularizer(s.csobj, v)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetprior(ByRef s As mcpdstate, ByVal pp(,) As Double)
        Try
            alglib.mcpdsetprior(s.csobj, pp)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsetpredictionweights(ByRef s As mcpdstate, ByVal pw() As Double)
        Try
            alglib.mcpdsetpredictionweights(s.csobj, pw)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdsolve(ByRef s As mcpdstate)
        Try
            alglib.mcpdsolve(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mcpdresults(ByVal s As mcpdstate, ByRef p(,) As Double, ByRef rep As mcpdreport)
        Try
            rep = New mcpdreport()
            alglib.mcpdresults(s.csobj, p, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Class logitmodel
        Public csobj As alglib.logitmodel
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'MNLReport structure contains information about training process:
    '* NGrad     -   number of gradient calculations
    '* NHess     -   number of Hessian calculations
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mnlreport
        Public Property ngrad() As Integer
        Get
            Return csobj.ngrad
        End Get
        Set(ByVal Value As Integer)
            csobj.ngrad = Value
        End Set
        End Property
        Public Property nhess() As Integer
        Get
            Return csobj.nhess
        End Get
        Set(ByVal Value As Integer)
            csobj.nhess = Value
        End Set
        End Property
        Public csobj As alglib.mnlreport
    End Class


    Public Sub mnltrainh(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer, ByRef info As Integer, ByRef lm As logitmodel, ByRef rep As mnlreport)
        Try
            lm = New logitmodel()
            rep = New mnlreport()
            alglib.mnltrainh(xy, npoints, nvars, nclasses, info, lm.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mnlprocess(ByRef lm As logitmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mnlprocess(lm.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mnlprocessi(ByRef lm As logitmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.mnlprocessi(lm.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mnlunpack(ByVal lm As logitmodel, ByRef a(,) As Double, ByRef nvars As Integer, ByRef nclasses As Integer)
        Try
            alglib.mnlunpack(lm.csobj, a, nvars, nclasses)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mnlpack(ByVal a(,) As Double, ByVal nvars As Integer, ByVal nclasses As Integer, ByRef lm As logitmodel)
        Try
            lm = New logitmodel()
            alglib.mnlpack(a, nvars, nclasses, lm.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mnlavgce(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mnlavgce = alglib.mnlavgce(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mnlrelclserror(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mnlrelclserror = alglib.mnlrelclserror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mnlrmserror(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mnlrmserror = alglib.mnlrmserror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mnlavgerror(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            mnlavgerror = alglib.mnlavgerror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mnlavgrelerror(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal ssize As Integer) As Double
        Try
            mnlavgrelerror = alglib.mnlavgrelerror(lm.csobj, xy, ssize)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function mnlclserror(ByRef lm As logitmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Integer
        Try
            mnlclserror = alglib.mnlclserror(lm.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function

    Public Class knnbuffer
        Public csobj As alglib.knnbuffer
    End Class
    Public Class knnbuilder
        Public csobj As alglib.knnbuilder
    End Class
    Public Class knnmodel
        Public csobj As alglib.knnmodel
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'KNN training report.
    '
    'Following fields store training set errors:
    '* relclserror       -   fraction of misclassified cases, [0,1]
    '* avgce             -   average cross-entropy in bits per symbol
    '* rmserror          -   root-mean-square error
    '* avgerror          -   average error
    '* avgrelerror       -   average relative error
    '
    'For classification problems:
    '* RMS, AVG and AVGREL errors are calculated for posterior probabilities
    '
    'For regression problems:
    '* RELCLS and AVGCE errors are zero
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class knnreport
        Public Property relclserror() As Double
        Get
            Return csobj.relclserror
        End Get
        Set(ByVal Value As Double)
            csobj.relclserror = Value
        End Set
        End Property
        Public Property avgce() As Double
        Get
            Return csobj.avgce
        End Get
        Set(ByVal Value As Double)
            csobj.avgce = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public csobj As alglib.knnreport
    End Class
    Public Sub knnserialize(ByVal obj As knnmodel, ByRef s_out As String)
        Try
            alglib.knnserialize(obj.csobj, s_out)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    Public Sub knnunserialize(ByVal s_in As String, ByRef obj As knnmodel)
        Try
            alglib.knnunserialize(s_in, obj.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knncreatebuffer(ByVal model As knnmodel, ByRef buf As knnbuffer)
        Try
            buf = New knnbuffer()
            alglib.knncreatebuffer(model.csobj, buf.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnbuildercreate(ByRef s As knnbuilder)
        Try
            s = New knnbuilder()
            alglib.knnbuildercreate(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnbuildersetdatasetreg(ByRef s As knnbuilder, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nout As Integer)
        Try
            alglib.knnbuildersetdatasetreg(s.csobj, xy, npoints, nvars, nout)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnbuildersetdatasetcls(ByRef s As knnbuilder, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal nclasses As Integer)
        Try
            alglib.knnbuildersetdatasetcls(s.csobj, xy, npoints, nvars, nclasses)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnbuildersetnorm(ByRef s As knnbuilder, ByVal nrmtype As Integer)
        Try
            alglib.knnbuildersetnorm(s.csobj, nrmtype)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnbuilderbuildknnmodel(ByRef s As knnbuilder, ByVal k As Integer, ByVal eps As Double, ByRef model As knnmodel, ByRef rep As knnreport)
        Try
            model = New knnmodel()
            rep = New knnreport()
            alglib.knnbuilderbuildknnmodel(s.csobj, k, eps, model.csobj, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnrewritekeps(ByRef model As knnmodel, ByVal k As Integer, ByVal eps As Double)
        Try
            alglib.knnrewritekeps(model.csobj, k, eps)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knnprocess(ByRef model As knnmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.knnprocess(model.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function knnprocess0(ByRef model As knnmodel, ByVal x() As Double) As Double
        Try
            knnprocess0 = alglib.knnprocess0(model.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function knnclassify(ByRef model As knnmodel, ByVal x() As Double) As Integer
        Try
            knnclassify = alglib.knnclassify(model.csobj, x)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub knnprocessi(ByRef model As knnmodel, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.knnprocessi(model.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub knntsprocess(ByVal model As knnmodel, ByRef buf As knnbuffer, ByVal x() As Double, ByRef y() As Double)
        Try
            alglib.knntsprocess(model.csobj, buf.csobj, x, y)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function knnrelclserror(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            knnrelclserror = alglib.knnrelclserror(model.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function knnavgce(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            knnavgce = alglib.knnavgce(model.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function knnrmserror(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            knnrmserror = alglib.knnrmserror(model.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function knnavgerror(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            knnavgerror = alglib.knnavgerror(model.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Function knnavgrelerror(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer) As Double
        Try
            knnavgrelerror = alglib.knnavgrelerror(model.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub knnallerrors(ByVal model As knnmodel, ByVal xy(,) As Double, ByVal npoints As Integer, ByRef rep As knnreport)
        Try
            rep = New knnreport()
            alglib.knnallerrors(model.csobj, xy, npoints, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Training report:
    '    * RelCLSError   -   fraction of misclassified cases.
    '    * AvgCE         -   acerage cross-entropy
    '    * RMSError      -   root-mean-square error
    '    * AvgError      -   average error
    '    * AvgRelError   -   average relative error
    '    * NGrad         -   number of gradient calculations
    '    * NHess         -   number of Hessian calculations
    '    * NCholesky     -   number of Cholesky decompositions
    '
    'NOTE 1: RelCLSError/AvgCE are zero on regression problems.
    '
    'NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
    '        errors in prediction of posterior probabilities
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mlpreport
        Public Property relclserror() As Double
        Get
            Return csobj.relclserror
        End Get
        Set(ByVal Value As Double)
            csobj.relclserror = Value
        End Set
        End Property
        Public Property avgce() As Double
        Get
            Return csobj.avgce
        End Get
        Set(ByVal Value As Double)
            csobj.avgce = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public Property ngrad() As Integer
        Get
            Return csobj.ngrad
        End Get
        Set(ByVal Value As Integer)
            csobj.ngrad = Value
        End Set
        End Property
        Public Property nhess() As Integer
        Get
            Return csobj.nhess
        End Get
        Set(ByVal Value As Integer)
            csobj.nhess = Value
        End Set
        End Property
        Public Property ncholesky() As Integer
        Get
            Return csobj.ncholesky
        End Get
        Set(ByVal Value As Integer)
            csobj.ncholesky = Value
        End Set
        End Property
        Public csobj As alglib.mlpreport
    End Class
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Cross-validation estimates of generalization error
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class mlpcvreport
        Public Property relclserror() As Double
        Get
            Return csobj.relclserror
        End Get
        Set(ByVal Value As Double)
            csobj.relclserror = Value
        End Set
        End Property
        Public Property avgce() As Double
        Get
            Return csobj.avgce
        End Get
        Set(ByVal Value As Double)
            csobj.avgce = Value
        End Set
        End Property
        Public Property rmserror() As Double
        Get
            Return csobj.rmserror
        End Get
        Set(ByVal Value As Double)
            csobj.rmserror = Value
        End Set
        End Property
        Public Property avgerror() As Double
        Get
            Return csobj.avgerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgerror = Value
        End Set
        End Property
        Public Property avgrelerror() As Double
        Get
            Return csobj.avgrelerror
        End Get
        Set(ByVal Value As Double)
            csobj.avgrelerror = Value
        End Set
        End Property
        Public csobj As alglib.mlpcvreport
    End Class
    Public Class mlptrainer
        Public csobj As alglib.mlptrainer
    End Class


    Public Sub mlptrainlm(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByRef info As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlptrainlm(network.csobj, xy, npoints, decay, restarts, info, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlptrainlbfgs(ByRef network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByVal wstep As Double, ByVal maxits As Integer, ByRef info As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlptrainlbfgs(network.csobj, xy, npoints, decay, restarts, wstep, maxits, info, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlptraines(ByRef network As multilayerperceptron, ByVal trnxy(,) As Double, ByVal trnsize As Integer, ByVal valxy(,) As Double, ByVal valsize As Integer, ByVal decay As Double, ByVal restarts As Integer, ByRef info As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlptraines(network.csobj, trnxy, trnsize, valxy, valsize, decay, restarts, info, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpkfoldcvlbfgs(ByVal network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByVal wstep As Double, ByVal maxits As Integer, ByVal foldscount As Integer, ByRef info As Integer, ByRef rep As mlpreport, ByRef cvrep As mlpcvreport)
        Try
            rep = New mlpreport()
            cvrep = New mlpcvreport()
            alglib.mlpkfoldcvlbfgs(network.csobj, xy, npoints, decay, restarts, wstep, maxits, foldscount, info, rep.csobj, cvrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpkfoldcvlm(ByVal network As multilayerperceptron, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByVal foldscount As Integer, ByRef info As Integer, ByRef rep As mlpreport, ByRef cvrep As mlpcvreport)
        Try
            rep = New mlpreport()
            cvrep = New mlpcvreport()
            alglib.mlpkfoldcvlm(network.csobj, xy, npoints, decay, restarts, foldscount, info, rep.csobj, cvrep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpkfoldcv(ByRef s As mlptrainer, ByVal network As multilayerperceptron, ByVal nrestarts As Integer, ByVal foldscount As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlpkfoldcv(s.csobj, network.csobj, nrestarts, foldscount, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreatetrainer(ByVal nin As Integer, ByVal nout As Integer, ByRef s As mlptrainer)
        Try
            s = New mlptrainer()
            alglib.mlpcreatetrainer(nin, nout, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpcreatetrainercls(ByVal nin As Integer, ByVal nclasses As Integer, ByRef s As mlptrainer)
        Try
            s = New mlptrainer()
            alglib.mlpcreatetrainercls(nin, nclasses, s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetdataset(ByRef s As mlptrainer, ByVal xy(,) As Double, ByVal npoints As Integer)
        Try
            alglib.mlpsetdataset(s.csobj, xy, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetsparsedataset(ByRef s As mlptrainer, ByVal xy As sparsematrix, ByVal npoints As Integer)
        Try
            alglib.mlpsetsparsedataset(s.csobj, xy.csobj, npoints)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetdecay(ByRef s As mlptrainer, ByVal decay As Double)
        Try
            alglib.mlpsetdecay(s.csobj, decay)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetcond(ByRef s As mlptrainer, ByVal wstep As Double, ByVal maxits As Integer)
        Try
            alglib.mlpsetcond(s.csobj, wstep, maxits)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpsetalgobatch(ByRef s As mlptrainer)
        Try
            alglib.mlpsetalgobatch(s.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlptrainnetwork(ByRef s As mlptrainer, ByRef network As multilayerperceptron, ByVal nrestarts As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlptrainnetwork(s.csobj, network.csobj, nrestarts, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpstarttraining(ByRef s As mlptrainer, ByRef network As multilayerperceptron, ByVal randomstart As Boolean)
        Try
            alglib.mlpstarttraining(s.csobj, network.csobj, randomstart)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Function mlpcontinuetraining(ByRef s As mlptrainer, ByRef network As multilayerperceptron) As Boolean
        Try
            mlpcontinuetraining = alglib.mlpcontinuetraining(s.csobj, network.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Function


    Public Sub mlpebagginglm(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByRef info As Integer, ByRef rep As mlpreport, ByRef ooberrors As mlpcvreport)
        Try
            rep = New mlpreport()
            ooberrors = New mlpcvreport()
            alglib.mlpebagginglm(ensemble.csobj, xy, npoints, decay, restarts, info, rep.csobj, ooberrors.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpebagginglbfgs(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByVal wstep As Double, ByVal maxits As Integer, ByRef info As Integer, ByRef rep As mlpreport, ByRef ooberrors As mlpcvreport)
        Try
            rep = New mlpreport()
            ooberrors = New mlpcvreport()
            alglib.mlpebagginglbfgs(ensemble.csobj, xy, npoints, decay, restarts, wstep, maxits, info, rep.csobj, ooberrors.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlpetraines(ByRef ensemble As mlpensemble, ByVal xy(,) As Double, ByVal npoints As Integer, ByVal decay As Double, ByVal restarts As Integer, ByRef info As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlpetraines(ensemble.csobj, xy, npoints, decay, restarts, info, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub


    Public Sub mlptrainensemblees(ByRef s As mlptrainer, ByRef ensemble As mlpensemble, ByVal nrestarts As Integer, ByRef rep As mlpreport)
        Try
            rep = New mlpreport()
            alglib.mlptrainensemblees(s.csobj, ensemble.csobj, nrestarts, rep.csobj)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




    Public Sub kmeansgenerate(ByVal xy(,) As Double, ByVal npoints As Integer, ByVal nvars As Integer, ByVal k As Integer, ByVal restarts As Integer, ByRef info As Integer, ByRef c(,) As Double, ByRef xyc() As Integer)
        Try
            alglib.kmeansgenerate(xy, npoints, nvars, k, restarts, info, c, xyc)
        Catch _E_Alglib As alglib.alglibexception
            Throw New AlglibException(_E_Alglib.Msg)
        End Try
    End Sub




End Module
