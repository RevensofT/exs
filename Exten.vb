
Namespace Info
    '''<summary>Get mass size of type in byte length.</summary>
    Public Structure mass(Of T)
        Public Shared ReadOnly size As int = "mt.0".compile(Of Func(Of Integer)).type(Of T).fin()()
    End Structure
    Public Structure token
        Private Shared ReadOnly info As Func(Of Object, int) = "la.0 li8".compile(Of Func(Of Object, int)).fin

        Public Shared Function from(Of T As Class)(Type_sample As T) As int
            Return info(Type_sample)
        End Function

        Friend Shared Function cant_def(Of T As Class)() As int
            Throw New Exception(GetType(T).FullName & " hasn't { Pubic Sub New()}, use { EXS.Info.token.from(Instance_of_type) } instead.")
            Return 0
        End Function
    End Structure
    '''<summary>Get token of type, if value is 0 use 'Info.token.from(Of T)(Sample)' instead.</summary>
    Public Structure token(Of T As Class)
        Private Shared ReadOnly def As Func(Of int) = "new.0 li8".compile(Of Func(Of int)).new(Of T).fin
        Public Shared ReadOnly value As int = If(GetType(T).IsArray, array(Of T).token,
                                              If(GetType(T).IsTypeDefinition, def(), token.cant_def(Of T)))
    End Structure
    Public Structure array(Of Array As Class)
        ''' <summary>Type of array's element.</summary>
        Public Shared ReadOnly type As System.Type = GetType(Array).GetElementType
        ''' <summary>Mass of array's element.</summary>
        Public Shared ReadOnly mass As int = "mt.0".compile(Of Func(Of int)).type(type).fin()()
        Public Shared ReadOnly token As int = ".0 nat.0 li8".compile(Of Func(Of int)).type(type).fin()()

        Public Shared Function cal_tuple_length(Of Tuple As Structure)() As int
            Return (Info.mass(Of Tuple).size - IntPtr.Size * 2) / mass
        End Function
    End Structure

End Namespace


