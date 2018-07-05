<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>
    <xsl:template match="/">
      <html>
        <head>
          <title>
            <xsl:value-of select="/Book/Title"/>
          </title>
        </head>
        <body>
          <b>Author:</b><xsl:value-of select="/Book/Author"/><br></br>
          Chapters:
          <table border="1">
            <xsl:for-each select="/Book/Chapters/Chapter">
              <tr>
                <td>
                  <xsl:value-of select="."/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
