<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:tesl="urn:http://www.tesl.com/IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.dll#SkillsReportXsltExtension"
                >
  <xsl:template match="/">
    <InterpretedSkillsDocument>
      <xsl:variable name ="SHCResult" select ="tesl:GetSHCReportResult(SkillsDocument/DataValues)"/>
      <xsl:variable name="lcletters">abcdefghijklmnopqrstuvwxyz</xsl:variable>
      <xsl:variable name="ucletters">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
      <xsl:variable name="CandidateFullName" select ="SkillsDocument/DataValues/SkillsDocumentDataValue[Title='CandidateFullName']/Value" />
      <WordTemplateName>
        SHCFullReport.docx
      </WordTemplateName>
      <CandidateFullName>
        <![CDATA[
           <w:p w:rsidR="00DE44E6" w:rsidP="000E3AA6" w:rsidRDefault="00DE44E6">
            <w:pPr>
              <w:autoSpaceDE w:val="0" />
              <w:autoSpaceDN w:val="0" />
              <w:adjustRightInd w:val="0" />
              <w:jc w:val="right" />
              <w:rPr>
                <w:rFonts w:ascii="Arial" w:hAnsi="Arial" w:cs="Arial" />
                <w:sz w:val="40" />
                <w:szCs w:val="40" />
                <w:lang w:eastAsia="zh-TW" />
              </w:rPr>
            </w:pPr>
            <w:r>
              <w:rPr>
                <w:rFonts w:ascii="Arial" w:hAnsi="Arial" w:cs="Arial" />
                <w:sz w:val="40" />
                <w:szCs w:val="40" />
                <w:lang w:eastAsia="zh-TW" />
              </w:rPr>
              <w:t>]]><xsl:choose><xsl:when test="$CandidateFullName != '' and translate($CandidateFullName,$ucletters,$lcletters) != 'anonymous'"><xsl:value-of select="$CandidateFullName"/></xsl:when></xsl:choose><![CDATA[</w:t>
                </w:r></w:p>]]>
      </CandidateFullName>
      <Currentdate>
        <![CDATA[<w:p w:rsidRPr="00492174" w:rsidR="004D555A" w:rsidP="00443786" w:rsidRDefault="004D555A">
            <w:pPr>
              <w:ind w:right="4" />
              <w:jc w:val="right" />
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:sz w:val="40" />
                <w:szCs w:val="40" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="00411307">
              <w:rPr>
                <w:rFonts w:ascii="Arial" w:hAnsi="Arial" w:cs="Arial" />
                <w:sz w:val="20" />
                <w:szCs w:val="20" />
                <w:lang w:eastAsia="zh-TW" />
              </w:rPr>
              <w:t>]]><xsl:value-of select ="$SHCResult/Root/CurrentDate"/><![CDATA[</w:t>
            </w:r>
          </w:p>]]>
      </Currentdate>
      <Level>
        <xsl:choose>
          <xsl:when test="SkillsDocument/DataValues/SkillsDocumentDataValue[Title='Qualification.Level']/Value != ''">
            <xsl:value-of select="SkillsDocument/DataValues/SkillsDocumentDataValue[Title='Qualification.Level']/Value"/>
          </xsl:when>
          <xsl:otherwise>
            1
          </xsl:otherwise>
        </xsl:choose>
      </Level>
      <xsl:for-each select="$SHCResult/Root">
        <AllAssessmentsNotCompleted>
          <xsl:choose>
            <xsl:when test="ShowNumeric = 'True' and ShowMechanical = 'True' and ShowShapes = 'True'
                      and ShowAbstract = 'True' and ShowVerbal = 'True' and ShowChecking = 'True'
                      and ShowMotivation = 'True' and ShowInterest = 'True' and ShowSkills= 'True'
                      and ShowPersonalStyle = 'True'">
              false
            </xsl:when>
            <xsl:otherwise>
              true
            </xsl:otherwise>
          </xsl:choose>
        </AllAssessmentsNotCompleted>
        <ShowNumeric>
          <xsl:value-of select ="ShowNumeric"/>
        </ShowNumeric>
        <Numeric>
          <Timing>
            <xsl:value-of select ="Numeric/Timing"/>
          </Timing>
          <QuestionsAttempted>
            <xsl:value-of select ="Numeric/QuestionsAttempted"/>
          </QuestionsAttempted>
          <FormattedQuestionsAttempted>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Numeric/QuestionsAttempted"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsAttempted>
          <TotalQuestions>
            <xsl:value-of select ="Numeric/TotalQuestions"/>
          </TotalQuestions>
          <FormattedTotalQuestions>
            <![CDATA[ 
            <w:bookmarkStart w:name="_GoBack" w:id="0" />
            <w:bookmarkEnd w:id="0" />
            <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Numeric/TotalQuestions"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedTotalQuestions>
          <QuestionsCorrect>
            <xsl:value-of select ="Numeric/QuestionsCorrect"/>
          </QuestionsCorrect>
          <FormattedQuestionsCorrect>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Numeric/QuestionsCorrect"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsCorrect>
          <Style>
            <xsl:value-of select ="Numeric/Style"/>
          </Style>
          <Accuracy>
            <xsl:value-of select ="Numeric/Accuracy"/>
          </Accuracy>
          <OP>
            <xsl:value-of select ="Numeric/OverallPotential"/>
          </OP>
          <Ease>
            <xsl:value-of select ="Numeric/Ease"/>
          </Ease>
        </Numeric>
        <ShowMechanical>
          <xsl:value-of select ="ShowMechanical"/>
        </ShowMechanical>
        <Mechanical>
          <Timing>
            <xsl:value-of select ="Mechanical/Timing"/>
          </Timing>
          <QuestionsAttempted>
            <xsl:value-of select ="Mechanical/QuestionsAttempted"/>
          </QuestionsAttempted>
          <FormattedQuestionsAttempted>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Mechanical/QuestionsAttempted"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsAttempted>
          <TotalQuestions>
            <xsl:value-of select ="Mechanical/TotalQuestions"/>
          </TotalQuestions>
          <FormattedTotalQuestions>
            <![CDATA[ 
            <w:bookmarkStart w:name="_GoBack" w:id="0" />
            <w:bookmarkEnd w:id="0" />
            <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Mechanical/TotalQuestions"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedTotalQuestions>
          <QuestionsCorrect>
            <xsl:value-of select ="Mechanical/QuestionsCorrect"/>
          </QuestionsCorrect>
          <FormattedQuestionsCorrect>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Mechanical/QuestionsCorrect"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsCorrect>
          <Style>
            <xsl:value-of select ="Mechanical/Style"/>
          </Style>
          <Accuracy>
            <xsl:value-of select ="Mechanical/Accuracy"/>
          </Accuracy>
          <Band>
            <xsl:value-of select ="Mechanical/QuestionsCorrectBand"/>
          </Band>
          <!-- Physical Principles Percent -->
          <PPP>
            <xsl:value-of select ="Mechanical/PhysicalPrinciplesPercent"/>
          </PPP>
          <!-- Movement of Objects Percent -->
          <MOP>
            <xsl:value-of select ="Mechanical/MovementOfObjectsPercent"/>
          </MOP>
          <!-- Structure and weights Percent  -->
          <SWP>
            <xsl:value-of select ="Mechanical/StructureAndWeightsPercent"/>
          </SWP>
          <!--Mechanical type id -->
          <MID>
            <xsl:value-of select ="Mechanical/MechanicalTypeId"/>
          </MID>
          <Enjoyment>
            <xsl:value-of select ="Mechanical/Enjoyment"/>
          </Enjoyment>
          <Ease>
            <xsl:value-of select ="Mechanical/Ease"/>
          </Ease>
        </Mechanical>
        <ShowShapes>
          <xsl:value-of select ="ShowShapes"/>
        </ShowShapes>
        <Shapes>
          <!-- Timing -->
          <TM>
            <xsl:value-of select ="Shapes/Timing"/>
          </TM>
          <QuestionsAttempted>
            <xsl:value-of select ="Shapes/QuestionsAttempted"/>
          </QuestionsAttempted>
          <FormattedQuestionsAttempted>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Shapes/QuestionsAttempted"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsAttempted>
          <TotalQuestions>
            <xsl:value-of select ="Shapes/TotalQuestions"/>
          </TotalQuestions>
          <FormattedTotalQuestions>
            <![CDATA[ 
            <w:bookmarkStart w:name="_GoBack" w:id="0" />
            <w:bookmarkEnd w:id="0" />
            <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Shapes/TotalQuestions"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedTotalQuestions>
          <QuestionsCorrect>
            <xsl:value-of select ="Shapes/QuestionsCorrect"/>
          </QuestionsCorrect>
          <FormattedQuestionsCorrect>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />               
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Shapes/QuestionsCorrect"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsCorrect>
          <Style>
            <xsl:value-of select ="Shapes/Style"/>
          </Style>
          <Accuracy>
            <xsl:value-of select ="Shapes/Accuracy"/>
          </Accuracy>
          <Band>
            <xsl:value-of select ="Shapes/QuestionsCorrectBand"/>
          </Band>
          <Enjoyment>
            <xsl:value-of select ="Shapes/Enjoyment"/>
          </Enjoyment>
          <Ease>
            <xsl:value-of select ="Shapes/Ease"/>
          </Ease>
          <Band>
            <xsl:value-of select ="Shapes/QuestionsCorrectBand"/>
          </Band>
        </Shapes>
        <ShowAbstract>
          <xsl:value-of select ="ShowAbstract"/>
        </ShowAbstract>
        <Abstract>
          <!-- Timing -->
          <TM>
            <xsl:value-of select ="Abstract/Timing"/>
          </TM>
          <QuestionsAttempted>
            <xsl:value-of select ="Abstract/QuestionsAttempted"/>
          </QuestionsAttempted>
          <FormattedQuestionsAttempted>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Abstract/QuestionsAttempted"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsAttempted>
          <TotalQuestions>
            <xsl:value-of select ="Abstract/TotalQuestions"/>
          </TotalQuestions>
          <FormattedTotalQuestions>
            <![CDATA[ 
            <w:bookmarkStart w:name="_GoBack" w:id="0" />
            <w:bookmarkEnd w:id="0" />
            <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Abstract/TotalQuestions"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedTotalQuestions>
          <QuestionsCorrect>
            <xsl:value-of select ="Abstract/QuestionsCorrect"/>
          </QuestionsCorrect>
          <FormattedQuestionsCorrect>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Abstract/QuestionsCorrect"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsCorrect>
          <Style>
            <xsl:value-of select ="Abstract/Style"/>
          </Style>
          <Accuracy>
            <xsl:value-of select ="Abstract/Accuracy"/>
          </Accuracy>
          <Band>
            <xsl:value-of select ="Abstract/QuestionsCorrectBand"/>
          </Band>
          <Enjoyment>
            <xsl:value-of select ="Abstract/Enjoyment"/>
          </Enjoyment>
          <Ease>
            <xsl:value-of select ="Abstract/Ease"/>
          </Ease>
          <Band>
            <xsl:value-of select ="Abstract/QuestionsCorrectBand"/>
          </Band>
          <!--Reflection Percent-->
          <REF>
            <xsl:value-of select ="Abstract/ReflectionPercent"/>
          </REF>
          <!--Rotation Percent-->
          <ROT>
            <xsl:value-of select ="Abstract/RotationPercent"/>
          </ROT>
          <!--Movement Percent-->
          <MOV>
            <xsl:value-of select ="Abstract/MovementPercent"/>
          </MOV>
          <!--Repetition Percent-->
          <REP>
            <xsl:value-of select ="Abstract/RepetitionPercent"/>
          </REP>
          <!--Abstract type id-->
          <AID>
            <xsl:value-of select ="Abstract/AbstractTypeId"/>
          </AID>
        </Abstract>
        <ShowVerbal>
          <xsl:value-of select ="ShowVerbal"/>
        </ShowVerbal>
        <Verbal>
          <Timing>
            <xsl:value-of select ="Verbal/Timing"/>
          </Timing>
          <QuestionsAttempted>
            <![CDATA[
              <w:r>
                <w:rPr>
                  <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                  <w:b />
                </w:rPr>
                <w:t>]]><xsl:value-of select ="Verbal/QuestionsAttempted"/><![CDATA[</w:t>
              </w:r>
            ]]>
          </QuestionsAttempted>
          <TotalQuestions>
            <![CDATA[
              <w:r>
                <w:rPr>
                  <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                  <w:b />
                </w:rPr>
                <w:t>]]><xsl:value-of select ="Verbal/TotalQuestions"/><![CDATA[</w:t>
              </w:r>
            ]]>
          </TotalQuestions>
          <QuestionsCorrect>
            <![CDATA[
              <w:r>
                <w:rPr>
                  <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                  <w:b />
                </w:rPr>
                <w:t>]]><xsl:value-of select ="Verbal/QuestionsCorrect"/><![CDATA[</w:t>
              </w:r>
            ]]>
          </QuestionsCorrect>
          <Style>
            <xsl:value-of select ="Verbal/Style"/>
          </Style>
          <Accuracy>
            <xsl:value-of select ="Verbal/Accuracy"/>
          </Accuracy>
          <OP>
            <xsl:value-of select ="Verbal/OverallPotential"/>
          </OP>
          <Ease>
            <xsl:value-of select ="Verbal/Ease"/>
          </Ease>
        </Verbal>
        <ShowChecking>
          <xsl:value-of select ="ShowChecking"/>
        </ShowChecking>
        <Checking>
          <Timing>
            <xsl:value-of select ="Checking/Timing"/>
          </Timing>
          <Band>
            <xsl:value-of select ="Checking/Band"/>
          </Band>          
          <HST>
            <xsl:value-of select ="Checking/HST"/>
          </HST>
          <LST>
            <xsl:value-of select ="Checking/LST"/>
          </LST>
          <FormattedQuestionsCorrect>
            <![CDATA[ <w:r w:rsidRPr="00470810">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />                
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Checking/QuestionsCorrect"/><![CDATA[</w:t>
            </w:r>]]>
          </FormattedQuestionsCorrect>          
          <Ease>
            <xsl:value-of select ="Checking/Ease"/>
          </Ease>
          <Enjoyment>
            <xsl:value-of select ="Checking/Enjoyment"/>
          </Enjoyment>
        </Checking>
        <ShowMotivation>
          <xsl:value-of select ="ShowMotivation"/>
        </ShowMotivation>
        <Motivation>
          <xsl:for-each select ="Motivation//MotivationCategory">
            <MotivationCategory>
              <RowNum>
                <![CDATA[ <w:p w:rsidRPr="00C15C53" w:rsidR="006654A3" w:rsidP="00CB67F5" w:rsidRDefault="002B0F8A">
                                <w:pPr>
                                  <w:rPr>
                                    <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                  </w:rPr>
                                </w:pPr>
                                <w:r>
                                  <w:rPr>
                                    <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                  </w:rPr>
                                  <w:t>]]><xsl:value-of select ="position()"/><![CDATA[</w:t>
                                </w:r>
                              </w:p>
                     ]]>
              </RowNum>
              <Category>
                <xsl:value-of select ="Category"/>
              </Category>
              <Score>
                <xsl:value-of select ="Score"/>
              </Score>
              <!--Scale Score-->
              <SS>
                <xsl:value-of select ="ScaleScore"/>
              </SS>
              <Name>
                <![CDATA[<w:r w:rsidRPr="00AC5C86" w:rsidR="00F27B01">
                            <w:rPr>
                              <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                              <w:b />
                              <w:bCs />
                              <w:color w:val="000000" />
                            </w:rPr>
                            <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
                                </w:r>]]>
              </Name>
              <Definition>
                <![CDATA[ <w:p w:rsidRPr="007D1B1B" w:rsidR="006654A3" w:rsidP="00F27B01" w:rsidRDefault="00F27B01">
                            <w:pPr>
                              <w:rPr>
                                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                <w:bCs />
                                <w:color w:val="000000" />
                                <w:sz w:val="20" />
                                <w:szCs w:val="20" />
                              </w:rPr>
                            </w:pPr>
                            <w:r w:rsidRPr="00CA24DC">
                              <w:rPr>
                                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                <w:bCs />
                                <w:color w:val="000000" />
                              </w:rPr>
                              <w:t>]]><xsl:value-of select ="Definition"/><![CDATA[</w:t>
                                  </w:r>                    
                                </w:p>]]>
              </Definition>
              <FC>B2A1C7</FC>
              <NC>FFFFFF</NC>
            </MotivationCategory>
          </xsl:for-each>
        </Motivation>
        <ShowInterest>
          <xsl:value-of select ="ShowInterest"/>
        </ShowInterest>
        <Interest>
          <QualificationLevel>
            <xsl:value-of select ="Interest/QualificationLevel"/>
          </QualificationLevel>
          <xsl:for-each select ="Interest//InterestCategory">
            <xsl:variable name="CategoryPosition" select ="position()"/>
            <xsl:variable name="CategoryColour" select ="Colour"/>
            <xsl:variable name="StengthName" select ="Name"/>
            <xsl:for-each select="InterestItem">
              <xsl:variable name="ItemPosition" select ="position()"/>
              <InterestItem>
                <StrengthName>
                  <![CDATA[<w:r w:rsidRPr="00FF6352" w:rsidR="00E86C11">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                          <w:b />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select ="$StengthName"/><![CDATA[</w:t>
                        </w:r>]]>
                </StrengthName>
                <InterestArea>
                  <![CDATA[<w:p w:rsidRPr="00FF6352" w:rsidR="00E86C11" w:rsidP="003E4976" w:rsidRDefault="00E86C11">
                    <w:pPr>
                      <w:rPr>
                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        <w:b />
                        <w:bCs />
                      </w:rPr>
                    </w:pPr>
                    <w:r w:rsidRPr="00FF6352">
                      <w:rPr>
                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        <w:b />
                        <w:bCs />
                      </w:rPr>
                      <w:t>]]><xsl:value-of select ="Name"/> <![CDATA[</w:t>
                    </w:r>
                  </w:p>]]>
                </InterestArea>
                <JobFamilies>
                  <![CDATA[<w:p w:rsidRPr="00FF6352" w:rsidR="00E86C11" w:rsidP="003E4976" w:rsidRDefault="00E86C11">
                  <w:pPr>
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                      <w:color w:val="000000" />
                    </w:rPr>
                  </w:pPr>
                  <w:r w:rsidRPr="00FF6352">
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                      <w:color w:val="000000" />
                    </w:rPr>
                    <w:t>]]><xsl:for-each select="RelatedJobFamilies//Job">
                    <xsl:value-of select ="."/>
                    <xsl:if test="not(position() = last())"><![CDATA[</w:t><w:br /><w:t>]]></xsl:if>
                  </xsl:for-each><![CDATA[</w:t>
                  </w:r>
                </w:p>]]>
                </JobFamilies>
                <InterestDefinition>
                  <![CDATA[<w:p w:rsidRPr="00FF6352" w:rsidR="00E86C11" w:rsidP="003E4976" w:rsidRDefault="00E86C11">
                      <w:pPr>
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF6352">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select ="WhatItInvolves"/> <![CDATA[</w:t>
                      </w:r>
                      <w:bookmarkStart w:name="_GoBack" w:id="0" />
                      <w:bookmarkEnd w:id="0" /> </w:p>]]>
                </InterestDefinition>
                <ShowHeader>
                  <xsl:choose>
                    <xsl:when test="$ItemPosition = 1">
                      true
                    </xsl:when>
                    <xsl:otherwise>
                      false
                    </xsl:otherwise>
                  </xsl:choose>
                </ShowHeader>
                <Colour>
                  <xsl:value-of select ="$CategoryColour"/>
                </Colour>
              </InterestItem>
            </xsl:for-each>
          </xsl:for-each >
        </Interest>
        <ShowSkills>
          <xsl:value-of select ="ShowSkills"/>
        </ShowSkills>
        <Skills>
          <QualificationLevel>
            <xsl:value-of select ="Skills/QualificationLevel"/>
          </QualificationLevel>
          <xsl:for-each select ="Skills//SkillsCategory">
            <SkillsCategory>
              <RowNum>
                <![CDATA[<w:p w:rsidRPr="00CD76BF" w:rsidR="00565BCD" w:rsidP="003A123C" w:rsidRDefault="00565BCD">
            <w:pPr>
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />
                <w:bCs />
              </w:rPr>
            </w:pPr>
            <w:r>
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />
                <w:bCs />
              </w:rPr>
              <w:t>]]><xsl:value-of select="position()"/><![CDATA[</w:t>
            </w:r>
          </w:p>]]>
              </RowNum>
              <Name>
                <![CDATA[<w:p w:rsidR="00565BCD" w:rsidP="003A123C" w:rsidRDefault="00565BCD">
            <w:pPr>
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />
                <w:bCs />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="00DF2027">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:b />
                <w:bCs />
                <w:noProof />
              </w:rPr>
              <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
            </w:r>
          </w:p>  ]]>

              </Name>
              <Definition>
                <![CDATA[<w:r w:rsidRPr="00DF2027" w:rsidR="00565BCD">
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                <w:bCs />
                <w:noProof />
              </w:rPr>
              <w:t>]]><xsl:value-of select="Definition"/><![CDATA[</w:t>
            </w:r> ]]>

              </Definition>
            </SkillsCategory>
          </xsl:for-each>
          <SkillsHigherCount>
            <xsl:value-of select="count(Skills//SkillsCategory[ScoreScale='Higher'])"/>
          </SkillsHigherCount>
          <xsl:for-each select ="Skills//SkillsCategory[ScoreScale='Higher']">
            <SkillsHigher>
              <HeaderName>
                <![CDATA[ <w:p w:rsidRPr="00623CE3" w:rsidR="00565BCD" w:rsidP="00B9735D" w:rsidRDefault="00B9735D">
                                    <w:pPr>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:b />
                                        <w:bCs />
                                      </w:rPr>
                                    </w:pPr>
                                    <w:r>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:b />
                                        <w:bCs />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
                                    </w:r>
                                  </w:p> ]]>
              </HeaderName>
              <DetailName>
                <![CDATA[<w:r w:rsidR="00B9735D">
                  <w:rPr>
                    <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    <w:noProof />
                  </w:rPr>
                  <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
                </w:r>]]>
              </DetailName>
              <Description>
                <![CDATA[<w:r w:rsidR="00B9735D">
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select="Description"/><![CDATA[</w:t>
                                    </w:r> ]]>

              </Description>
              <Strength>
                <xsl:value-of select ="Strength"/>
              </Strength>
            </SkillsHigher>
          </xsl:for-each>
          <SkillsLowerCount>
            <xsl:value-of select="count(Skills//SkillsCategory[ScoreScale='Lower'])"/>
          </SkillsLowerCount>
          <xsl:for-each select ="Skills//SkillsCategory[ScoreScale='Lower']">
            <SkillsLower>

              <HeaderName>
                <![CDATA[ <w:p w:rsidRPr="00623CE3" w:rsidR="00565BCD" w:rsidP="00B9735D" w:rsidRDefault="00B9735D">
                                    <w:pPr>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:b />
                                        <w:bCs />
                                      </w:rPr>
                                    </w:pPr>
                                    <w:r>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:b />
                                        <w:bCs />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
                                    </w:r>
                                  </w:p> ]]>
              </HeaderName>
              <DetailName>
                <![CDATA[<w:r w:rsidR="00B9735D">
                  <w:rPr>
                    <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    <w:noProof />
                  </w:rPr>
                  <w:t>]]><xsl:value-of select ="Name"/><![CDATA[</w:t>
                </w:r>]]>
              </DetailName>
              <Description>
                <![CDATA[<w:r w:rsidR="00B9735D">
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select="Description"/><![CDATA[</w:t>
                                    </w:r> ]]>

              </Description>
              <DevTip1>
                <![CDATA[<w:p w:rsidRPr="00535904" w:rsidR="00565BCD" w:rsidP="003A123C" w:rsidRDefault="00432974">
                                    <w:pPr>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:bCs />
                                      </w:rPr>
                                    </w:pPr>
                                    <w:r>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:bCs />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select ="DevTip1"/><![CDATA[</w:t>
                                    </w:r>
                                  </w:p> ]]>
              </DevTip1>
              <DevTip2>
                <![CDATA[<w:p w:rsidRPr="00535904" w:rsidR="00565BCD" w:rsidP="003A123C" w:rsidRDefault="00432974">
                                    <w:pPr>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:bCs />
                                      </w:rPr>
                                    </w:pPr>
                                    <w:r>
                                      <w:rPr>
                                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                                        <w:bCs />
                                        <w:noProof />
                                      </w:rPr>
                                      <w:t>]]><xsl:value-of select ="DevTip2"/><![CDATA[</w:t>
                                    </w:r>
                                  </w:p> ]]>
              </DevTip2>
            </SkillsLower>
          </xsl:for-each>
        </Skills>
        <ShowPersonalStyle>
          <xsl:value-of select="ShowPersonalStyle"/>
        </ShowPersonalStyle>
        <PersonalStyle>
          <ListOfStrength>
            <xsl:for-each select="PersonalStyle/ListOfStrength/Strength">
              <xsl:if test="Name != ''">
                <Strength>
                  <Name>
                    <![CDATA[  <w:p w:rsidRPr="00CB59C7" w:rsidR="00565BCD" w:rsidP="00045A5D" w:rsidRDefault="00D32266">
                  <w:pPr>
                    <w:pStyle w:val="ListParagraph" />
                    <w:numPr>
                      <w:ilvl w:val="0" />
                      <w:numId w:val="17" />
                    </w:numPr>
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    </w:rPr>
                  </w:pPr>
                  <w:r w:rsidRPr="00CB59C7">
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    </w:rPr>
                    <w:t>]]><xsl:value-of select="Name"/><![CDATA[</w:t>
                    </w:r>
                    <w:r w:rsidRPr="00CB59C7">
                      <w:rPr>
                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                      </w:rPr>
                      <w:br />
                    </w:r>
                  </w:p>]]>
                  </Name>
                </Strength>
              </xsl:if>
            </xsl:for-each>
          </ListOfStrength>
          <ListOfChallenges>
            <xsl:for-each select="PersonalStyle/ListOfChallenge/Challenge">
              <xsl:if test="Name != ''">
                <Challenge>
                  <Name>
                    <![CDATA[
                   <w:p w:rsidR="001A385B" w:rsidP="001A385B" w:rsidRDefault="001A385B">
                      <w:pPr>
                        <w:pStyle w:val="ListParagraph" />
                        <w:numPr>
                          <w:ilvl w:val="0" />
                          <w:numId w:val="18" />
                        </w:numPr>
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />                          
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00F1536F">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]>
                    <xsl:value-of select="Name"/>
                    <![CDATA[</w:t>
            </w:r>
          </w:p>
              ]]>
                  </Name>
                  <xsl:if test="Tip != ''">
                    <Tip>
                      <![CDATA[
                  <w:r>
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
              </w:rPr>
              <w:t>]]><xsl:value-of select="Tip"/><![CDATA[</w:t>
            </w:r>
            <w:r>
              <w:rPr>
                <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
              </w:rPr>
              <w:br />
            </w:r>]]>
                    </Tip>
                  </xsl:if>
                </Challenge>
              </xsl:if>
            </xsl:for-each>
          </ListOfChallenges>
        </PersonalStyle>
        <ShowJobSuggestions>
          <xsl:value-of select ="ShowSkills"/>
        </ShowJobSuggestions>
        <JobSuggestions>
          <ShowSummary>
            <xsl:value-of select="JobSuggestions/ShowSummary"/>
          </ShowSummary>
          <ShowBody>
            <xsl:value-of select="JobSuggestions/ShowBody"/>
          </ShowBody>
          <InterestBandName>
            <![CDATA[
            <w:r w:rsidR="001D2C0C">
              <w:t>]]>
            <xsl:value-of select="JobSuggestions/InterestBandName"/>
            <![CDATA[
              </w:t>
            </w:r>]]>
          </InterestBandName>
          <SkillsAndInterests>
            <InterestedJobFamilies>
              <xsl:for-each select="JobSuggestions/InterestedJobFamilies/InterestedJobFamily">
                <InterestedJobfamily>
                  <Title>
                    <![CDATA[
                      <w:r w:rsidRPr="004845E2" w:rsidR="001C1914">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                          <w:b />
                          <w:color w:val="1F497D" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                    </w:r>
                  ]]>
                  </Title>
                  <FamilyName>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                      </w:r>]]>
                  </FamilyName>
                  <Statement1>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement1" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement1>
                  <Statement2>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement2" /><![CDATA[</w:t>
                      </w:r>]]>
                  </Statement2>
                  <Statement3>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement3" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement3>                  
                  <ShowTask>
                    <xsl:choose>
                      <xsl:when test="Task != ''">
                        true
                      </xsl:when>
                      <xsl:otherwise>
                        false
                      </xsl:otherwise>
                    </xsl:choose>
                  </ShowTask>
                  <Task>
                    <![CDATA[
                       <w:r w:rsidRPr="001C1914" w:rsidR="001C1914">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Task" /><![CDATA[</w:t>
                    </w:r>]]>
                  </Task>
                </InterestedJobfamily>
              </xsl:for-each>
            </InterestedJobFamilies>
            <SkillsJobFamilies>
              <xsl:for-each select="JobSuggestions/SkillsJobFamilies/SkillsJobFamily">
                <SkillsJobFamily>
                  <Title>
                    <![CDATA[
                    <w:r w:rsidRPr="004845E2" w:rsidR="001C1914">
                      <w:rPr>
                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        <w:b />
                        <w:color w:val="1F497D" />
                      </w:rPr>
                      <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Title>
                  <FamilyName>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                      </w:r>]]>
                  </FamilyName>
                  <Statement1>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement1" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement1>
                  <Statement2>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement2" /><![CDATA[</w:t>
                      </w:r>]]>
                  </Statement2>
                  <Statement3>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement3" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement3>
                  <ShowBandMatchedText>
                    <xsl:choose>
                      <xsl:when test="BandMatched != ''">
                        true
                      </xsl:when>
                      <xsl:otherwise>
                        false
                      </xsl:otherwise>
                    </xsl:choose>
                  </ShowBandMatchedText>
                  <BandMatched>
                    <![CDATA[
                   <w:r w:rsidRPr="001C1914">
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    </w:rPr>
                    <w:t>]]><xsl:value-of select="BandMatched" /><![CDATA[</w:t>
                    </w:r>]]>
                  </BandMatched>
                  <ShowTask>
                    <xsl:choose>
                      <xsl:when test="Task != ''">
                        true
                      </xsl:when>
                      <xsl:otherwise>
                        false
                      </xsl:otherwise>
                    </xsl:choose>
                  </ShowTask>
                  <Task>
                    <![CDATA[
                     <w:p w:rsidRPr="001C1914" w:rsidR="001C1914" w:rsidP="001C1914" w:rsidRDefault="001C1914">
                      <w:pPr>
                        <w:autoSpaceDE w:val="0" />
                        <w:autoSpaceDN w:val="0" />
                        <w:adjustRightInd w:val="0" />
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="001C1914">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Task" /><![CDATA[</w:t>
                      </w:r>
                    </w:p>]]>
                  </Task>
                </SkillsJobFamily>
              </xsl:for-each>
            </SkillsJobFamilies>
          </SkillsAndInterests>
          <SkillsOnly>
            <SkillsJobFamilies>
              <xsl:for-each select="JobSuggestions/SkillsJobFamilies/SkillsJobFamily">
                <SkillsJobFamily>
                  <Title>
                    <![CDATA[
                    <w:r w:rsidRPr="004845E2" w:rsidR="001C1914">
                      <w:rPr>
                        <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        <w:b />
                        <w:color w:val="1F497D" />
                      </w:rPr>
                      <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Title>
                  <FamilyName>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Title" /><![CDATA[</w:t>
                      </w:r>]]>
                  </FamilyName>
                  <Statement1>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement1" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement1>
                  <Statement2>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement2" /><![CDATA[</w:t>
                      </w:r>]]>
                  </Statement2>
                  <Statement3>
                    <![CDATA[
                      <w:r w:rsidRPr="00A60ADB">
                        <w:rPr>
                          <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                        </w:rPr>
                        <w:t>]]><xsl:value-of select="Statement3" /><![CDATA[</w:t>
                  </w:r>]]>
                  </Statement3>
                  <ShowTask>
                    <xsl:choose>
                      <xsl:when test="Task != ''">
                        true
                      </xsl:when>
                      <xsl:otherwise>
                        false
                      </xsl:otherwise>
                    </xsl:choose>
                  </ShowTask>
                  <Task>
                    <![CDATA[
                   <w:r w:rsidRPr="001C1914" w:rsidR="001C1914">
                    <w:rPr>
                      <w:rFonts w:ascii="Verdana" w:hAnsi="Verdana" />
                    </w:rPr>
                    <w:t>]]><xsl:value-of select="Task" /><![CDATA[</w:t>
                    </w:r>
                    ]]>
                  </Task>
                </SkillsJobFamily>
              </xsl:for-each>
            </SkillsJobFamilies>
          </SkillsOnly>
        </JobSuggestions>
      </xsl:for-each>
    </InterpretedSkillsDocument>
  </xsl:template>
</xsl:stylesheet>
