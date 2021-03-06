﻿Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text
Imports ZXing
'Imports ThoughtWorks.QRCode.Codec
Module MD_GLOBALMOD
    Public Function GetCurrentAge(ByVal dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function

#Region "BindingSource"
    Public Function BS(ByVal BD As BindingSource, ROW As Integer, COL As Integer) As String
        Try
            Dim STR As String = CType(BD.List(ROW), DataRowView).Item(COL).ToString
            Return STR
        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region

#Region "REMOVE CHARACTER"
    Function RemoveCharacter(ByVal stringToCleanUp, ByVal characterToRemove)
        Return stringToCleanUp.Replace(characterToRemove, "")
    End Function
#End Region


#Region "TEXT WITH COMMA"
    Public Function TXTSETTO_0(ByVal txt As TextBox, intdec As Boolean) As String
        Dim ttxt_0 As String = "0.00"

        If (String.IsNullOrEmpty(txt.Text)) Then
            ttxt_0 = If(intdec, "0", "0.00")
        Else
            ttxt_0 = If(txt.Text.StartsWith(","), txt.Text.Substring(1), txt.Text)
        End If
        txt.Select(STARTINGPOSITION + 1, 0)
        Return If(intdec, CDec(ttxt_0).ToString("N0"), CDec(ttxt_0).ToString("N2"))
    End Function
#End Region

    Public Function BS_SINGLEROW(ByVal BD As BindingSource, COL As Integer) As String
        Dim STR As String = ""
        If BD.Count > 0 Then
            STR = CStr(If(IsDBNull(CType(BD.List(0), DataRowView).Item(COL)), "", CType(BD.List(0), DataRowView).Item(COL)))
        End If
        Return STR
    End Function

    Public Function REQFIELDVALIDATION(ByVal ctrl As Control) As Boolean
        Dim REQFIELD As Boolean = False
        If ctrl.Text.Length < 1 Then
            REQFIELD = True
        End If
        Return REQFIELD
    End Function

#Region "image"
    Public Function IMGTOBYTE(ByVal pb As PictureBox) As Byte()
        Dim psimg As New MemoryStream
        pb.BackgroundImage.Save(psimg, ImageFormat.Jpeg)
        Dim pbbyte As Byte() = psimg.ToArray
        Return pbbyte
    End Function

    'Public Function IMGPATHTOPBOX(ByVal path As String, ByVal pb As PictureBox)
    '    'pb.Image = Image.FromFile()
    '    Try
    '        Using fs As New FileStream(path, FileMode.Open)
    '            pb.Image = New Bitmap(Image.FromStream(fs))
    '            Return pb
    '        End Using
    '    Catch ex As Exception
    '        pb.Image = My.Resources._480px_No_image_available_svg
    '        Return pb
    '    End Try
    'End Function

    Public Function BYTETOIMG(ByVal byt As Byte()) As Image
        Dim ms As New IO.MemoryStream(CType(byt, Byte()))
        Dim returnImage As Image = Image.FromStream(ms)
        Return returnImage
    End Function

    'Public Function SAVEIMAGE_TOFOLDER(ByVal FOLDERNAME As String, FILNAME As String, IMG_PBX As Image) As String
    '    If Not Directory.Exists(My.Settings.PIS_ITEM_DIR & FOLDERNAME) Then
    '        'My.Computer.FileSystem.DeleteDirectory(My.Settings.PIS_ITEM_DIR & FOLDERNAME, FileIO.DeleteDirectoryOption.DeleteAllContents)
    '        Directory.CreateDirectory(My.Settings.PIS_ITEM_DIR & FOLDERNAME)
    '    End If
    '    IMG_PBX.Save(My.Settings.PIS_ITEM_DIR & FOLDERNAME & "\" & FILNAME & ".jpg", ImageFormat.Jpeg)
    '    Return My.Settings.PIS_ITEM_DIR & FOLDERNAME & "\" & FILNAME & ".jpg"
    'End Function
#End Region

#Region "QRCODE GENERATOR"
    'Public Function QRCODEGenerator(str As String) As Bitmap
    '    'If lblParts_Code.Text.Length > 0 Then
    '    Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
    '    Dim imgImage As Image
    '    Dim objBitmap As Bitmap

    '    objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
    '    objQRCode.QRCodeScale = 6
    '    objQRCode.QRCodeVersion = 0
    '    objQRCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
    '    imgImage = objQRCode.Encode(str)
    '    objBitmap = New Bitmap(imgImage)
    '    Return objBitmap
    'End Function
#End Region

#Region "SCANNING MAKEDIR"
    '  Public Sub SCANNEDFILEDIR_JPEG(ByVal FOLDERNAME As String, FILNAME As String, PRNO As String, IMG_CTR As Integer)
    'Public Sub SCANNEDFILEDIR_JPEG(ByVal FOLDERNAME As String, FILNAME As String, TDNUM As String, IMG_PBX As Image, IMG_CTR As Integer)
    '    If Not Directory.Exists(My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\") Then
    '        Directory.CreateDirectory(My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\")
    '    End If
    '    IMG_PBX.Save(My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\" & FILNAME & " (" & PRNO & ")_" & IMG_CTR & ".jpg", ImageFormat.Jpeg)
    'End Sub
    'Public Function SCANNEDFILEDIR_JPEG_FOLDER(ByVal FOLDERNAME As String, FILNAME As String, PRNO As String) As String
    '    If Not Directory.Exists(My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\") Then
    '        Directory.CreateDirectory(My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\")
    '    End If
    '    Return My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ")\"
    'End Function

    'Public Function SCANNEDFILEDIR(ByVal FOLDERNAME As String, FILNAME As String, PRNO As String) As String
    '    If Not Directory.Exists(My.Settings.PIS_DIR & FOLDERNAME & "\") Then
    '        Directory.CreateDirectory(My.Settings.PIS_DIR & FOLDERNAME & "\")
    '    End If
    '    Return My.Settings.PIS_DIR & FOLDERNAME & "\" & FILNAME & " (" & PRNO & ").pdf"
    'End Function

#End Region

#Region "RANDOM_CODE"
    Public Function RandomCode() As String
        Dim characters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 9
            Dim idx As Integer = rand.Next(0, 35)
            sb.Append(characters.Substring(idx, 1))
        Next
        '   Return sb.ToString() + Today.Month.ToString() + Today.Year.ToString()
        Return sb.ToString()
    End Function



    Public Function RandomGenCode(digits As Integer) As String
        Dim characters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To digits
            Dim idx As Integer = rand.Next(0, 35)
            sb.Append(characters.Substring(idx, 1))
        Next
        '   Return sb.ToString() + Today.Month.ToString() + Today.Year.ToString()
        Return sb.ToString()
    End Function
#End Region

#Region "FUNCTION FOR EDITABLE DGV"

    Public Function DGV_VALUE(ByVal DGV As DataGridView, COL As Integer, RW As Integer) As String
        Return DGV(COL, RW).Value
    End Function

    'Public Sub BS_DGV_STANDARD(ByVal BSMAIN As BindingSource, BSTRANSFER As BindingSource)
    '    For rw = 0 To BSMAIN.Count - 1
    '        dgv.Rows.Add()

    '        dgv(cl, rw).Value = CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl)), "", CType(BD.List(rw), DataRowView).Item(cl)))

    '    Next
    '  End Sub

    Public Sub BS_DGV_APPLIST_CHK(ByVal BD As BindingSource, DGV As DataGridView, COL_COUNT As Integer)
        For i = 0 To BD.Count - 1
            DGV.Rows.Add()
            For x = 1 To COL_COUNT
                DGV(x, i).Value = If(x = 13, CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(0)), "", CType(BD.List(i), DataRowView).Item(0))), CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(x)), "", CType(BD.List(i), DataRowView).Item(x))))
            Next
        Next
    End Sub

    Public Sub BS_DGV_APPLIST(ByVal BD As BindingSource, DGV As DataGridView, COL_COUNT As Integer)
        For i = 0 To BD.Count - 1
            DGV.Rows.Add()
            For x = 1 To COL_COUNT
                DGV(x, i).Value = CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(x)), "", CType(BD.List(i), DataRowView).Item(x)))
            Next
        Next
    End Sub

    Public Sub BS_DGV_BANKCERT(ByVal BD As BindingSource, DGV As DataGridView, COL_COUNT As Integer)
        For rw = 0 To BD.Count - 1
            DGV.Rows.Add()
            For cl = 0 To COL_COUNT
                If cl = 0 Or cl = 20 Or cl = 21 Or cl = 22 Or cl = 30 Then
                    DGV(If(cl = 0, 3, If(cl = 20, 0, If(cl = 21, 1, If(cl = 22, 2, 4)))), rw).Value = CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl)), "", CType(BD.List(rw), DataRowView).Item(cl)))
                End If
            Next
        Next
    End Sub

    Public Sub BS_DGV_BANKCERT_CHK(ByVal BD As BindingSource, DGV As DataGridView, COL_COUNT As Integer)
        For rw = 0 To BD.Count - 1
            DGV.Rows.Add()
            For cl = 0 To COL_COUNT
                DGV(cl, rw).Value = CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl)), "", CType(BD.List(rw), DataRowView).Item(cl)))
            Next
        Next
    End Sub

    Public Sub BS_DGV_CHKPAYMENT(ByVal BD As BindingSource, DGV As DataGridView, COL_COUNT As Integer)
        For rw = 0 To BD.Count - 1
            DGV.Rows.Add()
            For cl = 0 To COL_COUNT
                DGV(cl, rw).Value = If(cl = 1, If(CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl + 1)), "", CType(BD.List(rw), DataRowView).Item(cl + 1))) = "USD", "$ ", "₱ ") & CDec(CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl)), "", CType(BD.List(rw), DataRowView).Item(cl)))).ToString("N2"), CStr(If(IsDBNull(CType(BD.List(rw), DataRowView).Item(cl)), "", CType(BD.List(rw), DataRowView).Item(cl))))
                If cl = 3 Then
                    DGV(cl, rw).Value = If(DGV(cl, rw).Value = "True", 1, 0)
                End If
            Next
        Next
    End Sub

    Public Sub DGV_REMOVE_EMPTY_ROWS(ByVal DGV As DataGridView)
        For r As Integer = DGV.Rows.Count - 1 To 0 Step -1
            Dim empty As Boolean = True
            For Each cell As DataGridViewCell In DGV.Rows(r).Cells
                If Not IsNothing(cell.Value) Then
                    empty = False
                    Exit For
                End If
            Next
            If empty Then DGV.Rows.RemoveAt(r)
        Next
    End Sub

    Public Function DGV_APPREQ_VALIDATION(ByVal DGV As DataGridView) As Boolean
        Dim INCOMPLETE As Boolean
        Dim cell_ctr As Integer = 0
        For r As Integer = DGV.Rows.Count - 1 To 0 Step -1
            INCOMPLETE = False
            For Each cell As DataGridViewCell In DGV.Rows(r).Cells
                If cell_ctr > 1 And cell_ctr < 11 Then
                    If Trim((cell.Value)).Length = 0 Or CStr(Trim((cell.Value))).Contains("NOT COM") Then
                        INCOMPLETE = True
                        Exit For
                    End If
                End If
                cell_ctr += 1
            Next
            If INCOMPLETE Then
                Exit For
            End If
        Next
        Return INCOMPLETE
    End Function
