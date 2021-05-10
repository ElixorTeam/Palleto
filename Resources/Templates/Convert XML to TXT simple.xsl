<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:xs="http://www.w3.org/2001/XMLSchema">
<xsl:output method="text" encoding="UTF-16" omit-xml-declaration="no"/>

<xsl:template match="/">

<xsl:text>
^XA</xsl:text>

<xsl:text>
^CI28
^LH60,15
^CWZ,E:COU000.TTF
^CWX,E:COU001.TTF
^CWY,E:COU002.TTF
^CWW,E:COU003.TTF
</xsl:text>
<!-- размер этикеток
 6 dots = 1 mm, 152 dots = 1 inch
 8 dots = 1 mm, 203 dots = 1 inch
 11.8 dots = 1 mm, 300 dots = 1 inch
 24 dots = 1 mm, 608 dots = 1 inch -->
<!--
<xsl:variable name="width" select="80" />
<xsl:variable name="length" select="98" />

<xsl:variable name="length" select="60" />
<xsl:variable name="width" select="90" />
<xsl:text>
^LL</xsl:text><xsl:value-of select="$length*8" />
<xsl:text>
^PW</xsl:text><xsl:value-of select="$width*8" />
-->

<xsl:text>
^FO350,10
^CFZ,15,15
^FB350,5,0,J,0
</xsl:text>
<!-- адрес предприятия -->
<xsl:text>^FH^FD</xsl:text>
<xsl:text>Изготовитель: ООО "Владимирский стандарт" Россия, 600910 Владимирская обл. г.Радужный квартал 13/13 дом 20 Данную информацию считать приоритетной</xsl:text>
<xsl:text>^FS</xsl:text>

<xsl:text>
^FO10,100
^CFX,28,20
^FB700,4,0,J,0
</xsl:text>
<!-- полное наименование номенклатуры -->
<xsl:text>^FH^FD</xsl:text>
<xsl:value-of select="/WeighingFactEntity/PLU/GoodsFullName"/>
<xsl:text>^FS</xsl:text>

<xsl:text>
^FO10,210
^CFZ,22,10
^FB700,4,0,J,0
</xsl:text>
<!-- описание номенклатуры -->
<xsl:text>^FH^FD</xsl:text>
<xsl:value-of select="/WeighingFactEntity/PLU/GoodsDescription"/>
<xsl:text>^FS</xsl:text>


<!-- дата производства -->
<xsl:text>
^FO10,280
^CFZ,16,16
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FDДата изгот.: </xsl:text>
<xsl:text>^FS</xsl:text>

<xsl:text>
^FO10,300
^CFX,30,25
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FD</xsl:text>
<xsl:variable name="dt" select="/WeighingFactEntity/ProductDate"/>
<xsl:value-of select="concat(substring($dt,9,2),'.',substring($dt,6,2),'.',substring($dt,1,4))"/>
<xsl:text/>
<xsl:text>^FS</xsl:text>

<!-- срок годности -->
<xsl:text>
^FO220,280
^CFZ,16,16
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FDГоден до: </xsl:text>
<xsl:text>^FS</xsl:text>

<xsl:text>
^FO220,300
^CFX,30,25
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FD</xsl:text>
<xsl:variable name="et" select="/WeighingFactEntity/ExpirationDate"/>
<xsl:value-of select="concat(substring($et,9,2),'.',substring($et,6,2),'.',substring($et,1,4))"/>
<xsl:text/>
<xsl:text>^FS</xsl:text>



<!-- вес нетто -->

<xsl:text>
^FO420,280
^CFZ,16,16
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FDВес нетто: ^FS</xsl:text>

<xsl:text>
^FO420,300
^CFX,30,25
^FB170,1,0,L,0
</xsl:text>
<xsl:text>^FH^FD</xsl:text>
<xsl:variable name="netweight" select="/WeighingFactEntity/NetWeight"/>
<xsl:value-of select="$netweight div 1000"/>
<xsl:text> кг</xsl:text>
<xsl:text>^FS</xsl:text>


<xsl:text>
^FO10,340
^CFZ,16,16
^FB200,1,0,L,0
</xsl:text>
<!-- замес -->
<xsl:text>^FH^FDЗамес: </xsl:text>
<xsl:value-of select="/WeighingFactEntity/KneadingNumber"/>
<xsl:text>^FS</xsl:text>

<xsl:text>
^FO200,340
^CFZ,16,16
^FB200,1,0,L,0
</xsl:text>
<!-- цех - линия - весовой пост -->
<xsl:text>^FH^FDЦех/Линия: </xsl:text>
<xsl:value-of select="/WeighingFactEntity/Scale/Description"/>
<xsl:text>^FS
</xsl:text>

<!-- штрихкод SSCC -->
<xsl:text>
^FO5,5
^BCN,50,Y,N,Y,D
</xsl:text>
<xsl:text>^FD</xsl:text>
<xsl:value-of select="/WeighingFactEntity/Sscc/SynonymSSCC"/>
<xsl:text>^FS
</xsl:text>

<!-- штрихкод GS1 -->
<xsl:text>
^FO5,360
^BCN,60,Y,N,Y,D
</xsl:text>
<xsl:text>^FD</xsl:text>
<xsl:text>(01)</xsl:text><xsl:value-of select="/WeighingFactEntity/PLU/GTIN"/>
<xsl:variable name="nw" select="/WeighingFactEntity/NetWeight"/>
<xsl:text>(3103)</xsl:text><xsl:value-of select="substring(concat('000000',$nw),string-length($nw)+1,6)"/>
<xsl:variable name="xt" select="/WeighingFactEntity/ProductDate"/>
<xsl:text>(11)</xsl:text><xsl:value-of select="concat(substring($xt,3,2), substring($xt,6,2), substring($xt,9,2) )"/>
<xsl:text>(10)</xsl:text><xsl:value-of select="concat(substring($xt,6,2), substring($xt,9,2) )"/><xsl:text>&gt;8</xsl:text>
<xsl:text>(21)</xsl:text><xsl:value-of select="/WeighingFactEntity/Sscc/UnitID"/><xsl:text>&gt;8</xsl:text>
<xsl:text>^FS
</xsl:text>

<xsl:text>
^XZ</xsl:text><xsl:text>
</xsl:text>
</xsl:template>
</xsl:stylesheet>