'''<summary>Extend Standard library</summary>
Namespace EXS
    Public Structure pointer(Of T)
        Friend Shared pt_copy As Action(Of UIntPtr, UIntPtr, uint) = "la.1.0.2 cpyb".compile(Of Action(Of UIntPtr, UIntPtr, uint)).fin
        Friend Shared pt_isendp As Func(Of pointer(Of T), pointer(Of T), Boolean) = "la.0.1 >".compile(Of Func(Of pointer(Of T), pointer(Of T), Boolean)).type(Of T).fin
        Friend Shared pt_isend As Func(Of UIntPtr, UIntPtr, Boolean) = "la.0.1 mt.0 - >".compile(Of Func(Of UIntPtr, UIntPtr, Boolean)).type(Of T).fin
        Friend Shared pt_nexts As Func(Of UIntPtr, uint, pointer(Of T)) = "la.0.1 mt.0 * +".compile(Of Func(Of UIntPtr, uint, pointer(Of T))).type(Of T).fin
        Friend Shared pt_next As Func(Of UIntPtr, pointer(Of T)) = "la.0 mt.0 +".compile(Of Func(Of UIntPtr, pointer(Of T))).type(Of T).fin
        Friend Shared pt_get As Func(Of UIntPtr, T) = "la.0 lot.0".compile(Of Func(Of UIntPtr, T)).type(Of T).fin
        Friend Shared pt_set As Action(Of UIntPtr, T) = "la.0.1 sot.0".compile(Of Action(Of UIntPtr, T)).type(Of T).fin

        Friend address As UIntPtr
        Public Property value As T
            <Method(inline)>
            Get
                Return pt_get(address)
            End Get
            <Method(inline)>
            Set(Input As T)
                pt_set(address, Input)
            End Set
        End Property
        Default Public Property Item(Index As uint) As T
            <Method(inline)>
            Get
                Return move(Index).load
            End Get
            <Method(inline)>
            Set(Value As T)
                move(Index).store(Value)
            End Set
        End Property
        <Method(inline)>
        Public Function load() As T
            Return pt_get(address)
        End Function
        <Method(inline)>
        Public Function store(Input As T) As pointer(Of T)
            pt_set(address, Input)
            Return Me
        End Function

        ''''<summary>Update THIS pointer address, for non side effect use move() or next().</summary>
        '''<summary>Return new pointer from shifted current pointer by offset address.</summary>
        <Method(inline)>
        Public Function shift(Offset As Integer) As pointer(Of T)
            Return (address + Offset).as(Of pointer(Of T))
        End Function
        '''<summary>Return a new pointer with next item address.</summary>
        <Method(inline)>
        Public Function [next]() As pointer(Of T)
            Return pt_next(address)
        End Function
        '''<summary>Return a new pointer with target item address from index.</summary>
        <Method(inline)>
        Public Function move(Index As uint) As pointer(Of T)
            Return pt_nexts(address, Index)
        End Function

        '''<summary>Is (this.address > last_item.address) ?</summary>
        <Method(inline)>
        Public Function is_end(Last_item As pointer(Of T)) As Boolean
            Return pt_isendp(Me, Last_item)
        End Function
        <Method(inline)>
        Friend Function is_end(End_point As UIntPtr) As Boolean
            Return pt_isend(address, End_point)
        End Function
        <Method(inline)>
        Public Function copy_to(Target As pointer(Of T), Count As uint) As pointer(Of T)
            pt_copy(address, Target.address, Count * CULng(Info.mass(Of T).size))
            Return Me
        End Function
        <Method(inline)>
        Friend Function copy_to(Target As UIntPtr, Count As uint) As pointer(Of T)
            pt_copy(address, Target, Count * CULng(Info.mass(Of T).size))
            Return Me
        End Function
        '''<summary>Change pointer to new type.</summary>
        <Method(inline)>
        Public Function change(Of V)() As pointer(Of V)
            Return address.as(Of pointer(Of V))
        End Function
    End Structure

    Public Structure space
        Friend ReadOnly first, last As UIntPtr
        <Method(inline)>
        Friend Sub New(First As UIntPtr, Last As UIntPtr)
            Me.first = First
            Me.last = Last
        End Sub
        '''<summary>Create a pointer of space.</summary>
        <Method(inline)>
        Public Function pointer(Of T)() As pointer(Of T)
            Return first.as(Of pointer(Of T))
        End Function
        <Method(inline)>
        Public Function is_end(Of T)(Input As pointer(Of T)) As Boolean
            Return Input.is_end(last)
        End Function

        <Method(inline)>
        Public Function length() As uint
            Return CULng(last) - CULng(first)
        End Function
        <Method(inline)>
        Public Function length_as_array(Of T)() As uint
            Return (CULng(last) - CULng(first) - IntPtr.Size * 2) \ Info.mass(Of T).size
        End Function
        <Method(inline)>
        Public Function as_array(Of T)() As T()
            pointer(Of uint).store(Info.token(Of T()).value).next.store(length_as_array(Of T))
            Return DirectCast(first.as(Of T()), T())
        End Function
        '''<summary>Copy entire space to array except meta data(first 2 UIntPtr).</summary>
        <Method(inline)>
        Public Function to_array(Of T)() As T()
            Dim Tmp = New T(length_as_array(Of T)() - 1) {}
            pointer(Of uint).move(2).change(Of T).copy_to(Tmp(0).ref, Tmp.Length)
            Return Tmp
        End Function
    End Structure

    Friend Structure methods(Of T1)
        Friend Delegate Sub swapping(ByRef A As T1, ByRef B As T1)
        Friend Shared ReadOnly swap As swapping = "la.1.0 lot.0 la.0.1 lot.0 sot.0 sot.0".compile(Of swapping).type(Of T1).fin

        Friend Delegate Function recuring(Data As T1, [Until] As Func(Of T1, Boolean), Recur As Func(Of T1, T1)) As T1
        Friend Shared ReadOnly recur As recuring = "la.1.0 used f: la ; : la.2.0 used.1 la.1.2 re use-me".compile(Of recuring).fin

        '''<summary>Safe stack alloc.</summary>
        Friend Shared ReadOnly alloc As Func(Of uint, T1, Func(Of T1, space, T1), T1) =
            "la [n] [+] la + new so la.2.1 lo used".
            compile(Of Func(Of uint, T1, Func(Of T1, space, T1), T1)).
            def(Of space).
            [new](Of space).
            fin
    End Structure
    Friend Structure methods(Of T1, T2)
        Friend Shared ReadOnly [as] As Func(Of T1, T2) = "la.0".compile(Of Func(Of T1, T2)).fin

        Friend Delegate Function pointing(ByRef Input As T1) As pointer(Of T2)
        Friend Shared ReadOnly pointer As pointing = "la.0".compile(Of pointing).fin

        Friend Delegate Function recuring(Data As T1, Extra As T2, [Until] As Func(Of T1, T2, Boolean), Recur As Func(Of T1, T2, T1)) As T1
        Friend Shared ReadOnly recur As recuring = "la.2.0.1 used f: la ; : la.3.0.1 used.1 la.1.2.3 re use-me".compile(Of recuring).fin

        '''<summary>Safe stack alloc array.</summary>
        Friend Shared ReadOnly alloc As Func(Of uint, T2, Func(Of T2, T1(), T2), T2) =
            "la mt * mt.1. + + [n] [+] i8.0 si [+] mt.1 + la si so la.2.1 lo used".
            compile(Of Func(Of uint, T2, Func(Of T2, T1(), T2), T2)).
            i8(Info.token(Of T1()).value).
            def(Of T1()).
            type(Of T1, uint).
            fin
    End Structure

    Partial Public Module Exten
        '''<summary>Get current object return back.</summary>
        <Extension, Method(inline)>
        Public Function [me](Of T)(Input As T) As T
            Return Input
        End Function