#End Region

#Region "BS_TRANSFER"
    Public Sub BS_DGV_TRANSFER(ByVal BSMAIN As BindingSource, BSTRANSFER As DataTable)
        For i = 0 To BSMAIN.Count - 1
            BSTRANSFER.Rows.Add(CStr(If(IsDBNull(CType(BSMAIN.List(i), DataRowView).Item(0)), "", CType(BSMAIN.List(i), DataRowView).Item(0))),
                                                 CStr(If(IsDBNull(CType(BSMAIN.List(i), DataRowView).Item(1)), "", CType(BSMAIN.List(i), DataRowView).Item(1))),
                                                 CStr(If(IsDBNull(CType(BSMAIN.List(i), DataRowView).Item(2)), "", CType(BSMAIN.List(i), DataRowView).Item(2))))
        Next
    End Sub

    Public Sub BS_DGV_ITEM_LIST_DGV(ByVal BD As BindingSource, DAGV As DataGridView, COL_COUNT As Integer)
        For i = 0 To BD.Count
            DAGV.Rows.Add(CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(2)), "", CType(BD.List(i), DataRowView).Item(2))),
                          CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(1)), "", CType(BD.List(i), DataRowView).Item(1))),
                            CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(3)), "", CType(BD.List(i), DataRowView).Item(3))))
            'For x = 1 To COL_COUNT
            '    DAGV(x, i).Value = If(x = 13, CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(0)), "", CType(BD.List(i), DataRowView).Item(0))), CStr(If(IsDBNull(CType(BD.List(i), DataRowView).Item(x)), "", CType(BD.List(i), DataRowView).Item(x))))
            'Next
        Next
    End Sub