#Disable Warning IDE0060 ' Remove unused parameter
        '''<summary>Fake request input for any syntax that request unnecessary reciver.</summary>
        <Extension, Method(inline)>
        Public Sub [void](Of T)(Input As T)
        End Sub
#Enable Warning IDE0060 ' Remove unused parameter

        <Extension, Method(inline)>
        Public Sub [with](Of T)(Input As T, Method As System.Action(Of T))
            Method(Input)
        End Sub

        <Extension, Method(inline)>
        Public Function [with](Of T, R)(Input As T, Method As System.Func(Of T, R)) As R
            Return Method(Input)
        End Function

        '''<summary>Direct cast type without any checking, be careful.</summary>
        <Extension, Method(inline)>
        Public Function [as](Of T, V)(Input As T) As V
            Return methods(Of T, V).as(Input) 'DirectCast(Pre.as(Of T, V).as(Input), V)
        End Function

        '''<summary>Swap value between element of array.</summary>
        <Extension, Method(inline)>
        Public Function swap(Of T)(Input As T(), A_index As Int32, B_index As Int32) As T()
            methods(Of T).swap(Input(A_index), Input(B_index))
            Return Input
        End Function

        '''<summary>Swap value between 2 inputs.</summary>
        <Extension, Method(inline)>
        Public Sub swap(Of T)(ByRef A As T, ByRef B As T)
            methods(Of T).swap(A, B)
        End Sub

        <Extension, Method(inline)>
        Public Function be(Of Value As Structure, Refer As Class)(ByRef Value_tuple As Value) As Refer
            With Value_tuple.ref(Of int).store(Info.token(Of Refer).value).shift(IntPtr.Size)
                If .load = 0 AndAlso GetType(Refer).IsArray Then
                    .store(Info.array(Of Refer).cal_tuple_length(Of Value))
                End If
            End With
            Return Value_tuple.as(Of Refer) 'Pre.ref(Of Value, Refer).val(Value_tuple)
        End Function

        <Extension, Method(inline)>
        Public Function be(Of Value As Structure, Refer As Class)(Input As Refer) As Value
            Return Input.as(Of pointer(Of Value)).value  'Pre.ref(Of Value, Refer).rev(Input)
        End Function

        '''<summary>Create a reference pointer of R. Ex: XY_Pointer.ref(Of Integer).store(99) // XY_Pointer.X == 99</summary>
        <Extension, Method(inline)>
        Public Function ref(Of T, R)(ByRef Input As T) As pointer(Of R)
            Return methods(Of T, R).pointer(Input)
        End Function
        '''<summary>Create a reference pointer. Ex: XY_Pointer.Y.ref.store(77) // XY_Pointer.Y == 77</summary>
        <Extension, Method(inline)>
        Public Function ref(Of T)(ByRef Input As T) As pointer(Of T)
            Return methods(Of T, T).pointer(Input)
        End Function

        '''<summary>Create a memory space on stack memory.</summary>
        <Extension, Method(inline)>
        Public Function stack_alloc(Of V)(Extra_info As V, Memory_byte_size As uint, Process As Func(Of V, space, V)) As V
            Return methods(Of V).alloc(Memory_byte_size, Extra_info, Process)
        End Function
        '''<summary>Create array on stack memory.</summary>
        <Extension, Method(inline)>
        Public Function stack_alloc(Of T, V)(Extra_info As V, Array_length As Integer, Process As Func(Of V, T(), V)) As V
            Return methods(Of T, V).alloc(Array_length, Extra_info, Process)
        End Function

        <Extension, Method(inline)>
        Public Function [do](Of V, S)(Variant_data As V, Static_data As S,
                                      Until As Func(Of V, S, Boolean),
                                      [Loop] As Func(Of V, S, V)) As V
            Return methods(Of V, S).recur(Variant_data, Static_data, Until, [Loop])
        End Function
        <Extension, Method(inline)>
        Public Function [do](Of T)(Data As T, Until As Func(Of T, Boolean), [Loop] As Func(Of T, T)) As T
            'Return "la.2.0.1 used f: la ; : la.3.0.1 used.1 la.1.2.3 re use-me".func(Data, Until, [Loop], DirectCast(Nothing, T))
            Return methods(Of T).recur(Data, Until, [Loop])
        End Function
    End Module
End Namespace