#End Region



#Region "ENCRYPTION"
    Public Class SECURITY
        Public Function ENCRYPT(ByVal Text As String) As String
            ' Encrypts/decrypts the passed string using 
            ' a simple ASCII value-swapping algorithm
            Dim TempChar As String = ""
            Dim x As Integer
            For x = 1 To Len(Text)
                If Asc(Mid$(Text, x, 1)) < 128 Then
                    TempChar = CType(Asc(Mid$(Text, x, 1)) + 128, String)
                ElseIf Asc(Mid$(Text, x, 1)) > 128 Then
                    TempChar = CType(Asc(Mid$(Text, x, 1)) - 128, String)
                End If
                Mid$(Text, x, 1) = Chr(CType(TempChar, Integer))
            Next x
            Return Text
        End Function
    End Class

#End Region

    Public Function GenerateCode(name As String) As String
        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        Dim result = writer.Write(name)
        Dim path As String = Application.StartupPath() + "\Media_Files\QRs\" & name & ".jpg"
        Dim barcodeBitmap = New Bitmap(result)

        Using memory As New MemoryStream()
            Using fs As New FileStream(path, FileMode.Create, FileAccess.ReadWrite)
                barcodeBitmap.Save(memory, ImageFormat.Jpeg)
                Dim bytes As Byte() = memory.ToArray()
                fs.Write(bytes, 0, bytes.Length)
            End Using
        End Using
        Return path
    End Function
End Module
