DECLARE @Assessments TABLE (
    Id Int,
	Type NVARCHAR(MAX), 
	Title NVARCHAR(MAX),
	Subtitle NVARCHAR(MAX),
	Introduction NVARCHAR(MAX)
)

INSERT INTO @Assessments (Id, Type, Title, Subtitle, Introduction) VALUES
(10, 'Abstract', 'Solving abstract problems', 'Try some abstract problems', '<p>These skills are useful for jobs that involve creative thinking and logical decisions making, such as IT, scientific or medical jobs. Try this activity to see if you would enjoy doing this type of work.</p><div class="govuk-inset-text">The abstract assessment uses images. It is not suitable for users with screen readers. We are not able to supply alternative text without influencing your results.</div>'),
(7, 'Checking', 'Checking information activity', 'Try a clerical checking activity', '<p>You''ll be checking information in jobs like administration and clerical work, retail sales and customer service. Try this activity to help you think about how much you like this type of work and if you would enjoy doing it in your job.</p>'),
(2, 'Interests', 'Your interests', 'Explore areas of work you''re interested in', '<p>Complete this assessment to help you explore what work activities interest you the most. Knowing what you like to do in a job could help you make decisions about future career ideas. You''re more likely to be happy in a job where you find the work interesting.</p>'),
(8, 'Mechanical', 'Solving mechanical problems', 'Work through some mechanical problems', '<p>You''ll use mechanical skills in lots of jobs, such as engineering, manufacturing, construction and maintenance. Work through the mechanical problems to help you decide if this is something you would enjoy doing every day in your work.</p><div class="govuk-inset-text">The mechanical assessment uses images. It may not be suitable for users with screen readers. We are not able to supply detailed alternative text without influencing your results.</div>'),
(4, 'Motivation', 'Your motivation', 'Identify what you want from work', '<p>Understanding what''s important to you at work can help you explore jobs that could keep you happy and satisfied.</p><p>Think about how important each statement is to you.</p>'),
(5, 'Numerical', 'Working with numbers', 'Explore how you make judgements using numbers', '<p>Number skills are important for many job areas such as finance, management, and science and research. Think about how much you enjoy working with numbers and if you would want to do this in your work.</p>'),
(3, 'Personal', 'Your personal style', 'Understand how you prefer to work', '<p>Complete this assessment to learn more about what your personal style is. Understanding how you prefer to work will help you search for jobs that are suited to your style.</p>'),
(1, 'SkillAreas', 'Your skills', 'Identify activities you''re good at', '<p>This assessment is about how you see yourself and it can help you explore the skills you have. The answers you give will be used in your report to suggest job families you might be interested in.</p>'),
(9, 'Spatial', 'Working with shapes', 'Explore some spatial problems', '<p>Working with shapes is an important skill for jobs you would find in construction, arts and manufacturing. Try this activity to see if this is a skill you would enjoy using at work.</p><div class="govuk-inset-text">The spatial assessment uses images. It is not suitable for users with screen readers. We are not able to supply alternative text without influencing your results.</div>'),
(6, 'Verbal', 'Working with written information', 'Explore how you make judgements using written information.', '<p>Working with written information is important for many jobs like advertising, marketing, publishing, journalism and legal services. Think about how much you enjoy working with written information and whether you feel it''s something you would enjoy doing in your job.</p>')



MERGE Assessments AS target
USING (SELECT * FROM @Assessments) AS source
ON source.Id = target.Id
WHEN MATCHED THEN
	UPDATE SET
	target.Type = source.Type,
	target.Title = source.Title,
	target.Subtitle = source.Subtitle,
	target.Introduction = source.Introduction
WHEN NOT MATCHED THEN
	INSERT (Id, Type, Title, Subtitle, Introduction)
	VALUES (source.Id, source.Type, source.Title, source.Subtitle, source.Introduction)
WHEN NOT MATCHED BY SOURCE THEN DELETE;

;WITH QuestionsCte AS (
	SELECT * FROM (VALUES
	(1, 7, 1, '<h3 class="heading-medium">Compare the information in the tables and then answer below.</h3><p>You will need to refer to both tables to answer the question.</p>', '<table class="questionTbl1"> <caption>Information entry form</caption> <thead> <tr> <th class="refcolhead"></th> <th colspan="3"></th> <th colspan="2">Tick either</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>1 Week</th> <th>2 Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>IM6N</td> <td>11</td> <td>£1,180.75</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">2</td> <td>MA6L</td> <td>1</td> <td>£248.00</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">3</td> <td>WA6S</td> <td>1</td> <td>£155.99</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">4</td> <td>RO3M</td> <td>6</td> <td>£1,294.20</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">5</td> <td>IR5L</td> <td>2</td> <td>£310.25</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">6</td> <td>TU8N</td> <td>4</td> <td>£810.00</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">7</td> <td>FN1D</td> <td>20</td> <td>£6,800.55</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">8</td> <td>AU1T</td> <td>2</td> <td>£398.99</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">9</td> <td>SA3A</td> <td>10</td> <td>£1,635.90</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">10</td> <td>DM9K</td> <td>2</td> <td>£390.99</td> <td></td> <td>X</td> </tr> </tbody> </table><p>This information was then entered in to the record sheet below.</p><table class="questionTbl2"><caption>Record sheet</caption> <thead> <tr class="headerRow1"> <th class="refcolhead"></th> <th>A</th> <th>B</th> <th>C</th> <th>D</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>Number of Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>I6MN</td> <td>11</td> <td>£180.75</td> <td>One Week</td> </tr> <tr> <td class="refcol">2</td> <td>MA6L</td> <td>1</td> <td>£248.00</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">3</td> <td>WAS6</td> <td>2</td> <td>£255.99</td> <td>One Week</td> </tr> <tr> <td class="refcol">4</td> <td>ROM3</td> <td>6</td> <td>£1,294.20</td> <td>One Week</td> </tr> <tr> <td class="refcol">5</td> <td>IR5L</td> <td>2</td> <td>£310.25</td> <td>One Week</td> </tr> <tr> <td class="refcol">6</td> <td>T8UN</td> <td>4</td> <td>£810.00</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">7</td> <td>FN1D</td> <td>2</td> <td>£680.55</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">8</td> <td>AU1T</td> <td>2</td> <td>£389.99</td> <td>One Week</td> </tr> <tr> <td class="refcol">9</td> <td>3ASA</td> <td>1</td> <td>£1,365.90</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">10</td> <td>DM9K</td> <td>2</td> <td>£390.99</td> <td>Two Weeks</td> </tr> </tbody> </table>', NULL, NULL, NULL),
(2, 7, 2, '<h3 class="heading-medium">Compare the information in the tables and then answer below.</h3><p>You will need to refer to both tables to answer the question.</p>', '<table class="questionTbl1"> <caption>Information entry form</caption> <thead> <tr> <th class="refcolhead"></th> <th colspan="3"></th> <th colspan="2">Tick either</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>1 Week</th> <th>2 Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>EQ9R</td> <td>6</td> <td>£1,021.85</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">2</td> <td>CO3B</td> <td>22</td> <td>£3,659.99</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">3</td> <td>TU3I</td> <td>3</td> <td>£585.60</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">4</td> <td>TA1D</td> <td>12</td> <td>£3,610.80</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">5</td> <td>PA5N</td> <td>4</td> <td>£900.70</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">6</td> <td>BA5H</td> <td>3</td> <td>£480.85</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">7</td> <td>CR4S</td> <td>2</td> <td>£365.40</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">8</td> <td>PH8A</td> <td>9</td> <td>£1,260.50</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">9</td> <td>VI4S</td> <td>1</td> <td>£290.70</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">10</td> <td>SE8T</td> <td>12</td> <td>£3,040.80</td> <td>X</td> <td></td> </tr> </tbody> </table><p>This information was then entered in to the record sheet below.</p><table class="questionTbl2"> <caption>Record sheet</caption> <thead> <tr class="headerRow1"> <th class="refcolhead"></th> <th>A</th> <th>B</th> <th>C</th> <th>D</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>Number of Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>EQ9R</td> <td>4</td> <td>£1,021.85</td> <td>One Week</td> </tr> <tr> <td class="refcol">2</td> <td>CO3B</td> <td>22</td> <td>£3,659.99</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">3</td> <td>TU3I</td> <td>2</td> <td>£585.60</td> <td>One Week</td> </tr> <tr> <td class="refcol">4</td> <td>TA1D</td> <td>2</td> <td>£3,610.80</td> <td>One Week</td> </tr> <tr> <td class="refcol">5</td> <td>PA5N</td> <td>4</td> <td>£900.70</td> <td>One Week</td> </tr> <tr> <td class="refcol">6</td> <td>3A5H</td> <td>3</td> <td>£480.85</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">7</td> <td>C4RS</td> <td>2</td> <td>£365.40</td> <td>One Week</td> </tr> <tr> <td class="refcol">8</td> <td>PH8A</td> <td>9</td> <td>£1,260.50</td> <td>One Week</td> </tr> <tr> <td class="refcol">9</td> <td>VIS4</td> <td>1</td> <td>£290.70</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">10</td> <td>SE8T</td> <td>2</td> <td>£3,040.80</td> <td>One Week</td> </tr> </tbody> </table>', NULL, NULL, NULL),
(3, 7, 3, '<h3 class="heading-medium">Compare the information in the tables and then answer below.</h3><p>You will need to refer to both tables to answer the question.</p>', '<table class="questionTbl1"> <caption>Information entry form</caption> <thead> <tr> <th class="refcolhead"></th> <th colspan="3"></th> <th colspan="2">Tick either</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>1 Week</th> <th>2 Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>GR7A</td> <td>1</td> <td>£290.99</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">2</td> <td>SW2D</td> <td>1</td> <td>£302.90</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">3</td> <td>IN9S</td> <td>8</td> <td>£1,720.70</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">4</td> <td>PO4L</td> <td>3</td> <td>£357.25</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">5</td> <td>NO3W</td> <td>12</td> <td>£2,460.20</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">6</td> <td>NZ1D</td> <td>20</td> <td>£9,999.00</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">7</td> <td>YU4G</td> <td>12</td> <td>£1,756.50</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">8</td> <td>GE3R</td> <td>3</td> <td>£520.00</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">9</td> <td>FR1C</td> <td>2</td> <td>£520.00</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">10</td> <td>MO6Z</td> <td>14</td> <td>£2,954.45</td> <td></td> <td>X</td> </tr> </tbody> </table><p>This information was then entered in to the record sheet below.</p><table class="questionTbl2"> <caption>Record sheet</caption> <thead> <tr class="headerRow1"> <th class="refcolhead"></th> <th>A</th> <th>B</th> <th>C</th> <th>D</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>Number of Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>GRA7</td> <td>1</td> <td>£299.99</td> <td>One Week</td> </tr> <tr> <td class="refcol">2</td> <td>SW2D</td> <td>1</td> <td>£320.90</td> <td>One Week</td> </tr> <tr> <td class="refcol">3</td> <td>IN9S</td> <td>8</td> <td>£1,720.70</td> <td>One Week</td> </tr> <tr> <td class="refcol">4</td> <td>PO4L</td> <td>3</td> <td>£352.75</td> <td>One Week</td> </tr> <tr> <td class="refcol">5</td> <td>NO3W</td> <td>12</td> <td>£2,460.20</td> <td>One Week</td> </tr> <tr> <td class="refcol">6</td> <td>NZ1D</td> <td>2</td> <td>£999.00</td> <td>One Week</td> </tr> <tr> <td class="refcol">7</td> <td>YU4G</td> <td>12</td> <td>£1,756.50</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">8</td> <td>G3ER</td> <td>3</td> <td>£520.00</td> <td>One Week</td> </tr> <tr> <td class="refcol">9</td> <td>FR1C</td> <td>3</td> <td>£520.00</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">10</td> <td>MO6Z</td> <td>14</td> <td>£2,954.45</td> <td>Two Weeks</td> </tr> </tbody> </table>', NULL, NULL, NULL),
(4, 7, 4, '<h3 class="heading-medium">Compare the information in the tables and then answer below.</h3><p>You will need to refer to both tables to answer the question.</p>', '<table class="questionTbl1"> <caption>Information entry form</caption> <thead> <tr> <th class="refcolhead"></th> <th colspan="3"></th> <th colspan="2">Tick either</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>1 Week</th> <th>2 Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>AL9U</td> <td>1</td> <td>£256.49</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">2</td> <td>LU5B</td> <td>4</td> <td>£964.20</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">3</td> <td>PO3D</td> <td>16</td> <td>£3,072.84</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">4</td> <td>HUTR</td> <td>10</td> <td>£3,020.18</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">5</td> <td>TA8W</td> <td>2</td> <td>£420.12</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">6</td> <td>AR3A</td> <td>2</td> <td>£403.55</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">7</td> <td>BR8Z</td> <td>13</td> <td>£2,520.90</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">8</td> <td>ME1O</td> <td>1</td> <td>£221.40</td> <td></td> <td>X</td> </tr> <tr> <td class="refcol">9</td> <td>EG8D</td> <td>20</td> <td>£3,909.40</td> <td>X</td> <td></td> </tr> <tr> <td class="refcol">10</td> <td>PE6T</td> <td>2</td> <td>£390.20</td> <td>X</td> <td></td> </tr> </tbody> </table><p>This information was then entered in to the record sheet below.</p><table class="questionTbl2"> <caption>Record sheet</caption> <thead> <tr class="headerRow1"> <th class="refcolhead"></th> <th>A</th> <th>B</th> <th>C</th> <th>D</th> </tr> <tr> <th class="refcolhead"></th> <th>Reference</th> <th>Number of Travellers</th> <th>Total Price</th> <th>Number of Weeks</th> </tr> </thead> <tbody> <tr> <td class="refcol">1</td> <td>AL9U</td> <td>1</td> <td>£256.49</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">2</td> <td>LU53</td> <td>4</td> <td>£964.20</td> <td>One Week</td> </tr> <tr> <td class="refcol">3</td> <td>PO3D</td> <td>16</td> <td>£3,072.84</td> <td>One Week</td> </tr> <tr> <td class="refcol">4</td> <td>HU7R</td> <td>10</td> <td>£302.18</td> <td>One Week</td> </tr> <tr> <td class="refcol">5</td> <td>TA8W</td> <td>2</td> <td>£420.12</td> <td>One Week</td> </tr> <tr> <td class="refcol">6</td> <td>AR3A</td> <td>2</td> <td>£430.55</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">7</td> <td>BR8Z</td> <td>3</td> <td>£2,520.90</td> <td>One Week</td> </tr> <tr> <td class="refcol">8</td> <td>M1EO</td> <td>1</td> <td>£212.40</td> <td>One Week</td> </tr> <tr> <td class="refcol">9</td> <td>EG8D</td> <td>20</td> <td>£390.40</td> <td>Two Weeks</td> </tr> <tr> <td class="refcol">10</td> <td>PE6T</td> <td>2</td> <td>£390.20</td> <td>One Week</td> </tr> </tbody> </table>', NULL, NULL, NULL),
(5, 2, 1, 'How interested are you in this work activity?', 'Helping people by providing treatments or therapies', NULL, NULL, NULL),
(6, 2, 2, 'How interested are you in this work activity?', 'Selling products or services to people', NULL, NULL, NULL),
(7, 2, 3, 'How interested are you in this work activity?', 'Grouping old documents together into files', NULL, NULL, NULL),
(8, 2, 4, 'How interested are you in this work activity?', 'Making improvements to a house', NULL, NULL, NULL),
(9, 2, 5, 'How interested are you in this work activity?', 'Organising things in a warehouse', NULL, NULL, NULL),
(10, 2, 6, 'How interested are you in this work activity?', 'Checking information about money for mistakes', NULL, NULL, NULL),
(11, 2, 7, 'How interested are you in this work activity?', 'Predicting earthquakes', NULL, NULL, NULL),
(12, 2, 8, 'How interested are you in this work activity?', 'Designing products for use in people''s homes', NULL, NULL, NULL),
(13, 2, 9, 'How interested are you in this work activity?', 'Doing administrative tasks for a solicitor or lawyer', NULL, NULL, NULL),
(14, 2, 10, 'How interested are you in this work activity?', 'Working on a film or TV set', NULL, NULL, NULL),
(15, 2, 11, 'How interested are you in this work activity?', 'Checking legal documents for errors', NULL, NULL, NULL),
(16, 2, 12, 'How interested are you in this work activity?', 'Working with music or sound', NULL, NULL, NULL),
(17, 2, 13, 'How interested are you in this work activity?', 'Helping people fill in application forms correctly', NULL, NULL, NULL),
(18, 2, 14, 'How interested are you in this work activity?', 'Taking photographs', NULL, NULL, NULL),
(19, 2, 15, 'How interested are you in this work activity?', 'Making detailed plans for activities', NULL, NULL, NULL),
(20, 2, 16, 'How interested are you in this work activity?', 'Creating computer games', NULL, NULL, NULL),
(21, 2, 17, 'How interested are you in this work activity?', 'Checking that plans are on track', NULL, NULL, NULL),
(176, 8, 1, 'Which is the stronger arch?', NULL, 'this is a test', 'Image A is of a round arch and image B is a flat arch.', 'M01.gif'),
(177, 8, 2, 'Which pair of magnets will attract each other?', NULL, NULL, 'Images of two U-shaped magnets facing one another.', 'M02.gif'),
(178, 8, 3, 'Which trolley will turn in the smallest circle?', NULL, NULL, 'Images of three trolleys with fixed back wheels and pivoting front wheels.', 'M03.gif'),
(179, 8, 4, 'Which chain is likely to break first when the lever is pulled as shown?', NULL, NULL, 'Image of a lever held in position by two chains.', 'M04.gif'),
(180, 8, 5, 'How would the iron bar hang when suspended in a rope sling?', NULL, NULL, 'Image of an iron bar suspended in a rope sling.', 'M05.gif'),
(181, 8, 6, 'Which is the hottest part of the oven?', NULL, NULL, 'Image of an oven with a top and middle shelf.', 'M06.gif'),
(182, 8, 7, 'Which screw is least likely to pull out of the wall?', NULL, NULL, 'Image of two shelves each attached with two screws in a different configuration.', 'M07.gif'),
(183, 8, 8, 'Which liquid is the densest?', NULL, NULL, 'Image of three beakers each holding a cork bobbing at a different height in liquid.', 'M08.gif'),
(184, 8, 9, 'Which mirror will reflect light rays as shown?', NULL, NULL, 'Image of three mirrors each curved differently. The mirrors are all reflecting the same parallel light rays.', 'M09.gif'),
(185, 8, 10, 'Which apparatus requires least effort to lift the weight?', NULL, NULL, 'Image of three diagrams showing 100kg weights attached to different lifting apparatus.', 'M10.gif'),
(186, 8, 11, 'Which arrangement would support the heavier load?', NULL, NULL, 'Image of two structures made from 3 blocks each holding 100kg weights.', 'M11.gif'),
(187, 4, 1, NULL, 'How important is having activities where I can work with other people?', NULL, NULL, NULL),
(188, 4, 2, NULL, 'How important is it that research skills are valued?', NULL, NULL, NULL),
(189, 4, 3, NULL, 'How important is work where you can be creative?', NULL, NULL, NULL),
(190, 4, 4, NULL, 'How important is it that there is an organised way of doing things?', NULL, NULL, NULL),
(191, 4, 5, NULL, 'How important is being given money as a reward for doing well?', NULL, NULL, NULL),
(192, 4, 6, NULL, 'How important is it that the focus is on getting things done?', NULL, NULL, NULL),
(193, 4, 7, NULL, 'How important is working in a busy environment?', NULL, NULL, NULL),
(194, 4, 8, NULL, 'How important is it that the focus is on giving a good service to others?', NULL, NULL, NULL),
(195, 4, 9, NULL, 'How important is it that activities are focused on one specialist area?', NULL, NULL, NULL),
(206, 3, 1, 'Think about how often you do the following things.', 'Changing what other people think', NULL, NULL, NULL),
(207, 3, 2, 'Think about how often you do the following things.', 'Working with numbers', NULL, NULL, NULL),
(208, 3, 3, 'Think about how often you do the following things.', 'Thinking about why people act the way they do', NULL, NULL, NULL),
(209, 3, 4, 'Think about how often you do the following things.', 'Feeling worried', NULL, NULL, NULL),
(210, 3, 5, 'Think about how often you do the following things.', 'Trying to understand why people act the way they do', NULL, NULL, NULL),
(211, 3, 6, 'Think about how often you do the following things.', 'Using facts to understand something', NULL, NULL, NULL),
(212, 3, 7, 'Think about how often you do the following things.', 'Planning what you are going to do', NULL, NULL, NULL),
(213, 3, 8, 'Think about how often you do the following things.', 'Enjoying hands-on work', NULL, NULL, NULL),
(214, 3, 9, 'Think about how often you do the following things.', 'Finding out what other people think', NULL, NULL, NULL),
(215, 3, 10, 'Think about how often you do the following things.', 'Fixing things when they break', NULL, NULL, NULL),
(216, 3, 11, 'Think about how often you do the following things.', 'Being on time for things', NULL, NULL, NULL),
(217, 1, 1, '&nbsp;', NULL, NULL, NULL, NULL),
(218, 1, 2, '&nbsp;', NULL, NULL, NULL, NULL),
(219, 1, 3, '&nbsp;', NULL, NULL, NULL, NULL),
(220, 1, 4, '&nbsp;', NULL, NULL, NULL, NULL),
(221, 1, 5, '&nbsp;', NULL, NULL, NULL, NULL),
(222, 1, 6, '&nbsp;', NULL, NULL, NULL, NULL),
(223, 1, 7, '&nbsp;', NULL, NULL, NULL, NULL),
(224, 1, 8, '&nbsp;', NULL, NULL, NULL, NULL),
(225, 1, 9, '&nbsp;', NULL, NULL, NULL, NULL),
(226, 1, 10, '&nbsp;', NULL, NULL, NULL, NULL),
(227, 1, 11, '&nbsp;', NULL, NULL, NULL, NULL),
(228, 1, 12, '&nbsp;', NULL, NULL, NULL, NULL),
(229, 1, 13, '&nbsp;', NULL, NULL, NULL, NULL),
(230, 1, 14, '&nbsp;', NULL, NULL, NULL, NULL),
(231, 1, 15, '&nbsp;', NULL, NULL, NULL, NULL),
(232, 1, 16, '&nbsp;', NULL, NULL, NULL, NULL),
(233, 1, 17, '&nbsp;', NULL, NULL, NULL, NULL),
(234, 1, 18, '&nbsp;', NULL, NULL, NULL, NULL),
(235, 1, 19, '&nbsp;', NULL, NULL, NULL, NULL),
(236, 1, 20, '&nbsp;', NULL, NULL, NULL, NULL),
(237, 1, 21, '&nbsp;', NULL, NULL, NULL, NULL),
(238, 1, 22, '&nbsp;', NULL, NULL, NULL, NULL),
(239, 1, 23, '&nbsp;', NULL, NULL, NULL, NULL),
(240, 1, 24, '&nbsp;', NULL, NULL, NULL, NULL),
(241, 1, 25, '&nbsp;', NULL, NULL, NULL, NULL),
(242, 1, 26, '&nbsp;', NULL, NULL, NULL, NULL),
(243, 1, 27, '&nbsp;', NULL, NULL, NULL, NULL),
(244, 1, 28, '&nbsp;', NULL, NULL, NULL, NULL),
(245, 9, 1, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S01-Shape.gif'),
(246, 9, 2, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S02-Shape.gif'),
(247, 9, 3, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S03-Shape.gif'),
(248, 9, 4, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S04-Shape.gif'),
(249, 9, 5, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S05-Shape.gif'),
(250, 9, 6, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S06-Shape.gif'),
(251, 9, 7, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S07-Shape.gif'),
(252, 9, 8, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S08-Shape.gif'),
(253, 9, 9, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S09-Shape.gif'),
(254, 9, 10, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S10-Shape.gif'),
(255, 9, 11, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S11-Shape.gif'),
(256, 9, 12, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S12-Shape.gif'),
(257, 9, 13, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S13-Shape.gif'),
(258, 9, 14, 'Which shape is the same as the one below?', NULL, NULL, NULL, 'S14-Shape.gif'),
(259, 6, 1, 'Providers of sporting information face a range of initial decisions to be made.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>There are many options which the provider has to consider when giving sports information to the public. Firstly, the nature of the information is of primary importance; the provider needs to decide in which medium they will deliver their information, the range of sports that they will cover, and whether they will deliver current updates or report retrospectively. Secondly, the provider will also need to consider to whom they are marketing their information; an international approach has its benefits, but there is still an audience for local  information. Finally, the provider has to assess how they will guarantee the quality of their service; without a reputation for speed and accuracy, the provider will find it difficult to gain the support they need from the sporting associations and committees.</p></div>', NULL, NULL, NULL),
(260, 6, 2, 'The nature of the information they provide is the provider’s most important consideration.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>There are many options which the provider has to consider when giving sports information to the public. Firstly, the nature of the information is of primary importance; the provider needs to decide in which medium they will deliver their information, the range of sports that they will cover, and whether they will deliver current updates or report retrospectively. Secondly, the provider will also need to consider to whom they are marketing their information; an international approach has its benefits, but there is still an audience for local  information. Finally, the provider has to assess how they will guarantee the quality of their service; without a reputation for speed and accuracy, the provider will find it difficult to gain the support they need from the sporting associations and committees.</p></div>', NULL, NULL, NULL),
(261, 6, 3, 'The only consideration of the sports provider is the nature of the information to be provided.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>There are many options which the provider has to consider when giving sports information to the public. Firstly, the nature of the information is of primary importance; the provider needs to decide in which medium they will deliver their information, the range of sports that they will cover, and whether they will deliver current updates or report retrospectively. Secondly, the provider will also need to consider to whom they are marketing their information; an international approach has its benefits, but there is still an audience for local  information. Finally, the provider has to assess how they will guarantee the quality of their service; without a reputation for speed and accuracy, the provider will find it difficult to gain the support they need from the sporting associations and committees.</p></div>', NULL, NULL, NULL),
(262, 6, 4, 'Wi-Fi technology can benefit businesses.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Computer users can connect to the Internet without any wires using Wi-Fi technology. This has an obvious benefit because it gets rid of some of the unsightly wires which surround computers. However, Wi-Fi technology also helps businesses as it allows employees to work more flexibly, while removing the need for businesses to install expensive cables - particularly useful if their office is a rented building. As a result of the popularity of Wi-Fi, laws have had to be reviewed, and there have been convictions where people have been using someone else’s Wi-Fi without paying for the service. These reviews of the law are crucial to make sure that Wi-Fi is a secure form of technology, as only then will businesses give it their support.</p></div>', NULL, NULL, NULL),
(263, 6, 5, 'Wi-Fi is a new form of technology.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Computer users can connect to the Internet without any wires using Wi-Fi technology. This has an obvious benefit because it gets rid of some of the unsightly wires which surround computers. However, Wi-Fi technology also helps businesses as it allows employees to work more flexibly, while removing the need for businesses to install expensive cables - particularly useful if their office is a rented building. As a result of the popularity of Wi-Fi, laws have had to be reviewed, and there have been convictions where people have been using someone else’s Wi-Fi without paying for the service. These reviews of the law are crucial to make sure that Wi-Fi is a secure form of technology, as only then will businesses give it their support.</p></div>', NULL, NULL, NULL),
(264, 6, 6, 'A consequence of hiding money rather than investing it is a nationwide loss of many millions in interest each year.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Economic research has identified a trend exhibited by one in six Britons of hiding cash in their homes instead of investing it. This is termed the ‘Biscuit Tin Economy’. It is estimated that £3.5 billion is currently hidden, sometimes in obscure places, such as under mattresses or in fridges, in homes across the country. Reasons for this are varied, for example 6% are concealing it from their partners, and 4% believe their money to be safer at home than in a bank. Researchers maintain that these actions demonstrate economic folly, and that, as a result, Britons are sacrificing up to £174 million in interest every year. This ‘Biscuit Tin Economy’ is betraying those who trust in it, as it renders their hidden money both unproductive and potentially unsafe, whereas it could be profitably invested in a stocks or high-interest savings plan, for example.</p></div>', NULL, NULL, NULL),
(265, 6, 7, 'The majority of people who secrete cash on their property do so because they do not trust the bank.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Economic research has identified a trend exhibited by one in six Britons of hiding cash in their homes instead of investing it. This is termed the ‘Biscuit Tin Economy’. It is estimated that £3.5 billion is currently hidden, sometimes in obscure places, such as under mattresses or in fridges, in homes across the country. Reasons for this are varied, for example 6% are concealing it from their partners, and 4% believe their money to be safer at home than in a bank. Researchers maintain that these actions demonstrate economic folly, and that, as a result, Britons are sacrificing up to £174 million in interest every year. This ‘Biscuit Tin Economy’ is betraying those who trust in it, as it renders their hidden money both unproductive and potentially unsafe, whereas it could be profitably invested in a stocks or high-interest savings plan, for example.</p></div>', NULL, NULL, NULL),
(266, 6, 8, 'Only people in rural Britain store their cash in unexpected places.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Economic research has identified a trend exhibited by one in six Britons of hiding cash in their homes instead of investing it. This is termed the ‘Biscuit Tin Economy’. It is estimated that £3.5 billion is currently hidden, sometimes in obscure places, such as under mattresses or in fridges, in homes across the country. Reasons for this are varied, for example 6% are concealing it from their partners, and 4% believe their money to be safer at home than in a bank. Researchers maintain that these actions demonstrate economic folly, and that, as a result, Britons are sacrificing up to £174 million in interest every year. This ‘Biscuit Tin Economy’ is betraying those who trust in it, as it renders their hidden money both unproductive and potentially unsafe, whereas it could be profitably invested in a stocks or high-interest savings plan, for example.</p></div>', NULL, NULL, NULL),
(267, 6, 9, 'Campaigns for road safety have eliminated fatalities caused by car crashes.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Despite vehicle improvements and campaigns for road safety, many injuries and fatalities are still caused by collisions and other incidents involving vehicles. According to investigations in the United States, some of these accidents could be prevented through the development of a mobile internet network. All of the cars on a stretch of road would be linked to each other, making up the mobile network. Only one of these vehicles would need to be connected to the internet to download travel news to the mobile network. The studies highlight the safety advantages of such a system, which would enable drivers to find out about accidents and potential dangers as they happen and in relation to their particular location. Drivers and emergency service teams would have detailed information about problematic areas. There are, however, possible drawbacks to the development of such networks, especially that the data within it could break privacy laws.</p></div>', NULL, NULL, NULL),
(268, 6, 10, 'All those in the mobile network must be connected to the internet.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Despite vehicle improvements and campaigns for road safety, many injuries and fatalities are still caused by collisions and other incidents involving vehicles. According to investigations in the United States, some of these accidents could be prevented through the development of a mobile internet network. All of the cars on a stretch of road would be linked to each other, making up the mobile network. Only one of these vehicles would need to be connected to the internet to download travel news to the mobile network. The studies highlight the safety advantages of such a system, which would enable drivers to find out about accidents and potential dangers as they happen and in relation to their particular location. Drivers and emergency service teams would have detailed information about problematic areas. There are, however, possible drawbacks to the development of such networks, especially that the data within it could break privacy laws.</p></div>', NULL, NULL, NULL),
(269, 6, 11, 'Mobile networks are the best way of decreasing the road accident rate.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Despite vehicle improvements and campaigns for road safety, many injuries and fatalities are still caused by collisions and other incidents involving vehicles. According to investigations in the United States, some of these accidents could be prevented through the development of a mobile internet network. All of the cars on a stretch of road would be linked to each other, making up the mobile network. Only one of these vehicles would need to be connected to the internet to download travel news to the mobile network. The studies highlight the safety advantages of such a system, which would enable drivers to find out about accidents and potential dangers as they happen and in relation to their particular location. Drivers and emergency service teams would have detailed information about problematic areas. There are, however, possible drawbacks to the development of such networks, especially that the data within it could break privacy laws.</p></div>', NULL, NULL, NULL),
(270, 6, 12, 'Comparing foods consumed in different countries is useless for understanding the health advantages of particular foods.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Research into the diets of other countries and cultures can be used to point out the health benefits of certain foods. One example of this is the Japanese diet, which is rich in omega-3 fatty acids, compared to a Western diet, in which omega-6 fatty acids are more common. A recent study has discovered that diets with high levels of omega-3 fatty acids, found particularly in fish oils, may prevent some types of blindness. Tests - performed only on mice so far - revealed that omega-3 fatty acids protect against retinopathy, which leads to loss of vision. Further tests are scheduled to begin soon to ascertain the potential benefits for humans.</p></div>', NULL, NULL, NULL),
(271, 6, 13, 'Mice often get retinopathy.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Research into the diets of other countries and cultures can be used to point out the health benefits of certain foods. One example of this is the Japanese diet, which is rich in omega-3 fatty acids, compared to a Western diet, in which omega-6 fatty acids are more common. A recent study has discovered that diets with high levels of omega-3 fatty acids, found particularly in fish oils, may prevent some types of blindness. Tests - performed only on mice so far - revealed that omega-3 fatty acids protect against retinopathy, which leads to loss of vision. Further tests are scheduled to begin soon to ascertain the potential benefits for humans.</p></div>', NULL, NULL, NULL),
(272, 6, 14, 'The Western diet incorporates very few omega-3 fatty acids.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>Research into the diets of other countries and cultures can be used to point out the health benefits of certain foods. One example of this is the Japanese diet, which is rich in omega-3 fatty acids, compared to a Western diet, in which omega-6 fatty acids are more common. A recent study has discovered that diets with high levels of omega-3 fatty acids, found particularly in fish oils, may prevent some types of blindness. Tests - performed only on mice so far - revealed that omega-3 fatty acids protect against retinopathy, which leads to loss of vision. Further tests are scheduled to begin soon to ascertain the potential benefits for humans.</p></div>', NULL, NULL, NULL),
(273, 6, 15, 'Brand, speed and machine specification all contribute to the cost of the computer.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A number of things should be taken into consideration when looking to buy a new computer. One of the major things to bear in mind is what its main uses will be. For instance, if it is to be used for graphic design or complex data analysis, one would look to invest in a computer that has been recommended as the most efficient choice to satisfy these needs. The cost of a suitable computer can vary depending on the specification required, the brand and the speed. The performance of the system is dependent on major components such as the processor, memory and hard disk space. It therefore becomes a case of striking a balance between  buying a computer that will meet the user’s requirements, while also not over-spending in areas where the extra investment may be wasted.</p></div>', NULL, NULL, NULL),
(274, 6, 16, 'The intended use of a computer will influence the choices made when selecting a new computer to purchase.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A number of things should be taken into consideration when looking to buy a new computer. One of the major things to bear in mind is what its main uses will be. For instance, if it is to be used for graphic design or complex data analysis, one would look to invest in a computer that has been recommended as the most efficient choice to satisfy these needs. The cost of a suitable computer can vary depending on the specification required, the brand and the speed. The performance of the system is dependent on major components such as the processor, memory and hard disk space. It therefore becomes a case of striking a balance between  buying a computer that will meet the user’s requirements, while also not over-spending in areas where the extra investment may be wasted.</p></div>', NULL, NULL, NULL),
(275, 6, 17, 'Buying a home computer involves juggling between needs and costs.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A number of things should be taken into consideration when looking to buy a new computer. One of the major things to bear in mind is what its main uses will be. For instance, if it is to be used for graphic design or complex data analysis, one would look to invest in a computer that has been recommended as the most efficient choice to satisfy these needs. The cost of a suitable computer can vary depending on the specification required, the brand and the speed. The performance of the system is dependent on major components such as the processor, memory and hard disk space. It therefore becomes a case of striking a balance between  buying a computer that will meet the user’s requirements, while also not over-spending in areas where the extra investment may be wasted.</p></div>', NULL, NULL, NULL),
(276, 6, 18, 'Skin grafts are generally taken from concealed skin.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A newly-developed way of creating artificial skin might be useful for treating burns and wounds.  Treating these injuries is normally done with a skin graft taken from another, usually unseen, part of the body. However, artificial skin, grown in the lab from cells called fibroblasts, has so far shown itself to have better healing properties than ‘living skin’ and is less likely to scar. The process is currently being refined by researchers, who believe that the wide availability of artificial skin could completely transform the way burns and other skin damage are treated.</p></div>', NULL, NULL, NULL),
(277, 6, 19, 'Lack of scarring is the most important factor when choosing a method of treating burns.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A newly-developed way of creating artificial skin might be useful for treating burns and wounds.  Treating these injuries is normally done with a skin graft taken from another, usually unseen, part of the body. However, artificial skin, grown in the lab from cells called fibroblasts, has so far shown itself to have better healing properties than ‘living skin’ and is less likely to scar. The process is currently being refined by researchers, who believe that the wide availability of artificial skin could completely transform the way burns and other skin damage are treated.</p></div>', NULL, NULL, NULL),
(278, 6, 20, 'Skin grafts always leave scars.', '<h3 class="heading-medium">Read the information and answer the question below</h3><div class="panel panel-border-wide"><p>A newly-developed way of creating artificial skin might be useful for treating burns and wounds.  Treating these injuries is normally done with a skin graft taken from another, usually unseen, part of the body. However, artificial skin, grown in the lab from cells called fibroblasts, has so far shown itself to have better healing properties than ‘living skin’ and is less likely to scar. The process is currently being refined by researchers, who believe that the wide availability of artificial skin could completely transform the way burns and other skin damage are treated.</p></div>', NULL, NULL, NULL),
(279, 10, 1, 'Choose the image that you think should come next in this series.', '<images><image src="AB01-Shape1.gif" title="" alttext=""/><image src="AB01-Shape2.gif" title="" alttext=""/><image src="AB01-Shape3.gif" title="" alttext=""/><image src="AB01-Shape4.gif" title="" alttext=""/><image src="AB01-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(280, 10, 2, 'Choose the image that you think should come next in this series.', '<images><image src="AB02-Shape1.gif" title="" alttext=""/><image src="AB02-Shape2.gif" title="" alttext=""/><image src="AB02-Shape3.gif" title="" alttext=""/><image src="AB02-Shape4.gif" title="" alttext=""/><image src="AB02-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(281, 10, 3, 'Choose the image that you think should come next in this series.', '<images><image src="AB03-Shape1.gif" title="" alttext=""/><image src="AB03-Shape2.gif" title="" alttext=""/><image src="AB03-Shape3.gif" title="" alttext=""/><image src="AB03-Shape4.gif" title="" alttext=""/><image src="AB03-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(282, 10, 4, 'Choose the image that you think should come next in this series.', '<images><image src="AB04-Shape1.gif" title="" alttext=""/><image src="AB04-Shape2.gif" title="" alttext=""/><image src="AB04-Shape3.gif" title="" alttext=""/><image src="AB04-Shape4.gif" title="" alttext=""/><image src="AB04-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(283, 10, 5, 'Choose the image that you think should come next in this series.', '<images><image src="AB05-Shape1.gif" title="" alttext=""/><image src="AB05-Shape2.gif" title="" alttext=""/><image src="AB05-Shape3.gif" title="" alttext=""/><image src="AB05-Shape4.gif" title="" alttext=""/><image src="AB05-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(284, 10, 6, 'Choose the image that you think should come next in this series.', '<images><image src="AB06-Shape1.gif" title="" alttext=""/><image src="AB06-Shape2.gif" title="" alttext=""/><image src="AB06-Shape3.gif" title="" alttext=""/><image src="AB06-Shape4.gif" title="" alttext=""/><image src="AB06-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(285, 10, 7, 'Choose the image that you think should come next in this series.', '<images><image src="AB07-Shape1.gif" title="" alttext=""/><image src="AB07-Shape2.gif" title="" alttext=""/><image src="AB07-Shape3.gif" title="" alttext=""/><image src="AB07-Shape4.gif" title="" alttext=""/><image src="AB07-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(286, 10, 8, 'Choose the image that you think should come next in this series.', '<images><image src="AB08-Shape1.gif" title="" alttext=""/><image src="AB08-Shape2.gif" title="" alttext=""/><image src="AB08-Shape3.gif" title="" alttext=""/><image src="AB08-Shape4.gif" title="" alttext=""/><image src="AB08-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(287, 10, 9, 'Choose the image that you think should come next in this series.', '<images><image src="AB09-Shape1.gif" title="" alttext=""/><image src="AB09-Shape2.gif" title="" alttext=""/><image src="AB09-Shape3.gif" title="" alttext=""/><image src="AB09-Shape4.gif" title="" alttext=""/><image src="AB09-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(288, 10, 10, 'Choose the image that you think should come next in this series.', '<images><image src="AB10-Shape1.gif" title="" alttext=""/><image src="AB10-Shape2.gif" title="" alttext=""/><image src="AB10-Shape3.gif" title="" alttext=""/><image src="AB10-Shape4.gif" title="" alttext=""/><image src="AB10-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(289, 10, 11, 'Choose the image that you think should come next in this series.', '<images><image src="AB11-Shape1.gif" title="" alttext=""/><image src="AB11-Shape2.gif" title="" alttext=""/><image src="AB11-Shape3.gif" title="" alttext=""/><image src="AB11-Shape4.gif" title="" alttext=""/><image src="AB11-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(290, 10, 12, 'Choose the image that you think should come next in this series.', '<images><image src="AB12-Shape1.gif" title="" alttext=""/><image src="AB12-Shape2.gif" title="" alttext=""/><image src="AB12-Shape3.gif" title="" alttext=""/><image src="AB12-Shape4.gif" title="" alttext=""/><image src="AB12-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(291, 10, 13, 'Choose the image that you think should come next in this series.', '<images><image src="AB13-Shape1.gif" title="" alttext=""/><image src="AB13-Shape2.gif" title="" alttext=""/><image src="AB13-Shape3.gif" title="" alttext=""/><image src="AB13-Shape4.gif" title="" alttext=""/><image src="AB13-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(292, 10, 14, 'Choose the image that you think should come next in this series.', '<images><image src="AB14-Shape1.gif" title="" alttext=""/><image src="AB14-Shape2.gif" title="" alttext=""/><image src="AB14-Shape3.gif" title="" alttext=""/><image src="AB14-Shape4.gif" title="" alttext=""/><image src="AB14-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(293, 10, 15, 'Choose the image that you think should come next in this series.', '<images><image src="AB15-Shape1.gif" title="" alttext=""/><image src="AB15-Shape2.gif" title="" alttext=""/><image src="AB15-Shape3.gif" title="" alttext=""/><image src="AB15-Shape4.gif" title="" alttext=""/><image src="AB15-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(294, 10, 16, 'Choose the image that you think should come next in this series.', '<images><image src="AB16-Shape1.gif" title="" alttext=""/><image src="AB16-Shape2.gif" title="" alttext=""/><image src="AB16-Shape3.gif" title="" alttext=""/><image src="AB16-Shape4.gif" title="" alttext=""/><image src="AB16-Shape5.gif" title="" alttext=""/><image src="shcQuestionMark.png" title="" alttext=""/></images>', NULL, NULL, NULL),
(295, 5, 1, 'How many calls would there be if there was a 5% decrease from May?', '<p class="passageHdr">Table showing details of calls to TV show "Talented America" each month from January to May, in terms of total numbers and money made.</p>
<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
              "Talented America" Phone Line Information
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row" aria-hidden="true">
              <th scope="col"></th>
			  <th colspan="5" role="columnheader" scope="row">Month</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Phone Line Data</th>
              <th role="columnheader" scope="col" class="govuk-table__header">January</th>
              <th role="columnheader" scope="col" class="govuk-table__header">February</th>
              <th role="columnheader" scope="col" class="govuk-table__header">March</th>
              <th role="columnheader" scope="col" class="govuk-table__header" >April</th>
              <th role="columnheader" scope="col" class="govuk-table__header">May</th>
            </tr>
          </thead>
          <tbody>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="colgroup" class="govuk-table__cell"><span class="table-heading">Phone Line Data</span>Money from calls (million dollars)</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >January</span>11</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >February</span>14</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >March</span>18.5</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >April</span>12</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >May</span>19</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="colgroup" class="govuk-table__cell"><span class="table-heading" >Phone Line Data</span>Number of calls (millions)</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >January</span>22</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >February</span>27</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >March</span>32</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >April</span>26</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >May</span>40</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="colgroup" class="govuk-table__cell"><span class="table-heading" >Phone Line Data</span>Calls during weekends</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >January</span>82%</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell" >February</span>94%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >March</span>90%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >April</span>87%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell" >May</span>93%</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(296, 5, 2, 'Which sector consumed the most fossil fuels in Country Y over the three year period?', '<p class="passageHdr">Table showing use of fossil fuels for different purposes by Countries X and Y each year for three years.</p>
<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          	Fossil Fuels Consumption (in million tonnes)
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th rowspan="2" role="columnheader" scope="rowgroup">Types of Fuel Consumption</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 1</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 2</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 3</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Manufacturing</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>22</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>19</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>23</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>19</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>25</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>18</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Electricity, Gas and Water</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>27</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>25</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>28</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>26</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>30</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>28</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Transport and Communication	</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>12</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>11</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>10</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>11</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>11</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>13</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Domestic</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>26</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>28</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>24</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>23</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>24</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>21</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Mining and Quarrying</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>4</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>4</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>4</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>3</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>3</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>4</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(297, 5, 3, 'The December money from calls was 9 million dollars. Which month saw the greatest percentage increase in call money?', '<p class="passageHdr">Table showing details of calls to TV show "Talented America" each month from January to May, in terms of total numbers and money made.</p>
<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          "Talented America" Phone Line Information
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th colspan="6" role="columnheader" scope="colgroup">Month</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Phone Line Data</th>
              <th role="columnheader" scope="col" class="govuk-table__header">January</th>
              <th role="columnheader" scope="col" class="govuk-table__header">February</th>
              <th role="columnheader" scope="col" class="govuk-table__header">March</th>
              <th role="columnheader" scope="col" class="govuk-table__header">April</th>
              <th role="columnheader" scope="col" class="govuk-table__header">May</th>
            </tr>
          </thead>
          <tbody>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell  table-heading-bold"><span class="table-heading">Phone Line Data</span>Money from calls (million dollars)</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">January</span>11</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">February</span>14</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">March</span>18.5</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">April</span>12</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">May</span>19</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell  table-heading-bold"><span class="table-heading">Phone Line Data</span>Number of calls (millions)</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">January</span>22</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">February</span>27</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">March</span>32</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">April</span>26</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">May</span>40</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell  table-heading-bold"><span class="table-heading">Phone Line Data</span>Calls during weekends</th>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">January</span>82%</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">February</span>94%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">March</span>90%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">April</span>87%</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">May</span>93%</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(298, 5, 4, 'The costs for using an arcade machine are 20p for a new game and 6p for an old game. What was the total money collected in the Racing category on Saturday?', '<p class="passageHdr">Table showing amount of new and old games played in an amusement park, including the category of game played.</p>
<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          Use of arcade games
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th rowspan="2" role="columnheader" scope="rowgroup">Type of game</th>
              <th colspan="2" role="columnheader" scope="colgroup">New games played (000s)</th>
              <th colspan="2" role="columnheader" scope="colgroup">Old games played (000s)</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Saturday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Sunday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Saturday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Sunday</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Racing</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>2.9</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>3.7</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>8.6</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>8.6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Sports</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">Saturday</span>10.1</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">Sunday</span>8.6</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>14.1</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>9.7</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Fighting</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>6.7</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>6.9</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>12.2</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>12.4</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Music</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>1.6</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>2.4</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>5.1</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>4.6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Puzzles</th>
              <td role="cell" scope="row" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>3.4</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>3.5</td>
              <td role="cell" scope="row" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>6.7</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>6.3</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(299, 5, 5, 'Which site had the greatest losses (both known and unknown)?', '<p class="passageHdr">Table showing performance of Company "Prime International" across different European sites, in terms of different figures.</p>
<table class="govuk-table questionTbl responsive-table" role="table">
          <caption class="govuk-table__caption">
          Prime International: Performance by Site
          </caption>
          <thead class="govuk-table__head">
            <tr class="govuk-table__row" role="row">
              <td rowspan="2" class="govuk-table__header" aria-hidden="true"></td>
              <th role="columnheader" scope="colgroup" colspan="5" class="govuk-table__header">Site Location</th>
            </tr>
            <tr role="row">
              <th role="columnheader" scope="col" class="govuk-table__header govuk-table__header--numeric">Geneva</th>
              <th role="columnheader" scope="col" class="govuk-table__header govuk-table__header--numeric">London</th>
              <th role="columnheader" scope="col" class="govuk-table__header govuk-table__header--numeric">Munich</th>
              <th role="columnheader" scope="col" class="govuk-table__header govuk-table__header--numeric">Paris</th>
              <th role="columnheader" scope="col" class="govuk-table__header govuk-table__header--numeric">Rome</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr role="row" class="govuk-table__row">
              <th role="cell" scope="colgroup" class="govuk-table__cell" colspan="6">Performance Figures (in million £ sterling) </th>
            </tr> 
            <tr role="row" class="govuk-table__row">
              <td role="rowheader" class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Sales Revenue</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric" ><span class="table-cell">Geneva</span>109</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>533</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>495</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell numeric">Paris</span>385</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>254</td>
            </tr>
            <tr role="row" class="govuk-table__row">
              <td role="rowheader"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Operating Costs </td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Geneva</span>55</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>96</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>85</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Paris</span>75</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>103</td>
            </tr>
            <tr role="row" class="govuk-table__row">
              <td role="rowheader" class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Payroll Costs </td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Geneva</span>85</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>210</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>195</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Paris</span>112</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>91</td>
            </tr>
            <tr role="row" class="govuk-table__row">
              <td role="rowheader" class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Waste - known loss </td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Geneva</span>19</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>42</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>25</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Paris</span>90</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>78</td>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="rowheader" class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Shrinkage - unknown loss</th>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Geneva</span>7</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>21</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>17</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Paris</span>10</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>52</td>
            </tr>
            <tr role="row" class="govuk-table__row">
              <td role="rowheader" class="govuk-table__cell table-heading-bold"><span class="table-heading">Site Location</span>Number of Employees (in Thousands)</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Geneva</span>0.9</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">London</span>4.5</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Munich</span>3.8</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Paris</span>2.9</td>
              <td role="cell" class="govuk-table__cell govuk-table__cell--numeric"><span class="table-cell">Rome</span>1.5</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(300, 5, 6, 'In the racing category, we expect 15% more new games to be played on Monday compared to Sunday. How many new game plays do they expect on Monday?', '<p class="passageHdr">Table showing amount of new and old games played in an amusement park, including the category of game played.</p>
	<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          Use of arcade games
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th rowspan="2" role="columnheader" scope="rowgroup">Type of game</th>
              <th colspan="2" role="columnheader" scope="colgroup">New games played (000s)</th>
              <th colspan="2"  role="columnheader" scope="colgroup">Old games played (000s)</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Saturday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Sunday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Saturday</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Sunday</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Racing</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>2.9</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>3.7</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>8.6</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>8.6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Sports</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">Saturday</span>10.1</td>
              <td role="cell" class="govuk-table__cell"><span class="table-cell">Sunday</span>8.6</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>14.1</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>9.7</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Fighting</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>6.7</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>6.9</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>12.2</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>12.4</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Music</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>1.6</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>2.4</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>5.1</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>4.6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
				<th role="cell" scope="row"  class="govuk-table__cell table-heading-bold"><span class="table-heading">Type of game</span>Puzzles</th>
              <td role="cell" scope="row" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">New games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>3.4</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>3.5</td>
              <td role="cell" scope="row" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Old games played (000s)</span></td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Saturday</span>6.7</td>
              <td role="cell" class="govuk-table__cell " ><span class="table-cell">Sunday</span>6.3</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(301, 5, 7, 'What percentage of Country X''s total fossil fuel consumption was accounted for by Transport and Communication in Year 2?', '<p class="passageHdr">Table showing use of fossil fuels for different purposes by Countries X and Y each year for three years.</p>
<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          	Fossil Fuels Consumption (in million tonnes)
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th rowspan="2" role="columnheader" scope="rowgroup">Types of Fuel Consumption</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 1</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 2</th>
              <th colspan="2" role="columnheader" scope="colgroup">Year 3</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Country X</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Country Y</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Manufacturing</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>22</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>19</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>23</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>19</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>25</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>18</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Electricity, Gas and Water</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>27</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>25</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>28</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>26</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>30</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>28</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Transport and Communication	</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>12</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>11</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>10</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>11</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>11</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>13</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Domestic</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>26</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>28</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>24</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>23</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>24</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>21</td>
            </tr>
            <tr class="govuk-table__row" role="row">
			  <th role="cell" scope="row" class="govuk-table__cell table-heading-bold"><span class="table-heading">Types of Fuel Consumption</span>Mining and Quarrying</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>4</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>4</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>4</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>3</td>
			  <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Saturday</span>3</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Sunday</span>4</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(302, 5, 8, 'Within the Under 35 ''Others'' group, if 65% walk and the remainder cycle, what is the approximate ratio of walkers to cyclists?', '<p class="passageHdr">Table showing different types of transport used by commuters under 35 as well as those 35 or older.</p>
	<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption govuk-!-margin-bottom-3">
          Type of Transport used by Commuters in City Z this Year
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header">Age Group</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Average Number of Daily Commuters</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Rail</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Car</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Subway</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Bus</th>
			  <th role="columnheader" scope="col" class="govuk-table__header">Others</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <td role="cell" class="govuk-table__cell"><span class="table-heading table-heading-bold" >Age Group</span>Under 35</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Average Number of Daily Commuters</span>425,600</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Rail</span>43%	</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Car</span>9%	</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Subway</span>27%	</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Bus</span>12%	</td>
			  <td role="cell" class="govuk-table__cell"><span class="table-heading">Others</span>9%</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <td role="cell" class="govuk-table__cell"><span class="table-heading table-heading-bold">Age Group</span>35 and Over</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Average Number of Daily Commuters</span>638,400</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Rail</span>42%</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Car</span>12%</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Subway</span>25%	</td>
              <td role="cell" class="govuk-table__cell"><span class="table-heading">Bus</span>14%</td>
			  <td role="cell" class="govuk-table__cell"><span class="table-heading">Others</span>7%</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(303, 5, 9, 'Which factory produced the most defects in Year 2?', '<p class="passageHdr">Table showing defective components in five different factories across three years.</p>
	<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          Percentage of Defective Components in Factories
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header" aria-hidden="true"></th>
              <th role="columnheader" scope="colgroup" colspan="2" class="govuk-table__header">Year 1</th>
              <th role="columnheader" scope="colgroup" colspan="2" class="govuk-table__header">Year 2</th>
              <th role="columnheader" scope="colgroup" colspan="2" class="govuk-table__header">Year 3</th>
            </tr>
            <tr role="row" class="govuk-table__row">
              <th></th>
              <th role="columnheader" scope="colgroup" class="govuk-table__header">Units Produced</th>
              <th role="columnheader" scope="colgroup" class="govuk-table__header">Defects (%)</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Units Produced</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Defects (%)</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Units Produced</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Defects (%)</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Factory A</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>12,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>10</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>14,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>8</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>16,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Factory B</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>23,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>5</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>20,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>5</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>25,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>3</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Factory C</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>14,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>3</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>14,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>9</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>12,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>5</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Factory D</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>13,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>12</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>25,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>12</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>27,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>6</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Factory E</th>
              <td role="cell" class="govuk-table__cell table-heading table-heading-bold"><span class="table-heading">Year 1</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>21,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>15</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 2</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>16,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>12</td>
              <td role="cell" class="govuk-table__cell table-heading-bold table-heading"><span class="table-heading">Year 3</span></td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Units Produced</span>20,000</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Defects (%)</span>6</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL),
(304, 5, 10, 'If Company 1 aims to increase profits to £350,000 by Year 12, and assuming a constant rate of increase after Year 10, approximately, what is the percentage increase in profits each year to reach this target?', '<p class="passageHdr">Table showing profits in two different shipping companies across ten years.</p>
	<table class="govuk-table questionTbl responsive-table tablet-mode" role="table">
          <caption class="govuk-table__caption">
          Shipping Company Profits
          </caption>
          <thead class="govuk-table__head" role="rowgroup">
            <tr role="row" class="govuk-table__row">
              <th role="columnheader" scope="col" class="govuk-table__header" aria-hidden="true"></th>
              <th role="columnheader" scope="col" class="govuk-table__header">Profits in Company 1 (£)</th>
              <th role="columnheader" scope="col" class="govuk-table__header">Profits in Company 2 (£)</th>
            </tr>
          </thead>
          <tbody class="govuk-table__body">
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 1</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>207,934</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>202,239</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 2</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>279,182</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>218,992</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 3</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>299,734</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>231,674</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 4</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>329,722</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>249,747</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 5</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span> 330,571</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>255,964 </td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 6</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span> 367,942</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>269,827 </td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 7</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>368,794</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>275,662</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 8</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>348,371</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>280,086</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 9</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>334,791</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>289,163</td>
            </tr>
            <tr class="govuk-table__row" role="row">
              <th role="cell" scope="row" class="govuk-table__cell table-heading-bold">Year 10</th>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 1 (£)</span>317,946</td>
              <td role="cell" class="govuk-table__cell" ><span class="table-cell">Profits in Company 2 (£)</span>317,946</td>
            </tr>
          </tbody>
        </table>', NULL, NULL, NULL)
        ) AS QuestionsTemp(
	Id,
	AssessmentId,
	Number,
	Text,
	DataHTML,
	ImageTitle,
	ImageCaption,
	ImageURL
))
MERGE Questions AS target
USING (
	SELECT *
	FROM QuestionsCte
) AS source
ON target.Id = source.Id
WHEN MATCHED
	THEN UPDATE SET
		target.AssessmentId = source.AssessmentId,
		target.Number = source.Number,
		target.Text = source.Text,
		target.DataHTML = source.DataHTML,
		target.ImageTitle = source.ImageTitle,
		target.ImageCaption = source.ImageCaption,
		target.ImageURL = source.ImageURL
WHEN NOT MATCHED
	THEN INSERT (
		Id,
	    AssessmentId,
	    Number,
	    Text,
	    DataHTML,
	    ImageTitle,
	    ImageCaption,
	    ImageURL
	) VALUES (
		source.Id,
		source.AssessmentId,
		source.Number,
		source.Text,
		source.DataHTML,
		source.ImageTitle,
		source.ImageCaption,
		source.ImageURL
	)
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;

;WITH AnswersCte AS (
	SELECT * FROM (VALUES
(1, 1, 'A', 999, 'A', NULL, NULL, NULL),
(2, 1, 'B', 999, 'B', NULL, NULL, NULL),
(3, 1, 'C', 999, 'C', NULL, NULL, NULL),
(4, 1, 'D', 999, 'D', NULL, NULL, NULL),
(5, 1, 'E', 999, 'E', NULL, NULL, NULL),
(6, 1, 'A', 999, 'A', NULL, NULL, NULL),
(7, 1, 'B', 999, 'B', NULL, NULL, NULL),
(8, 1, 'C', 999, 'C', NULL, NULL, NULL),
(9, 1, 'D', 999, 'D', NULL, NULL, NULL),
(10, 1, 'E', 999, 'E', NULL, NULL, NULL),
(11, 1, 'A', 999, 'A', NULL, NULL, NULL),
(12, 1, 'B', 999, 'B', NULL, NULL, NULL),
(13, 1, 'C', 999, 'C', NULL, NULL, NULL),
(14, 1, 'D', 999, 'D', NULL, NULL, NULL),
(15, 1, 'E', 999, 'E', NULL, NULL, NULL),
(16, 1, 'A', 999, 'A', NULL, NULL, NULL),
(17, 1, 'B', 999, 'B', NULL, NULL, NULL),
(18, 1, 'C', 999, 'C', NULL, NULL, NULL),
(19, 1, 'D', 999, 'D', NULL, NULL, NULL),
(20, 1, 'E', 999, 'E', NULL, NULL, NULL),
(21, 1, 'A', 999, 'A', NULL, NULL, NULL),
(22, 1, 'B', 999, 'B', NULL, NULL, NULL),
(23, 1, 'C', 999, 'C', NULL, NULL, NULL),
(24, 1, 'D', 999, 'D', NULL, NULL, NULL),
(25, 1, 'E', 999, 'E', NULL, NULL, NULL),
(26, 1, 'A', 999, 'A', NULL, NULL, NULL),
(27, 1, 'B', 999, 'B', NULL, NULL, NULL),
(28, 1, 'C', 999, 'C', NULL, NULL, NULL),
(29, 1, 'D', 999, 'D', NULL, NULL, NULL),
(30, 1, 'E', 999, 'E', NULL, NULL, NULL),
(31, 1, 'A', 999, 'A', NULL, NULL, NULL),
(32, 1, 'B', 999, 'B', NULL, NULL, NULL),
(33, 1, 'C', 999, 'C', NULL, NULL, NULL),
(34, 1, 'D', 999, 'D', NULL, NULL, NULL),
(35, 1, 'E', 999, 'E', NULL, NULL, NULL),
(36, 1, 'A', 999, 'A', NULL, NULL, NULL),
(37, 1, 'B', 999, 'B', NULL, NULL, NULL),
(38, 1, 'C', 999, 'C', NULL, NULL, NULL),
(39, 1, 'D', 999, 'D', NULL, NULL, NULL),
(40, 1, 'E', 999, 'E', NULL, NULL, NULL),
(41, 1, 'A', 999, 'A', NULL, NULL, NULL),
(42, 1, 'B', 999, 'B', NULL, NULL, NULL),
(43, 1, 'C', 999, 'C', NULL, NULL, NULL),
(44, 1, 'D', 999, 'D', NULL, NULL, NULL),
(45, 1, 'E', 999, 'E', NULL, NULL, NULL),
(46, 1, 'A', 999, 'A', NULL, NULL, NULL),
(47, 1, 'B', 999, 'B', NULL, NULL, NULL),
(48, 1, 'C', 999, 'C', NULL, NULL, NULL),
(49, 1, 'D', 999, 'D', NULL, NULL, NULL),
(50, 1, 'E', 999, 'E', NULL, NULL, NULL),
(51, 2, 'A', 999, 'A', NULL, NULL, NULL),
(52, 2, 'B', 999, 'B', NULL, NULL, NULL),
(53, 2, 'C', 999, 'C', NULL, NULL, NULL),
(54, 2, 'D', 999, 'D', NULL, NULL, NULL),
(55, 2, 'E', 999, 'E', NULL, NULL, NULL),
(56, 2, 'A', 999, 'A', NULL, NULL, NULL),
(57, 2, 'B', 999, 'B', NULL, NULL, NULL),
(58, 2, 'C', 999, 'C', NULL, NULL, NULL),
(59, 2, 'D', 999, 'D', NULL, NULL, NULL),
(60, 2, 'E', 999, 'E', NULL, NULL, NULL),
(61, 2, 'A', 999, 'A', NULL, NULL, NULL),
(62, 2, 'B', 999, 'B', NULL, NULL, NULL),
(63, 2, 'C', 999, 'C', NULL, NULL, NULL),
(64, 2, 'D', 999, 'D', NULL, NULL, NULL),
(65, 2, 'E', 999, 'E', NULL, NULL, NULL),
(66, 2, 'A', 999, 'A', NULL, NULL, NULL),
(67, 2, 'B', 999, 'B', NULL, NULL, NULL),
(68, 2, 'C', 999, 'C', NULL, NULL, NULL),
(69, 2, 'D', 999, 'D', NULL, NULL, NULL),
(70, 2, 'E', 999, 'E', NULL, NULL, NULL),
(71, 2, 'A', 999, 'A', NULL, NULL, NULL),
(72, 2, 'B', 999, 'B', NULL, NULL, NULL),
(73, 2, 'C', 999, 'C', NULL, NULL, NULL),
(74, 2, 'D', 999, 'D', NULL, NULL, NULL),
(75, 2, 'E', 999, 'E', NULL, NULL, NULL),
(76, 2, 'A', 999, 'A', NULL, NULL, NULL),
(77, 2, 'B', 999, 'B', NULL, NULL, NULL),
(78, 2, 'C', 999, 'C', NULL, NULL, NULL),
(79, 2, 'D', 999, 'D', NULL, NULL, NULL),
(80, 2, 'E', 999, 'E', NULL, NULL, NULL),
(81, 2, 'A', 999, 'A', NULL, NULL, NULL),
(82, 2, 'B', 999, 'B', NULL, NULL, NULL),
(83, 2, 'C', 999, 'C', NULL, NULL, NULL),
(84, 2, 'D', 999, 'D', NULL, NULL, NULL),
(85, 2, 'E', 999, 'E', NULL, NULL, NULL),
(86, 2, 'A', 999, 'A', NULL, NULL, NULL),
(87, 2, 'B', 999, 'B', NULL, NULL, NULL),
(88, 2, 'C', 999, 'C', NULL, NULL, NULL),
(89, 2, 'D', 999, 'D', NULL, NULL, NULL),
(90, 2, 'E', 999, 'E', NULL, NULL, NULL),
(91, 2, 'A', 999, 'A', NULL, NULL, NULL),
(92, 2, 'B', 999, 'B', NULL, NULL, NULL),
(93, 2, 'C', 999, 'C', NULL, NULL, NULL),
(94, 2, 'D', 999, 'D', NULL, NULL, NULL),
(95, 2, 'E', 999, 'E', NULL, NULL, NULL),
(96, 2, 'A', 999, 'A', NULL, NULL, NULL),
(97, 2, 'B', 999, 'B', NULL, NULL, NULL),
(98, 2, 'C', 999, 'C', NULL, NULL, NULL),
(99, 2, 'D', 999, 'D', NULL, NULL, NULL),
(100, 2, 'E', 999, 'E', NULL, NULL, NULL),
(101, 3, 'A', 999, 'A', NULL, NULL, NULL),
(102, 3, 'B', 999, 'B', NULL, NULL, NULL),
(103, 3, 'C', 999, 'C', NULL, NULL, NULL),
(104, 3, 'D', 999, 'D', NULL, NULL, NULL),
(105, 3, 'E', 999, 'E', NULL, NULL, NULL),
(106, 3, 'A', 999, 'A', NULL, NULL, NULL),
(107, 3, 'B', 999, 'B', NULL, NULL, NULL),
(108, 3, 'C', 999, 'C', NULL, NULL, NULL),
(109, 3, 'D', 999, 'D', NULL, NULL, NULL),
(110, 3, 'E', 999, 'E', NULL, NULL, NULL),
(111, 3, 'A', 999, 'A', NULL, NULL, NULL),
(112, 3, 'B', 999, 'B', NULL, NULL, NULL),
(113, 3, 'C', 999, 'C', NULL, NULL, NULL),
(114, 3, 'D', 999, 'D', NULL, NULL, NULL),
(115, 3, 'E', 999, 'E', NULL, NULL, NULL),
(116, 3, 'A', 999, 'A', NULL, NULL, NULL),
(117, 3, 'B', 999, 'B', NULL, NULL, NULL),
(118, 3, 'C', 999, 'C', NULL, NULL, NULL),
(119, 3, 'D', 999, 'D', NULL, NULL, NULL),
(120, 3, 'E', 999, 'E', NULL, NULL, NULL),
(121, 3, 'A', 999, 'A', NULL, NULL, NULL),
(122, 3, 'B', 999, 'B', NULL, NULL, NULL),
(123, 3, 'C', 999, 'C', NULL, NULL, NULL),
(124, 3, 'D', 999, 'D', NULL, NULL, NULL),
(125, 3, 'E', 999, 'E', NULL, NULL, NULL),
(126, 3, 'A', 999, 'A', NULL, NULL, NULL),
(127, 3, 'B', 999, 'B', NULL, NULL, NULL),
(128, 3, 'C', 999, 'C', NULL, NULL, NULL),
(129, 3, 'D', 999, 'D', NULL, NULL, NULL),
(130, 3, 'E', 999, 'E', NULL, NULL, NULL),
(131, 3, 'A', 999, 'A', NULL, NULL, NULL),
(132, 3, 'B', 999, 'B', NULL, NULL, NULL),
(133, 3, 'C', 999, 'C', NULL, NULL, NULL),
(134, 3, 'D', 999, 'D', NULL, NULL, NULL),
(135, 3, 'E', 999, 'E', NULL, NULL, NULL),
(136, 3, 'A', 999, 'A', NULL, NULL, NULL),
(137, 3, 'B', 999, 'B', NULL, NULL, NULL),
(138, 3, 'C', 999, 'C', NULL, NULL, NULL),
(139, 3, 'D', 999, 'D', NULL, NULL, NULL),
(140, 3, 'E', 999, 'E', NULL, NULL, NULL),
(141, 3, 'A', 999, 'A', NULL, NULL, NULL),
(142, 3, 'B', 999, 'B', NULL, NULL, NULL),
(143, 3, 'C', 999, 'C', NULL, NULL, NULL),
(144, 3, 'D', 999, 'D', NULL, NULL, NULL),
(145, 3, 'E', 999, 'E', NULL, NULL, NULL),
(146, 3, 'A', 999, 'A', NULL, NULL, NULL),
(147, 3, 'B', 999, 'B', NULL, NULL, NULL),
(148, 3, 'C', 999, 'C', NULL, NULL, NULL),
(149, 3, 'D', 999, 'D', NULL, NULL, NULL),
(150, 3, 'E', 999, 'E', NULL, NULL, NULL),
(151, 4, 'A', 999, 'A', NULL, NULL, NULL),
(152, 4, 'B', 999, 'B', NULL, NULL, NULL),
(153, 4, 'C', 999, 'C', NULL, NULL, NULL),
(154, 4, 'D', 999, 'D', NULL, NULL, NULL),
(155, 4, 'E', 999, 'E', NULL, NULL, NULL),
(156, 4, 'A', 999, 'A', NULL, NULL, NULL),
(157, 4, 'B', 999, 'B', NULL, NULL, NULL),
(158, 4, 'C', 999, 'C', NULL, NULL, NULL),
(159, 4, 'D', 999, 'D', NULL, NULL, NULL),
(160, 4, 'E', 999, 'E', NULL, NULL, NULL),
(161, 4, 'A', 999, 'A', NULL, NULL, NULL),
(162, 4, 'B', 999, 'B', NULL, NULL, NULL),
(163, 4, 'C', 999, 'C', NULL, NULL, NULL),
(164, 4, 'D', 999, 'D', NULL, NULL, NULL),
(165, 4, 'E', 999, 'E', NULL, NULL, NULL),
(166, 4, 'A', 999, 'A', NULL, NULL, NULL),
(167, 4, 'B', 999, 'B', NULL, NULL, NULL),
(168, 4, 'C', 999, 'C', NULL, NULL, NULL),
(169, 4, 'D', 999, 'D', NULL, NULL, NULL),
(170, 4, 'E', 999, 'E', NULL, NULL, NULL),
(171, 4, 'A', 999, 'A', NULL, NULL, NULL),
(172, 4, 'B', 999, 'B', NULL, NULL, NULL),
(173, 4, 'C', 999, 'C', NULL, NULL, NULL),
(174, 4, 'D', 999, 'D', NULL, NULL, NULL),
(175, 4, 'E', 999, 'E', NULL, NULL, NULL),
(176, 4, 'A', 999, 'A', NULL, NULL, NULL),
(177, 4, 'B', 999, 'B', NULL, NULL, NULL),
(178, 4, 'C', 999, 'C', NULL, NULL, NULL),
(179, 4, 'D', 999, 'D', NULL, NULL, NULL),
(180, 4, 'E', 999, 'E', NULL, NULL, NULL),
(181, 4, 'A', 999, 'A', NULL, NULL, NULL),
(182, 4, 'B', 999, 'B', NULL, NULL, NULL),
(183, 4, 'C', 999, 'C', NULL, NULL, NULL),
(184, 4, 'D', 999, 'D', NULL, NULL, NULL),
(185, 4, 'E', 999, 'E', NULL, NULL, NULL),
(186, 4, 'A', 999, 'A', NULL, NULL, NULL),
(187, 4, 'B', 999, 'B', NULL, NULL, NULL),
(188, 4, 'C', 999, 'C', NULL, NULL, NULL),
(189, 4, 'D', 999, 'D', NULL, NULL, NULL),
(190, 4, 'E', 999, 'E', NULL, NULL, NULL),
(191, 4, 'A', 999, 'A', NULL, NULL, NULL),
(192, 4, 'B', 999, 'B', NULL, NULL, NULL),
(193, 4, 'C', 999, 'C', NULL, NULL, NULL),
(194, 4, 'D', 999, 'D', NULL, NULL, NULL),
(195, 4, 'E', 999, 'E', NULL, NULL, NULL),
(196, 4, 'A', 999, 'A', NULL, NULL, NULL),
(197, 4, 'B', 999, 'B', NULL, NULL, NULL),
(198, 4, 'C', 999, 'C', NULL, NULL, NULL),
(199, 4, 'D', 999, 'D', NULL, NULL, NULL),
(200, 4, 'E', 999, 'E', NULL, NULL, NULL),
(201, 5, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(202, 5, 'A', 999, 'A little interested', NULL, NULL, NULL),
(203, 5, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(204, 5, 'A', 999, 'Very interested', NULL, NULL, NULL),
(205, 5, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(206, 5, 'B', 999, 'Not at all interested', NULL, NULL, NULL),
(207, 5, 'B', 999, 'A little interested', NULL, NULL, NULL),
(208, 5, 'B', 999, 'Moderately interested', NULL, NULL, NULL),
(209, 5, 'B', 999, 'Very interested', NULL, NULL, NULL),
(210, 5, 'B', 999, 'Extremely interested', NULL, NULL, NULL),
(211, 5, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(212, 5, 'C', 999, 'A little interested', NULL, NULL, NULL),
(213, 5, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(214, 5, 'C', 999, 'Very interested', NULL, NULL, NULL),
(215, 5, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(216, 5, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(217, 5, 'D', 999, 'A little interested', NULL, NULL, NULL),
(218, 5, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(219, 5, 'D', 999, 'Very interested', NULL, NULL, NULL),
(220, 5, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(221, 5, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(222, 5, 'E', 999, 'A little interested', NULL, NULL, NULL),
(223, 5, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(224, 5, 'E', 999, 'Very interested', NULL, NULL, NULL),
(225, 5, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(226, 6, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(227, 6, 'F', 999, 'A little interested', NULL, NULL, NULL),
(228, 6, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(229, 6, 'F', 999, 'Very interested', NULL, NULL, NULL),
(230, 6, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(231, 6, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(232, 6, 'G', 999, 'A little interested', NULL, NULL, NULL),
(233, 6, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(234, 6, 'G', 999, 'Very interested', NULL, NULL, NULL),
(235, 6, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(236, 6, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(237, 6, 'H', 999, 'A little interested', NULL, NULL, NULL),
(238, 6, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(239, 6, 'H', 999, 'Very interested', NULL, NULL, NULL),
(240, 6, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(241, 6, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(242, 6, 'I', 999, 'A little interested', NULL, NULL, NULL),
(243, 6, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(244, 6, 'I', 999, 'Very interested', NULL, NULL, NULL),
(245, 6, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(246, 6, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(247, 6, 'J', 999, 'A little interested', NULL, NULL, NULL),
(248, 6, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(249, 6, 'J', 999, 'Very interested', NULL, NULL, NULL),
(250, 6, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(251, 7, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(252, 7, 'K', 999, 'A little interested', NULL, NULL, NULL),
(253, 7, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(254, 7, 'K', 999, 'Very interested', NULL, NULL, NULL),
(255, 7, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(256, 7, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(257, 7, 'A', 999, 'A little interested', NULL, NULL, NULL),
(258, 7, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(259, 7, 'A', 999, 'Very interested', NULL, NULL, NULL),
(260, 7, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(261, 7, 'B', 999, 'Not at all interested', NULL, NULL, NULL),
(262, 7, 'B', 999, 'A little interested', NULL, NULL, NULL),
(263, 7, 'B', 999, 'Moderately interested', NULL, NULL, NULL),
(264, 7, 'B', 999, 'Very interested', NULL, NULL, NULL),
(265, 7, 'B', 999, 'Extremely interested', NULL, NULL, NULL),
(266, 7, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(267, 7, 'C', 999, 'A little interested', NULL, NULL, NULL),
(268, 7, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(269, 7, 'C', 999, 'Very interested', NULL, NULL, NULL),
(270, 7, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(271, 7, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(272, 7, 'D', 999, 'A little interested', NULL, NULL, NULL),
(273, 7, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(274, 7, 'D', 999, 'Very interested', NULL, NULL, NULL),
(275, 7, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(276, 8, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(277, 8, 'E', 999, 'A little interested', NULL, NULL, NULL),
(278, 8, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(279, 8, 'E', 999, 'Very interested', NULL, NULL, NULL),
(280, 8, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(281, 8, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(282, 8, 'F', 999, 'A little interested', NULL, NULL, NULL),
(283, 8, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(284, 8, 'F', 999, 'Very interested', NULL, NULL, NULL),
(285, 8, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(286, 8, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(287, 8, 'G', 999, 'A little interested', NULL, NULL, NULL),
(288, 8, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(289, 8, 'G', 999, 'Very interested', NULL, NULL, NULL),
(290, 8, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(291, 8, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(292, 8, 'H', 999, 'A little interested', NULL, NULL, NULL),
(293, 8, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(294, 8, 'H', 999, 'Very interested', NULL, NULL, NULL),
(295, 8, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(296, 8, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(297, 8, 'I', 999, 'A little interested', NULL, NULL, NULL),
(298, 8, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(299, 8, 'I', 999, 'Very interested', NULL, NULL, NULL),
(300, 8, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(301, 9, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(302, 9, 'J', 999, 'A little interested', NULL, NULL, NULL),
(303, 9, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(304, 9, 'J', 999, 'Very interested', NULL, NULL, NULL),
(305, 9, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(306, 9, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(307, 9, 'K', 999, 'A little interested', NULL, NULL, NULL),
(308, 9, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(309, 9, 'K', 999, 'Very interested', NULL, NULL, NULL),
(310, 9, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(311, 9, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(312, 9, 'A', 999, 'A little interested', NULL, NULL, NULL),
(313, 9, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(314, 9, 'A', 999, 'Very interested', NULL, NULL, NULL),
(315, 9, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(316, 9, 'B', 999, 'Not at all interested', NULL, NULL, NULL),
(317, 9, 'B', 999, 'A little interested', NULL, NULL, NULL),
(318, 9, 'B', 999, 'Moderately interested', NULL, NULL, NULL),
(319, 9, 'B', 999, 'Very interested', NULL, NULL, NULL),
(320, 9, 'B', 999, 'Extremely interested', NULL, NULL, NULL),
(321, 9, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(322, 9, 'C', 999, 'A little interested', NULL, NULL, NULL),
(323, 9, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(324, 9, 'C', 999, 'Very interested', NULL, NULL, NULL),
(325, 9, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(326, 10, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(327, 10, 'D', 999, 'A little interested', NULL, NULL, NULL),
(328, 10, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(329, 10, 'D', 999, 'Very interested', NULL, NULL, NULL),
(330, 10, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(331, 10, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(332, 10, 'E', 999, 'A little interested', NULL, NULL, NULL),
(333, 10, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(334, 10, 'E', 999, 'Very interested', NULL, NULL, NULL),
(335, 10, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(336, 10, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(337, 10, 'F', 999, 'A little interested', NULL, NULL, NULL),
(338, 10, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(339, 10, 'F', 999, 'Very interested', NULL, NULL, NULL),
(340, 10, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(341, 10, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(342, 10, 'G', 999, 'A little interested', NULL, NULL, NULL),
(343, 10, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(344, 10, 'G', 999, 'Very interested', NULL, NULL, NULL),
(345, 10, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(346, 10, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(347, 10, 'H', 999, 'A little interested', NULL, NULL, NULL),
(348, 10, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(349, 10, 'H', 999, 'Very interested', NULL, NULL, NULL),
(350, 10, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(351, 11, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(352, 11, 'I', 999, 'A little interested', NULL, NULL, NULL),
(353, 11, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(354, 11, 'I', 999, 'Very interested', NULL, NULL, NULL),
(355, 11, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(356, 11, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(357, 11, 'J', 999, 'A little interested', NULL, NULL, NULL),
(358, 11, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(359, 11, 'J', 999, 'Very interested', NULL, NULL, NULL),
(360, 11, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(361, 11, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(362, 11, 'K', 999, 'A little interested', NULL, NULL, NULL),
(363, 11, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(364, 11, 'K', 999, 'Very interested', NULL, NULL, NULL),
(365, 11, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(366, 11, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(367, 11, 'A', 999, 'A little interested', NULL, NULL, NULL),
(368, 11, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(369, 11, 'A', 999, 'Very interested', NULL, NULL, NULL),
(370, 11, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(371, 11, 'B', 999, 'Not at all interested', NULL, NULL, NULL),
(372, 11, 'B', 999, 'A little interested', NULL, NULL, NULL),
(373, 11, 'B', 999, 'Moderately interested', NULL, NULL, NULL),
(374, 11, 'B', 999, 'Very interested', NULL, NULL, NULL),
(375, 11, 'B', 999, 'Extremely interested', NULL, NULL, NULL),
(376, 12, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(377, 12, 'C', 999, 'A little interested', NULL, NULL, NULL),
(378, 12, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(379, 12, 'C', 999, 'Very interested', NULL, NULL, NULL),
(380, 12, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(381, 12, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(382, 12, 'D', 999, 'A little interested', NULL, NULL, NULL),
(383, 12, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(384, 12, 'D', 999, 'Very interested', NULL, NULL, NULL),
(385, 12, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(386, 12, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(387, 12, 'E', 999, 'A little interested', NULL, NULL, NULL),
(388, 12, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(389, 12, 'E', 999, 'Very interested', NULL, NULL, NULL),
(390, 12, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(391, 12, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(392, 12, 'F', 999, 'A little interested', NULL, NULL, NULL),
(393, 12, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(394, 12, 'F', 999, 'Very interested', NULL, NULL, NULL),
(395, 12, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(396, 12, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(397, 12, 'G', 999, 'A little interested', NULL, NULL, NULL),
(398, 12, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(399, 12, 'G', 999, 'Very interested', NULL, NULL, NULL),
(400, 12, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(401, 13, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(402, 13, 'H', 999, 'A little interested', NULL, NULL, NULL),
(403, 13, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(404, 13, 'H', 999, 'Very interested', NULL, NULL, NULL),
(405, 13, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(406, 13, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(407, 13, 'I', 999, 'A little interested', NULL, NULL, NULL),
(408, 13, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(409, 13, 'I', 999, 'Very interested', NULL, NULL, NULL),
(410, 13, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(411, 13, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(412, 13, 'J', 999, 'A little interested', NULL, NULL, NULL),
(413, 13, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(414, 13, 'J', 999, 'Very interested', NULL, NULL, NULL),
(415, 13, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(416, 13, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(417, 13, 'K', 999, 'A little interested', NULL, NULL, NULL),
(418, 13, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(419, 13, 'K', 999, 'Very interested', NULL, NULL, NULL),
(420, 13, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(421, 13, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(422, 13, 'A', 999, 'A little interested', NULL, NULL, NULL),
(423, 13, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(424, 13, 'A', 999, 'Very interested', NULL, NULL, NULL),
(425, 13, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(426, 14, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(427, 14, 'C', 999, 'A little interested', NULL, NULL, NULL),
(428, 14, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(429, 14, 'C', 999, 'Very interested', NULL, NULL, NULL),
(430, 14, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(431, 14, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(432, 14, 'D', 999, 'A little interested', NULL, NULL, NULL),
(433, 14, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(434, 14, 'D', 999, 'Very interested', NULL, NULL, NULL),
(435, 14, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(436, 14, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(437, 14, 'E', 999, 'A little interested', NULL, NULL, NULL),
(438, 14, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(439, 14, 'E', 999, 'Very interested', NULL, NULL, NULL),
(440, 14, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(441, 14, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(442, 14, 'F', 999, 'A little interested', NULL, NULL, NULL),
(443, 14, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(444, 14, 'F', 999, 'Very interested', NULL, NULL, NULL),
(445, 14, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(446, 14, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(447, 14, 'G', 999, 'A little interested', NULL, NULL, NULL),
(448, 14, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(449, 14, 'G', 999, 'Very interested', NULL, NULL, NULL),
(450, 14, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(451, 15, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(452, 15, 'H', 999, 'A little interested', NULL, NULL, NULL),
(453, 15, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(454, 15, 'H', 999, 'Very interested', NULL, NULL, NULL),
(455, 15, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(456, 15, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(457, 15, 'I', 999, 'A little interested', NULL, NULL, NULL),
(458, 15, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(459, 15, 'I', 999, 'Very interested', NULL, NULL, NULL),
(460, 15, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(461, 15, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(462, 15, 'J', 999, 'A little interested', NULL, NULL, NULL),
(463, 15, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(464, 15, 'J', 999, 'Very interested', NULL, NULL, NULL),
(465, 15, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(466, 15, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(467, 15, 'K', 999, 'A little interested', NULL, NULL, NULL),
(468, 15, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(469, 15, 'K', 999, 'Very interested', NULL, NULL, NULL),
(470, 15, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(471, 15, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(472, 15, 'A', 999, 'A little interested', NULL, NULL, NULL),
(473, 15, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(474, 15, 'A', 999, 'Very interested', NULL, NULL, NULL),
(475, 15, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(476, 16, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(477, 16, 'C', 999, 'A little interested', NULL, NULL, NULL),
(478, 16, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(479, 16, 'C', 999, 'Very interested', NULL, NULL, NULL),
(480, 16, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(481, 16, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(482, 16, 'D', 999, 'A little interested', NULL, NULL, NULL),
(483, 16, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(484, 16, 'D', 999, 'Very interested', NULL, NULL, NULL),
(485, 16, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(486, 16, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(487, 16, 'E', 999, 'A little interested', NULL, NULL, NULL),
(488, 16, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(489, 16, 'E', 999, 'Very interested', NULL, NULL, NULL),
(490, 16, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(491, 16, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(492, 16, 'F', 999, 'A little interested', NULL, NULL, NULL),
(493, 16, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(494, 16, 'F', 999, 'Very interested', NULL, NULL, NULL),
(495, 16, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(496, 16, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(497, 16, 'G', 999, 'A little interested', NULL, NULL, NULL),
(498, 16, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(499, 16, 'G', 999, 'Very interested', NULL, NULL, NULL),
(500, 16, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(501, 17, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(502, 17, 'H', 999, 'A little interested', NULL, NULL, NULL),
(503, 17, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(504, 17, 'H', 999, 'Very interested', NULL, NULL, NULL),
(505, 17, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(506, 17, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(507, 17, 'I', 999, 'A little interested', NULL, NULL, NULL),
(508, 17, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(509, 17, 'I', 999, 'Very interested', NULL, NULL, NULL),
(510, 17, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(511, 17, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(512, 17, 'J', 999, 'A little interested', NULL, NULL, NULL),
(513, 17, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(514, 17, 'J', 999, 'Very interested', NULL, NULL, NULL),
(515, 17, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(516, 17, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(517, 17, 'K', 999, 'A little interested', NULL, NULL, NULL),
(518, 17, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(519, 17, 'K', 999, 'Very interested', NULL, NULL, NULL),
(520, 17, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(521, 17, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(522, 17, 'A', 999, 'A little interested', NULL, NULL, NULL),
(523, 17, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(524, 17, 'A', 999, 'Very interested', NULL, NULL, NULL),
(525, 17, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(526, 18, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(527, 18, 'C', 999, 'A little interested', NULL, NULL, NULL),
(528, 18, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(529, 18, 'C', 999, 'Very interested', NULL, NULL, NULL),
(530, 18, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(531, 18, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(532, 18, 'D', 999, 'A little interested', NULL, NULL, NULL),
(533, 18, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(534, 18, 'D', 999, 'Very interested', NULL, NULL, NULL),
(535, 18, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(536, 18, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(537, 18, 'E', 999, 'A little interested', NULL, NULL, NULL),
(538, 18, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(539, 18, 'E', 999, 'Very interested', NULL, NULL, NULL),
(540, 18, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(541, 18, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(542, 18, 'F', 999, 'A little interested', NULL, NULL, NULL),
(543, 18, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(544, 18, 'F', 999, 'Very interested', NULL, NULL, NULL),
(545, 18, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(546, 18, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(547, 18, 'G', 999, 'A little interested', NULL, NULL, NULL),
(548, 18, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(549, 18, 'G', 999, 'Very interested', NULL, NULL, NULL),
(550, 18, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(551, 19, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(552, 19, 'H', 999, 'A little interested', NULL, NULL, NULL),
(553, 19, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(554, 19, 'H', 999, 'Very interested', NULL, NULL, NULL),
(555, 19, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(556, 19, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(557, 19, 'I', 999, 'A little interested', NULL, NULL, NULL),
(558, 19, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(559, 19, 'I', 999, 'Very interested', NULL, NULL, NULL),
(560, 19, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(561, 19, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(562, 19, 'J', 999, 'A little interested', NULL, NULL, NULL),
(563, 19, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(564, 19, 'J', 999, 'Very interested', NULL, NULL, NULL),
(565, 19, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(566, 19, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(567, 19, 'K', 999, 'A little interested', NULL, NULL, NULL),
(568, 19, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(569, 19, 'K', 999, 'Very interested', NULL, NULL, NULL),
(570, 19, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(571, 19, 'A', 999, 'Not at all interested', NULL, NULL, NULL),
(572, 19, 'A', 999, 'A little interested', NULL, NULL, NULL),
(573, 19, 'A', 999, 'Moderately interested', NULL, NULL, NULL),
(574, 19, 'A', 999, 'Very interested', NULL, NULL, NULL),
(575, 19, 'A', 999, 'Extremely interested', NULL, NULL, NULL),
(576, 20, 'C', 999, 'Not at all interested', NULL, NULL, NULL),
(577, 20, 'C', 999, 'A little interested', NULL, NULL, NULL),
(578, 20, 'C', 999, 'Moderately interested', NULL, NULL, NULL),
(579, 20, 'C', 999, 'Very interested', NULL, NULL, NULL),
(580, 20, 'C', 999, 'Extremely interested', NULL, NULL, NULL),
(581, 20, 'D', 999, 'Not at all interested', NULL, NULL, NULL),
(582, 20, 'D', 999, 'A little interested', NULL, NULL, NULL),
(583, 20, 'D', 999, 'Moderately interested', NULL, NULL, NULL),
(584, 20, 'D', 999, 'Very interested', NULL, NULL, NULL),
(585, 20, 'D', 999, 'Extremely interested', NULL, NULL, NULL),
(586, 20, 'E', 999, 'Not at all interested', NULL, NULL, NULL),
(587, 20, 'E', 999, 'A little interested', NULL, NULL, NULL),
(588, 20, 'E', 999, 'Moderately interested', NULL, NULL, NULL),
(589, 20, 'E', 999, 'Very interested', NULL, NULL, NULL),
(590, 20, 'E', 999, 'Extremely interested', NULL, NULL, NULL),
(591, 20, 'F', 999, 'Not at all interested', NULL, NULL, NULL),
(592, 20, 'F', 999, 'A little interested', NULL, NULL, NULL),
(593, 20, 'F', 999, 'Moderately interested', NULL, NULL, NULL),
(594, 20, 'F', 999, 'Very interested', NULL, NULL, NULL),
(595, 20, 'F', 999, 'Extremely interested', NULL, NULL, NULL),
(596, 20, 'G', 999, 'Not at all interested', NULL, NULL, NULL),
(597, 20, 'G', 999, 'A little interested', NULL, NULL, NULL),
(598, 20, 'G', 999, 'Moderately interested', NULL, NULL, NULL),
(599, 20, 'G', 999, 'Very interested', NULL, NULL, NULL),
(600, 20, 'G', 999, 'Extremely interested', NULL, NULL, NULL),
(601, 21, 'H', 999, 'Not at all interested', NULL, NULL, NULL),
(602, 21, 'H', 999, 'A little interested', NULL, NULL, NULL),
(603, 21, 'H', 999, 'Moderately interested', NULL, NULL, NULL),
(604, 21, 'H', 999, 'Very interested', NULL, NULL, NULL),
(605, 21, 'H', 999, 'Extremely interested', NULL, NULL, NULL),
(606, 21, 'I', 999, 'Not at all interested', NULL, NULL, NULL),
(607, 21, 'I', 999, 'A little interested', NULL, NULL, NULL),
(608, 21, 'I', 999, 'Moderately interested', NULL, NULL, NULL),
(609, 21, 'I', 999, 'Very interested', NULL, NULL, NULL),
(610, 21, 'I', 999, 'Extremely interested', NULL, NULL, NULL),
(611, 21, 'J', 999, 'Not at all interested', NULL, NULL, NULL),
(612, 21, 'J', 999, 'A little interested', NULL, NULL, NULL),
(613, 21, 'J', 999, 'Moderately interested', NULL, NULL, NULL),
(614, 21, 'J', 999, 'Very interested', NULL, NULL, NULL),
(615, 21, 'J', 999, 'Extremely interested', NULL, NULL, NULL),
(616, 21, 'K', 999, 'Not at all interested', NULL, NULL, NULL),
(617, 21, 'K', 999, 'A little interested', NULL, NULL, NULL),
(618, 21, 'K', 999, 'Moderately interested', NULL, NULL, NULL),
(619, 21, 'K', 999, 'Very interested', NULL, NULL, NULL),
(620, 21, 'K', 999, 'Extremely interested', NULL, NULL, NULL),
(621, 176, 'A', 999, 'A', NULL, NULL, NULL),
(622, 176, 'B', 999, 'B', NULL, NULL, NULL),
(623, 176, 'Neither', 999, 'Neither', NULL, NULL, NULL),
(624, 177, 'A', 999, 'A', NULL, NULL, NULL),
(625, 177, 'B', 999, 'B', NULL, NULL, NULL),
(626, 177, 'Both', 999, 'Both', NULL, NULL, NULL),
(627, 178, 'A', 999, 'A', NULL, NULL, NULL),
(628, 178, 'B', 999, 'B', NULL, NULL, NULL),
(629, 178, 'C', 999, 'C', NULL, NULL, NULL),
(630, 179, 'A', 999, 'A', NULL, NULL, NULL),
(631, 179, 'B', 999, 'B', NULL, NULL, NULL),
(632, 179, 'Either', 999, 'Either', NULL, NULL, NULL),
(633, 180, 'A', 999, 'A', NULL, NULL, NULL),
(634, 180, 'B', 999, 'B', NULL, NULL, NULL),
(635, 180, 'C', 999, 'C', NULL, NULL, NULL),
(636, 181, 'A', 999, 'A', NULL, NULL, NULL),
(637, 181, 'B', 999, 'B', NULL, NULL, NULL),
(638, 181, 'Equal', 999, 'Equal', NULL, NULL, NULL),
(639, 182, 'A', 999, 'A', NULL, NULL, NULL),
(640, 182, 'B', 999, 'B', NULL, NULL, NULL),
(641, 182, 'C', 999, 'C', NULL, NULL, NULL),
(642, 183, 'A', 999, 'A', NULL, NULL, NULL),
(643, 183, 'B', 999, 'B', NULL, NULL, NULL),
(644, 183, 'C', 999, 'C', NULL, NULL, NULL),
(645, 184, 'A', 999, 'A', NULL, NULL, NULL),
(646, 184, 'B', 999, 'B', NULL, NULL, NULL),
(647, 184, 'C', 999, 'C', NULL, NULL, NULL),
(648, 185, 'A', 999, 'A', NULL, NULL, NULL),
(649, 185, 'B', 999, 'B', NULL, NULL, NULL),
(650, 185, 'C', 999, 'C', NULL, NULL, NULL),
(651, 186, 'A', 999, 'A', NULL, NULL, NULL),
(652, 186, 'B', 999, 'B', NULL, NULL, NULL),
(653, 186, 'Equal', 999, 'Equal', NULL, NULL, NULL),
(654, 187, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(655, 187, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(656, 187, 'A', 999, 'Generally important', NULL, NULL, NULL),
(657, 187, 'A', 999, 'Very important', NULL, NULL, NULL),
(658, 187, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(659, 187, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(660, 187, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(661, 187, 'B', 999, 'Generally important', NULL, NULL, NULL),
(662, 187, 'B', 999, 'Very important', NULL, NULL, NULL),
(663, 187, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(664, 187, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(665, 187, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(666, 187, 'C', 999, 'Generally important', NULL, NULL, NULL),
(667, 187, 'C', 999, 'Very important', NULL, NULL, NULL),
(668, 187, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(669, 187, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(670, 187, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(671, 187, 'D', 999, 'Generally important', NULL, NULL, NULL),
(672, 187, 'D', 999, 'Very important', NULL, NULL, NULL),
(673, 187, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(674, 187, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(675, 187, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(676, 187, 'E', 999, 'Generally important', NULL, NULL, NULL),
(677, 187, 'E', 999, 'Very important', NULL, NULL, NULL),
(678, 187, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(679, 188, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(680, 188, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(681, 188, 'F', 999, 'Generally important', NULL, NULL, NULL),
(682, 188, 'F', 999, 'Very important', NULL, NULL, NULL),
(683, 188, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(684, 188, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(685, 188, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(686, 188, 'G', 999, 'Generally important', NULL, NULL, NULL),
(687, 188, 'G', 999, 'Very important', NULL, NULL, NULL),
(688, 188, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(689, 188, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(690, 188, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(691, 188, 'A', 999, 'Generally important', NULL, NULL, NULL),
(692, 188, 'A', 999, 'Very important', NULL, NULL, NULL),
(693, 188, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(694, 188, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(695, 188, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(696, 188, 'B', 999, 'Generally important', NULL, NULL, NULL),
(697, 188, 'B', 999, 'Very important', NULL, NULL, NULL),
(698, 188, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(699, 188, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(700, 188, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(701, 188, 'C', 999, 'Generally important', NULL, NULL, NULL),
(702, 188, 'C', 999, 'Very important', NULL, NULL, NULL),
(703, 188, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(704, 189, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(705, 189, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(706, 189, 'D', 999, 'Generally important', NULL, NULL, NULL),
(707, 189, 'D', 999, 'Very important', NULL, NULL, NULL),
(708, 189, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(709, 189, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(710, 189, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(711, 189, 'E', 999, 'Generally important', NULL, NULL, NULL),
(712, 189, 'E', 999, 'Very important', NULL, NULL, NULL),
(713, 189, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(714, 189, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(715, 189, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(716, 189, 'F', 999, 'Generally important', NULL, NULL, NULL),
(717, 189, 'F', 999, 'Very important', NULL, NULL, NULL),
(718, 189, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(719, 189, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(720, 189, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(721, 189, 'G', 999, 'Generally important', NULL, NULL, NULL),
(722, 189, 'G', 999, 'Very important', NULL, NULL, NULL),
(723, 189, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(724, 189, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(725, 189, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(726, 189, 'A', 999, 'Generally important', NULL, NULL, NULL),
(727, 189, 'A', 999, 'Very important', NULL, NULL, NULL),
(728, 189, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(729, 190, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(730, 190, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(731, 190, 'B', 999, 'Generally important', NULL, NULL, NULL),
(732, 190, 'B', 999, 'Very important', NULL, NULL, NULL),
(733, 190, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(734, 190, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(735, 190, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(736, 190, 'C', 999, 'Generally important', NULL, NULL, NULL),
(737, 190, 'C', 999, 'Very important', NULL, NULL, NULL),
(738, 190, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(739, 190, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(740, 190, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(741, 190, 'D', 999, 'Generally important', NULL, NULL, NULL),
(742, 190, 'D', 999, 'Very important', NULL, NULL, NULL),
(743, 190, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(744, 190, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(745, 190, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(746, 190, 'E', 999, 'Generally important', NULL, NULL, NULL),
(747, 190, 'E', 999, 'Very important', NULL, NULL, NULL),
(748, 190, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(749, 190, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(750, 190, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(751, 190, 'F', 999, 'Generally important', NULL, NULL, NULL),
(752, 190, 'F', 999, 'Very important', NULL, NULL, NULL),
(753, 190, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(754, 191, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(755, 191, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(756, 191, 'G', 999, 'Generally important', NULL, NULL, NULL),
(757, 191, 'G', 999, 'Very important', NULL, NULL, NULL),
(758, 191, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(759, 191, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(760, 191, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(761, 191, 'A', 999, 'Generally important', NULL, NULL, NULL),
(762, 191, 'A', 999, 'Very important', NULL, NULL, NULL),
(763, 191, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(764, 191, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(765, 191, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(766, 191, 'B', 999, 'Generally important', NULL, NULL, NULL),
(767, 191, 'B', 999, 'Very important', NULL, NULL, NULL),
(768, 191, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(769, 191, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(770, 191, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(771, 191, 'C', 999, 'Generally important', NULL, NULL, NULL),
(772, 191, 'C', 999, 'Very important', NULL, NULL, NULL),
(773, 191, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(774, 191, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(775, 191, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(776, 191, 'D', 999, 'Generally important', NULL, NULL, NULL),
(777, 191, 'D', 999, 'Very important', NULL, NULL, NULL),
(778, 191, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(779, 192, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(780, 192, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(781, 192, 'E', 999, 'Generally important', NULL, NULL, NULL),
(782, 192, 'E', 999, 'Very important', NULL, NULL, NULL),
(783, 192, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(784, 192, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(785, 192, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(786, 192, 'F', 999, 'Generally important', NULL, NULL, NULL),
(787, 192, 'F', 999, 'Very important', NULL, NULL, NULL),
(788, 192, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(789, 192, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(790, 192, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(791, 192, 'G', 999, 'Generally important', NULL, NULL, NULL),
(792, 192, 'G', 999, 'Very important', NULL, NULL, NULL),
(793, 192, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(794, 192, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(795, 192, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(796, 192, 'A', 999, 'Generally important', NULL, NULL, NULL),
(797, 192, 'A', 999, 'Very important', NULL, NULL, NULL),
(798, 192, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(799, 192, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(800, 192, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(801, 192, 'B', 999, 'Generally important', NULL, NULL, NULL),
(802, 192, 'B', 999, 'Very important', NULL, NULL, NULL),
(803, 192, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(804, 193, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(805, 193, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(806, 193, 'C', 999, 'Generally important', NULL, NULL, NULL),
(807, 193, 'C', 999, 'Very important', NULL, NULL, NULL),
(808, 193, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(809, 193, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(810, 193, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(811, 193, 'D', 999, 'Generally important', NULL, NULL, NULL),
(812, 193, 'D', 999, 'Very important', NULL, NULL, NULL),
(813, 193, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(814, 193, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(815, 193, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(816, 193, 'E', 999, 'Generally important', NULL, NULL, NULL),
(817, 193, 'E', 999, 'Very important', NULL, NULL, NULL),
(818, 193, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(819, 193, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(820, 193, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(821, 193, 'F', 999, 'Generally important', NULL, NULL, NULL),
(822, 193, 'F', 999, 'Very important', NULL, NULL, NULL),
(823, 193, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(824, 193, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(825, 193, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(826, 193, 'G', 999, 'Generally important', NULL, NULL, NULL),
(827, 193, 'G', 999, 'Very important', NULL, NULL, NULL),
(828, 193, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(829, 194, 'A', 999, 'Of no importance', NULL, NULL, NULL),
(830, 194, 'A', 999, 'Of some importance', NULL, NULL, NULL),
(831, 194, 'A', 999, 'Generally important', NULL, NULL, NULL),
(832, 194, 'A', 999, 'Very important', NULL, NULL, NULL),
(833, 194, 'A', 999, 'Extremely important', NULL, NULL, NULL),
(834, 194, 'B', 999, 'Of no importance', NULL, NULL, NULL),
(835, 194, 'B', 999, 'Of some importance', NULL, NULL, NULL),
(836, 194, 'B', 999, 'Generally important', NULL, NULL, NULL),
(837, 194, 'B', 999, 'Very important', NULL, NULL, NULL),
(838, 194, 'B', 999, 'Extremely important', NULL, NULL, NULL),
(839, 194, 'C', 999, 'Of no importance', NULL, NULL, NULL),
(840, 194, 'C', 999, 'Of some importance', NULL, NULL, NULL),
(841, 194, 'C', 999, 'Generally important', NULL, NULL, NULL),
(842, 194, 'C', 999, 'Very important', NULL, NULL, NULL),
(843, 194, 'C', 999, 'Extremely important', NULL, NULL, NULL),
(844, 194, 'D', 999, 'Of no importance', NULL, NULL, NULL),
(845, 194, 'D', 999, 'Of some importance', NULL, NULL, NULL),
(846, 194, 'D', 999, 'Generally important', NULL, NULL, NULL),
(847, 194, 'D', 999, 'Very important', NULL, NULL, NULL),
(848, 194, 'D', 999, 'Extremely important', NULL, NULL, NULL),
(849, 194, 'E', 999, 'Of no importance', NULL, NULL, NULL),
(850, 194, 'E', 999, 'Of some importance', NULL, NULL, NULL),
(851, 194, 'E', 999, 'Generally important', NULL, NULL, NULL),
(852, 194, 'E', 999, 'Very important', NULL, NULL, NULL),
(853, 194, 'E', 999, 'Extremely important', NULL, NULL, NULL),
(854, 195, 'F', 999, 'Of no importance', NULL, NULL, NULL),
(855, 195, 'F', 999, 'Of some importance', NULL, NULL, NULL),
(856, 195, 'F', 999, 'Generally important', NULL, NULL, NULL),
(857, 195, 'F', 999, 'Very important', NULL, NULL, NULL),
(858, 195, 'F', 999, 'Extremely important', NULL, NULL, NULL),
(859, 195, 'G', 999, 'Of no importance', NULL, NULL, NULL),
(860, 195, 'G', 999, 'Of some importance', NULL, NULL, NULL),
(861, 195, 'G', 999, 'Generally important', NULL, NULL, NULL),
(862, 195, 'G', 999, 'Very important', NULL, NULL, NULL),
(863, 195, 'G', 999, 'Extremely important', NULL, NULL, NULL),
(864, 206, 'A1', 999, 'Not at all', NULL, NULL, NULL),
(865, 206, 'A1', 999, 'A little', NULL, NULL, NULL),
(866, 206, 'A1', 999, 'Moderately', NULL, NULL, NULL),
(867, 206, 'A1', 999, 'Quite often', NULL, NULL, NULL),
(868, 206, 'A1', 999, 'Very often', NULL, NULL, NULL),
(869, 206, 'B1', 999, 'Not at all', NULL, NULL, NULL),
(870, 206, 'B1', 999, 'A little', NULL, NULL, NULL),
(871, 206, 'B1', 999, 'Moderately', NULL, NULL, NULL),
(872, 206, 'B1', 999, 'Quite often', NULL, NULL, NULL),
(873, 206, 'B1', 999, 'Very often', NULL, NULL, NULL),
(874, 206, 'C1', 999, 'Not at all', NULL, NULL, NULL),
(875, 206, 'C1', 999, 'A little', NULL, NULL, NULL),
(876, 206, 'C1', 999, 'Moderately', NULL, NULL, NULL),
(877, 206, 'C1', 999, 'Quite often', NULL, NULL, NULL),
(878, 206, 'C1', 999, 'Very often', NULL, NULL, NULL),
(879, 206, 'D1', 999, 'Not at all', NULL, NULL, NULL),
(880, 206, 'D1', 999, 'A little', NULL, NULL, NULL),
(881, 206, 'D1', 999, 'Moderately', NULL, NULL, NULL),
(882, 206, 'D1', 999, 'Quite often', NULL, NULL, NULL),
(883, 206, 'D1', 999, 'Very often', NULL, NULL, NULL),
(884, 206, 'A2', 999, 'Not at all', NULL, NULL, NULL),
(885, 206, 'A2', 999, 'A little', NULL, NULL, NULL),
(886, 206, 'A2', 999, 'Moderately', NULL, NULL, NULL),
(887, 206, 'A2', 999, 'Quite often', NULL, NULL, NULL),
(888, 206, 'A2', 999, 'Very often', NULL, NULL, NULL),
(889, 207, 'B2', 999, 'Not at all', NULL, NULL, NULL),
(890, 207, 'B2', 999, 'A little', NULL, NULL, NULL),
(891, 207, 'B2', 999, 'Moderately', NULL, NULL, NULL),
(892, 207, 'B2', 999, 'Quite often', NULL, NULL, NULL),
(893, 207, 'B2', 999, 'Very often', NULL, NULL, NULL),
(894, 207, 'C2', 999, 'Not at all', NULL, NULL, NULL),
(895, 207, 'C2', 999, 'A little', NULL, NULL, NULL),
(896, 207, 'C2', 999, 'Moderately', NULL, NULL, NULL),
(897, 207, 'C2', 999, 'Quite often', NULL, NULL, NULL),
(898, 207, 'C2', 999, 'Very often', NULL, NULL, NULL),
(899, 207, 'D2', 999, 'Not at all', NULL, NULL, NULL),
(900, 207, 'D2', 999, 'A little', NULL, NULL, NULL),
(901, 207, 'D2', 999, 'Moderately', NULL, NULL, NULL),
(902, 207, 'D2', 999, 'Quite often', NULL, NULL, NULL),
(903, 207, 'D2', 999, 'Very often', NULL, NULL, NULL),
(904, 207, 'A3', 999, 'Not at all', NULL, NULL, NULL),
(905, 207, 'A3', 999, 'A little', NULL, NULL, NULL),
(906, 207, 'A3', 999, 'Moderately', NULL, NULL, NULL),
(907, 207, 'A3', 999, 'Quite often', NULL, NULL, NULL),
(908, 207, 'A3', 999, 'Very often', NULL, NULL, NULL),
(909, 207, 'D3', 999, 'Not at all', NULL, NULL, NULL),
(910, 207, 'D3', 999, 'A little', NULL, NULL, NULL),
(911, 207, 'D3', 999, 'Moderately', NULL, NULL, NULL),
(912, 207, 'D3', 999, 'Quite often', NULL, NULL, NULL),
(913, 207, 'D3', 999, 'Very often', NULL, NULL, NULL),
(914, 208, 'B3', 999, 'Not at all', NULL, NULL, NULL),
(915, 208, 'B3', 999, 'A little', NULL, NULL, NULL),
(916, 208, 'B3', 999, 'Moderately', NULL, NULL, NULL),
(917, 208, 'B3', 999, 'Quite often', NULL, NULL, NULL),
(918, 208, 'B3', 999, 'Very often', NULL, NULL, NULL),
(919, 208, 'B1', 999, 'Not at all', NULL, NULL, NULL),
(920, 208, 'B1', 999, 'A little', NULL, NULL, NULL),
(921, 208, 'B1', 999, 'Moderately', NULL, NULL, NULL),
(922, 208, 'B1', 999, 'Quite often', NULL, NULL, NULL),
(923, 208, 'B1', 999, 'Very often', NULL, NULL, NULL),
(924, 208, 'C1', 999, 'Not at all', NULL, NULL, NULL),
(925, 208, 'C1', 999, 'A little', NULL, NULL, NULL),
(926, 208, 'C1', 999, 'Moderately', NULL, NULL, NULL),
(927, 208, 'C1', 999, 'Quite often', NULL, NULL, NULL),
(928, 208, 'C1', 999, 'Very often', NULL, NULL, NULL),
(929, 208, 'A1', 999, 'Not at all', NULL, NULL, NULL),
(930, 208, 'A1', 999, 'A little', NULL, NULL, NULL),
(931, 208, 'A1', 999, 'Moderately', NULL, NULL, NULL),
(932, 208, 'A1', 999, 'Quite often', NULL, NULL, NULL),
(933, 208, 'A1', 999, 'Very often', NULL, NULL, NULL),
(934, 208, 'B2', 999, 'Not at all', NULL, NULL, NULL),
(935, 208, 'B2', 999, 'A little', NULL, NULL, NULL),
(936, 208, 'B2', 999, 'Moderately', NULL, NULL, NULL),
(937, 208, 'B2', 999, 'Quite often', NULL, NULL, NULL),
(938, 208, 'B2', 999, 'Very often', NULL, NULL, NULL),
(939, 209, 'D1', 999, 'Not at all', NULL, NULL, NULL),
(940, 209, 'D1', 999, 'A little', NULL, NULL, NULL),
(941, 209, 'D1', 999, 'Moderately', NULL, NULL, NULL),
(942, 209, 'D1', 999, 'Quite often', NULL, NULL, NULL),
(943, 209, 'D1', 999, 'Very often', NULL, NULL, NULL),
(944, 209, 'A2', 999, 'Not at all', NULL, NULL, NULL),
(945, 209, 'A2', 999, 'A little', NULL, NULL, NULL),
(946, 209, 'A2', 999, 'Moderately', NULL, NULL, NULL),
(947, 209, 'A2', 999, 'Quite often', NULL, NULL, NULL),
(948, 209, 'A2', 999, 'Very often', NULL, NULL, NULL),
(949, 209, 'D2', 999, 'Not at all', NULL, NULL, NULL),
(950, 209, 'D2', 999, 'A little', NULL, NULL, NULL),
(951, 209, 'D2', 999, 'Moderately', NULL, NULL, NULL),
(952, 209, 'D2', 999, 'Quite often', NULL, NULL, NULL),
(953, 209, 'D2', 999, 'Very often', NULL, NULL, NULL),
(954, 209, 'A3', 999, 'Not at all', NULL, NULL, NULL),
(955, 209, 'A3', 999, 'A little', NULL, NULL, NULL),
(956, 209, 'A3', 999, 'Moderately', NULL, NULL, NULL),
(957, 209, 'A3', 999, 'Quite often', NULL, NULL, NULL),
(958, 209, 'A3', 999, 'Very often', NULL, NULL, NULL),
(959, 209, 'C2', 999, 'Not at all', NULL, NULL, NULL),
(960, 209, 'C2', 999, 'A little', NULL, NULL, NULL),
(961, 209, 'C2', 999, 'Moderately', NULL, NULL, NULL),
(962, 209, 'C2', 999, 'Quite often', NULL, NULL, NULL),
(963, 209, 'C2', 999, 'Very often', NULL, NULL, NULL),
(964, 210, 'B3', 999, 'Not at all', NULL, NULL, NULL),
(965, 210, 'B3', 999, 'A little', NULL, NULL, NULL),
(966, 210, 'B3', 999, 'Moderately', NULL, NULL, NULL),
(967, 210, 'B3', 999, 'Quite often', NULL, NULL, NULL),
(968, 210, 'B3', 999, 'Very often', NULL, NULL, NULL),
(969, 210, 'D3', 999, 'Not at all', NULL, NULL, NULL),
(970, 210, 'D3', 999, 'A little', NULL, NULL, NULL),
(971, 210, 'D3', 999, 'Moderately', NULL, NULL, NULL),
(972, 210, 'D3', 999, 'Quite often', NULL, NULL, NULL),
(973, 210, 'D3', 999, 'Very often', NULL, NULL, NULL),
(974, 210, 'C1', 999, 'Not at all', NULL, NULL, NULL),
(975, 210, 'C1', 999, 'A little', NULL, NULL, NULL),
(976, 210, 'C1', 999, 'Moderately', NULL, NULL, NULL),
(977, 210, 'C1', 999, 'Quite often', NULL, NULL, NULL),
(978, 210, 'C1', 999, 'Very often', NULL, NULL, NULL),
(979, 210, 'B1', 999, 'Not at all', NULL, NULL, NULL),
(980, 210, 'B1', 999, 'A little', NULL, NULL, NULL),
(981, 210, 'B1', 999, 'Moderately', NULL, NULL, NULL),
(982, 210, 'B1', 999, 'Quite often', NULL, NULL, NULL),
(983, 210, 'B1', 999, 'Very often', NULL, NULL, NULL),
(984, 210, 'A1', 999, 'Not at all', NULL, NULL, NULL),
(985, 210, 'A1', 999, 'A little', NULL, NULL, NULL),
(986, 210, 'A1', 999, 'Moderately', NULL, NULL, NULL),
(987, 210, 'A1', 999, 'Quite often', NULL, NULL, NULL),
(988, 210, 'A1', 999, 'Very often', NULL, NULL, NULL),
(989, 211, 'B2', 999, 'Not at all', NULL, NULL, NULL),
(990, 211, 'B2', 999, 'A little', NULL, NULL, NULL),
(991, 211, 'B2', 999, 'Moderately', NULL, NULL, NULL),
(992, 211, 'B2', 999, 'Quite often', NULL, NULL, NULL),
(993, 211, 'B2', 999, 'Very often', NULL, NULL, NULL),
(994, 211, 'A2', 999, 'Not at all', NULL, NULL, NULL),
(995, 211, 'A2', 999, 'A little', NULL, NULL, NULL),
(996, 211, 'A2', 999, 'Moderately', NULL, NULL, NULL),
(997, 211, 'A2', 999, 'Quite often', NULL, NULL, NULL),
(998, 211, 'A2', 999, 'Very often', NULL, NULL, NULL),
(999, 211, 'D1', 999, 'Not at all', NULL, NULL, NULL),
(1000, 211, 'D1', 999, 'A little', NULL, NULL, NULL),
(1001, 211, 'D1', 999, 'Moderately', NULL, NULL, NULL),
(1002, 211, 'D1', 999, 'Quite often', NULL, NULL, NULL),
(1003, 211, 'D1', 999, 'Very often', NULL, NULL, NULL),
(1004, 211, 'A3', 999, 'Not at all', NULL, NULL, NULL),
(1005, 211, 'A3', 999, 'A little', NULL, NULL, NULL),
(1006, 211, 'A3', 999, 'Moderately', NULL, NULL, NULL),
(1007, 211, 'A3', 999, 'Quite often', NULL, NULL, NULL),
(1008, 211, 'A3', 999, 'Very often', NULL, NULL, NULL),
(1009, 211, 'D2', 999, 'Not at all', NULL, NULL, NULL),
(1010, 211, 'D2', 999, 'A little', NULL, NULL, NULL),
(1011, 211, 'D2', 999, 'Moderately', NULL, NULL, NULL),
(1012, 211, 'D2', 999, 'Quite often', NULL, NULL, NULL),
(1013, 211, 'D2', 999, 'Very often', NULL, NULL, NULL),
(1014, 212, 'C2', 999, 'Not at all', NULL, NULL, NULL),
(1015, 212, 'C2', 999, 'A little', NULL, NULL, NULL),
(1016, 212, 'C2', 999, 'Moderately', NULL, NULL, NULL),
(1017, 212, 'C2', 999, 'Quite often', NULL, NULL, NULL),
(1018, 212, 'C2', 999, 'Very often', NULL, NULL, NULL),
(1019, 212, 'B3', 999, 'Not at all', NULL, NULL, NULL),
(1020, 212, 'B3', 999, 'A little', NULL, NULL, NULL),
(1021, 212, 'B3', 999, 'Moderately', NULL, NULL, NULL),
(1022, 212, 'B3', 999, 'Quite often', NULL, NULL, NULL),
(1023, 212, 'B3', 999, 'Very often', NULL, NULL, NULL),
(1024, 212, 'D3', 999, 'Not at all', NULL, NULL, NULL),
(1025, 212, 'D3', 999, 'A little', NULL, NULL, NULL),
(1026, 212, 'D3', 999, 'Moderately', NULL, NULL, NULL),
(1027, 212, 'D3', 999, 'Quite often', NULL, NULL, NULL),
(1028, 212, 'D3', 999, 'Very often', NULL, NULL, NULL),
(1029, 212, 'A1', 999, 'Not at all', NULL, NULL, NULL),
(1030, 212, 'A1', 999, 'A little', NULL, NULL, NULL),
(1031, 212, 'A1', 999, 'Moderately', NULL, NULL, NULL),
(1032, 212, 'A1', 999, 'Quite often', NULL, NULL, NULL),
(1033, 212, 'A1', 999, 'Very often', NULL, NULL, NULL),
(1034, 212, 'C1', 999, 'Not at all', NULL, NULL, NULL),
(1035, 212, 'C1', 999, 'A little', NULL, NULL, NULL),
(1036, 212, 'C1', 999, 'Moderately', NULL, NULL, NULL),
(1037, 212, 'C1', 999, 'Quite often', NULL, NULL, NULL),
(1038, 212, 'C1', 999, 'Very often', NULL, NULL, NULL),
(1039, 213, 'B1', 999, 'Not at all', NULL, NULL, NULL),
(1040, 213, 'B1', 999, 'A little', NULL, NULL, NULL),
(1041, 213, 'B1', 999, 'Moderately', NULL, NULL, NULL),
(1042, 213, 'B1', 999, 'Quite often', NULL, NULL, NULL),
(1043, 213, 'B1', 999, 'Very often', NULL, NULL, NULL),
(1044, 213, 'A2', 999, 'Not at all', NULL, NULL, NULL),
(1045, 213, 'A2', 999, 'A little', NULL, NULL, NULL),
(1046, 213, 'A2', 999, 'Moderately', NULL, NULL, NULL),
(1047, 213, 'A2', 999, 'Quite often', NULL, NULL, NULL),
(1048, 213, 'A2', 999, 'Very often', NULL, NULL, NULL),
(1049, 213, 'D1', 999, 'Not at all', NULL, NULL, NULL),
(1050, 213, 'D1', 999, 'A little', NULL, NULL, NULL),
(1051, 213, 'D1', 999, 'Moderately', NULL, NULL, NULL),
(1052, 213, 'D1', 999, 'Quite often', NULL, NULL, NULL),
(1053, 213, 'D1', 999, 'Very often', NULL, NULL, NULL),
(1054, 213, 'B2', 999, 'Not at all', NULL, NULL, NULL),
(1055, 213, 'B2', 999, 'A little', NULL, NULL, NULL),
(1056, 213, 'B2', 999, 'Moderately', NULL, NULL, NULL),
(1057, 213, 'B2', 999, 'Quite often', NULL, NULL, NULL),
(1058, 213, 'B2', 999, 'Very often', NULL, NULL, NULL),
(1059, 213, 'C2', 999, 'Not at all', NULL, NULL, NULL),
(1060, 213, 'C2', 999, 'A little', NULL, NULL, NULL),
(1061, 213, 'C2', 999, 'Moderately', NULL, NULL, NULL),
(1062, 213, 'C2', 999, 'Quite often', NULL, NULL, NULL),
(1063, 213, 'C2', 999, 'Very often', NULL, NULL, NULL),
(1064, 214, 'A3', 999, 'Not at all', NULL, NULL, NULL),
(1065, 214, 'A3', 999, 'A little', NULL, NULL, NULL),
(1066, 214, 'A3', 999, 'Moderately', NULL, NULL, NULL),
(1067, 214, 'A3', 999, 'Quite often', NULL, NULL, NULL),
(1068, 214, 'A3', 999, 'Very often', NULL, NULL, NULL),
(1069, 214, 'D2', 999, 'Not at all', NULL, NULL, NULL),
(1070, 214, 'D2', 999, 'A little', NULL, NULL, NULL),
(1071, 214, 'D2', 999, 'Moderately', NULL, NULL, NULL),
(1072, 214, 'D2', 999, 'Quite often', NULL, NULL, NULL),
(1073, 214, 'D2', 999, 'Very often', NULL, NULL, NULL),
(1074, 214, 'D3', 999, 'Not at all', NULL, NULL, NULL),
(1075, 214, 'D3', 999, 'A little', NULL, NULL, NULL),
(1076, 214, 'D3', 999, 'Moderately', NULL, NULL, NULL),
(1077, 214, 'D3', 999, 'Quite often', NULL, NULL, NULL),
(1078, 214, 'D3', 999, 'Very often', NULL, NULL, NULL),
(1079, 214, 'B3', 999, 'Not at all', NULL, NULL, NULL),
(1080, 214, 'B3', 999, 'A little', NULL, NULL, NULL),
(1081, 214, 'B3', 999, 'Moderately', NULL, NULL, NULL),
(1082, 214, 'B3', 999, 'Quite often', NULL, NULL, NULL),
(1083, 214, 'B3', 999, 'Very often', NULL, NULL, NULL),
(1084, 214, 'A1', 999, 'Not at all', NULL, NULL, NULL),
(1085, 214, 'A1', 999, 'A little', NULL, NULL, NULL),
(1086, 214, 'A1', 999, 'Moderately', NULL, NULL, NULL),
(1087, 214, 'A1', 999, 'Quite often', NULL, NULL, NULL),
(1088, 214, 'A1', 999, 'Very often', NULL, NULL, NULL),
(1089, 215, 'B1', 999, 'Not at all', NULL, NULL, NULL),
(1090, 215, 'B1', 999, 'A little', NULL, NULL, NULL),
(1091, 215, 'B1', 999, 'Moderately', NULL, NULL, NULL),
(1092, 215, 'B1', 999, 'Quite often', NULL, NULL, NULL),
(1093, 215, 'B1', 999, 'Very often', NULL, NULL, NULL),
(1094, 215, 'C1', 999, 'Not at all', NULL, NULL, NULL),
(1095, 215, 'C1', 999, 'A little', NULL, NULL, NULL),
(1096, 215, 'C1', 999, 'Moderately', NULL, NULL, NULL),
(1097, 215, 'C1', 999, 'Quite often', NULL, NULL, NULL),
(1098, 215, 'C1', 999, 'Very often', NULL, NULL, NULL),
(1099, 215, 'D1', 999, 'Not at all', NULL, NULL, NULL),
(1100, 215, 'D1', 999, 'A little', NULL, NULL, NULL),
(1101, 215, 'D1', 999, 'Moderately', NULL, NULL, NULL),
(1102, 215, 'D1', 999, 'Quite often', NULL, NULL, NULL),
(1103, 215, 'D1', 999, 'Very often', NULL, NULL, NULL),
(1104, 215, 'A2', 999, 'Not at all', NULL, NULL, NULL),
(1105, 215, 'A2', 999, 'A little', NULL, NULL, NULL),
(1106, 215, 'A2', 999, 'Moderately', NULL, NULL, NULL),
(1107, 215, 'A2', 999, 'Quite often', NULL, NULL, NULL),
(1108, 215, 'A2', 999, 'Very often', NULL, NULL, NULL),
(1109, 215, 'B2', 999, 'Not at all', NULL, NULL, NULL),
(1110, 215, 'B2', 999, 'A little', NULL, NULL, NULL),
(1111, 215, 'B2', 999, 'Moderately', NULL, NULL, NULL),
(1112, 215, 'B2', 999, 'Quite often', NULL, NULL, NULL),
(1113, 215, 'B2', 999, 'Very often', NULL, NULL, NULL),
(1114, 216, 'C2', 999, 'Not at all', NULL, NULL, NULL),
(1115, 216, 'C2', 999, 'A little', NULL, NULL, NULL),
(1116, 216, 'C2', 999, 'Moderately', NULL, NULL, NULL),
(1117, 216, 'C2', 999, 'Quite often', NULL, NULL, NULL),
(1118, 216, 'C2', 999, 'Very often', NULL, NULL, NULL),
(1119, 216, 'D2', 999, 'Not at all', NULL, NULL, NULL),
(1120, 216, 'D2', 999, 'A little', NULL, NULL, NULL),
(1121, 216, 'D2', 999, 'Moderately', NULL, NULL, NULL),
(1122, 216, 'D2', 999, 'Quite often', NULL, NULL, NULL),
(1123, 216, 'D2', 999, 'Very often', NULL, NULL, NULL),
(1124, 216, 'A3', 999, 'Not at all', NULL, NULL, NULL),
(1125, 216, 'A3', 999, 'A little', NULL, NULL, NULL),
(1126, 216, 'A3', 999, 'Moderately', NULL, NULL, NULL),
(1127, 216, 'A3', 999, 'Quite often', NULL, NULL, NULL),
(1128, 216, 'A3', 999, 'Very often', NULL, NULL, NULL),
(1129, 216, 'D3', 999, 'Not at all', NULL, NULL, NULL),
(1130, 216, 'D3', 999, 'A little', NULL, NULL, NULL),
(1131, 216, 'D3', 999, 'Moderately', NULL, NULL, NULL),
(1132, 216, 'D3', 999, 'Quite often', NULL, NULL, NULL),
(1133, 216, 'D3', 999, 'Very often', NULL, NULL, NULL),
(1134, 216, 'B3', 999, 'Not at all', NULL, NULL, NULL),
(1135, 216, 'B3', 999, 'A little', NULL, NULL, NULL),
(1136, 216, 'B3', 999, 'Moderately', NULL, NULL, NULL),
(1137, 216, 'B3', 999, 'Quite often', NULL, NULL, NULL),
(1138, 216, 'B3', 999, 'Very often', NULL, NULL, NULL),
(1139, 217, 'SA1', 999, 'I always spend a lot of time making plans', NULL, NULL, NULL),
(1140, 217, 'SA1', 999, 'I always spend a lot of time making plans', NULL, NULL, NULL),
(1141, 217, 'SA1', 999, 'I always spend a lot of time making plans', NULL, NULL, NULL),
(1142, 217, 'SA2', 999, 'I find change exciting', NULL, NULL, NULL),
(1143, 217, 'SA2', 999, 'I find change exciting', NULL, NULL, NULL),
(1144, 217, 'SA2', 999, 'I find change exciting', NULL, NULL, NULL),
(1145, 217, 'SA3', 999, 'I look for more work when there is little to do', NULL, NULL, NULL),
(1146, 217, 'SA3', 999, 'I look for more work when there is little to do', NULL, NULL, NULL),
(1147, 217, 'SA3', 999, 'I look for more work when there is little to do', NULL, NULL, NULL),
(1148, 218, 'SA4', 999, 'I am comfortable in situations where I have to make a decision', NULL, NULL, NULL),
(1149, 218, 'SA4', 999, 'I am comfortable in situations where I have to make a decision', NULL, NULL, NULL),
(1150, 218, 'SA4', 999, 'I am comfortable in situations where I have to make a decision', NULL, NULL, NULL),
(1151, 218, 'SA5', 999, 'I am quick to see when I need to help team members', NULL, NULL, NULL),
(1152, 218, 'SA5', 999, 'I am quick to see when I need to help team members', NULL, NULL, NULL),
(1153, 218, 'SA5', 999, 'I am quick to see when I need to help team members', NULL, NULL, NULL),
(1154, 218, 'SA6', 999, 'I get on well with all types of people', NULL, NULL, NULL),
(1155, 218, 'SA6', 999, 'I get on well with all types of people', NULL, NULL, NULL),
(1156, 218, 'SA6', 999, 'I get on well with all types of people', NULL, NULL, NULL),
(1157, 219, 'SA7', 999, 'I am usually one of the first people to find a problem', NULL, NULL, NULL),
(1158, 219, 'SA7', 999, 'I am usually one of the first people to find a problem', NULL, NULL, NULL),
(1159, 219, 'SA7', 999, 'I am usually one of the first people to find a problem', NULL, NULL, NULL),
(1160, 219, 'SA9', 999, 'I learn new computer programs quickly', NULL, NULL, NULL),
(1161, 219, 'SA9', 999, 'I learn new computer programs quickly', NULL, NULL, NULL),
(1162, 219, 'SA9', 999, 'I learn new computer programs quickly', NULL, NULL, NULL),
(1163, 219, 'SA8', 999, 'I often come up with new ways to do work', NULL, NULL, NULL),
(1164, 219, 'SA8', 999, 'I often come up with new ways to do work', NULL, NULL, NULL),
(1165, 219, 'SA8', 999, 'I often come up with new ways to do work', NULL, NULL, NULL),
(1166, 220, 'SA12', 999, 'When working on my tasks I take a lot of care with the details', NULL, NULL, NULL),
(1167, 220, 'SA12', 999, 'When working on my tasks I take a lot of care with the details', NULL, NULL, NULL),
(1168, 220, 'SA12', 999, 'When working on my tasks I take a lot of care with the details', NULL, NULL, NULL),
(1169, 220, 'SA11', 999, 'Before making decisions I try to get as much information as possible', NULL, NULL, NULL),
(1170, 220, 'SA11', 999, 'Before making decisions I try to get as much information as possible', NULL, NULL, NULL),
(1171, 220, 'SA11', 999, 'Before making decisions I try to get as much information as possible', NULL, NULL, NULL),
(1172, 220, 'SA10', 999, 'I always own up when I have made a mistake', NULL, NULL, NULL),
(1173, 220, 'SA10', 999, 'I always own up when I have made a mistake', NULL, NULL, NULL),
(1174, 220, 'SA10', 999, 'I always own up when I have made a mistake', NULL, NULL, NULL),
(1175, 221, 'SA14', 999, 'I always keep up-to-date with new technology', NULL, NULL, NULL),
(1176, 221, 'SA14', 999, 'I always keep up-to-date with new technology', NULL, NULL, NULL),
(1177, 221, 'SA14', 999, 'I always keep up-to-date with new technology', NULL, NULL, NULL),
(1178, 221, 'SA15', 999, 'I can easily see things from other people''s points of view', NULL, NULL, NULL),
(1179, 221, 'SA15', 999, 'I can easily see things from other people''s points of view', NULL, NULL, NULL),
(1180, 221, 'SA15', 999, 'I can easily see things from other people''s points of view', NULL, NULL, NULL),
(1181, 221, 'SA13', 999, 'I can usually stay positive in difficult situations', NULL, NULL, NULL),
(1182, 221, 'SA13', 999, 'I can usually stay positive in difficult situations', NULL, NULL, NULL),
(1183, 221, 'SA13', 999, 'I can usually stay positive in difficult situations', NULL, NULL, NULL),
(1184, 222, 'SA18', 999, 'I always finish what I start', NULL, NULL, NULL),
(1185, 222, 'SA18', 999, 'I always finish what I start', NULL, NULL, NULL),
(1186, 222, 'SA18', 999, 'I always finish what I start', NULL, NULL, NULL),
(1187, 222, 'SA17', 999, 'I am quick to think of ways to solve problems', NULL, NULL, NULL),
(1188, 222, 'SA17', 999, 'I am quick to think of ways to solve problems', NULL, NULL, NULL),
(1189, 222, 'SA17', 999, 'I am quick to think of ways to solve problems', NULL, NULL, NULL),
(1190, 222, 'SA16', 999, 'I am able to persuade people to do things my way', NULL, NULL, NULL),
(1191, 222, 'SA16', 999, 'I am able to persuade people to do things my way', NULL, NULL, NULL),
(1192, 222, 'SA16', 999, 'I am able to persuade people to do things my way', NULL, NULL, NULL),
(1193, 223, 'SA21', 999, 'I get used to new processes easily', NULL, NULL, NULL),
(1194, 223, 'SA21', 999, 'I get used to new processes easily', NULL, NULL, NULL),
(1195, 223, 'SA21', 999, 'I get used to new processes easily', NULL, NULL, NULL),
(1196, 223, 'SA20', 999, 'I often find problems that other people have missed', NULL, NULL, NULL),
(1197, 223, 'SA20', 999, 'I often find problems that other people have missed', NULL, NULL, NULL),
(1198, 223, 'SA20', 999, 'I often find problems that other people have missed', NULL, NULL, NULL),
(1199, 223, 'SA19', 999, 'I help others to find different ways to solve their disagreements', NULL, NULL, NULL),
(1200, 223, 'SA19', 999, 'I help others to find different ways to solve their disagreements', NULL, NULL, NULL),
(1201, 223, 'SA19', 999, 'I help others to find different ways to solve their disagreements', NULL, NULL, NULL),
(1202, 224, 'SA22', 999, 'I always feel for people who are in difficulty', NULL, NULL, NULL),
(1203, 224, 'SA22', 999, 'I always feel for people who are in difficulty', NULL, NULL, NULL),
(1204, 224, 'SA22', 999, 'I always feel for people who are in difficulty', NULL, NULL, NULL),
(1205, 224, 'SA24', 999, 'I am the kind of person who will not stop until I reach my goal', NULL, NULL, NULL),
(1206, 224, 'SA24', 999, 'I am the kind of person who will not stop until I reach my goal', NULL, NULL, NULL),
(1207, 224, 'SA24', 999, 'I am the kind of person who will not stop until I reach my goal', NULL, NULL, NULL),
(1208, 224, 'SA23', 999, 'I know exactly which tasks to do first when I am short of time', NULL, NULL, NULL),
(1209, 224, 'SA23', 999, 'I know exactly which tasks to do first when I am short of time', NULL, NULL, NULL),
(1210, 224, 'SA23', 999, 'I know exactly which tasks to do first when I am short of time', NULL, NULL, NULL),
(1211, 225, 'SA26', 999, 'It is easy for me to write things that others will find interesting to read', NULL, NULL, NULL),
(1212, 225, 'SA26', 999, 'It is easy for me to write things that others will find interesting to read', NULL, NULL, NULL),
(1213, 225, 'SA26', 999, 'It is easy for me to write things that others will find interesting to read', NULL, NULL, NULL),
(1214, 225, 'SA25', 999, 'I am usually the one who organises team activities', NULL, NULL, NULL),
(1215, 225, 'SA25', 999, 'I am usually the one who organises team activities', NULL, NULL, NULL),
(1216, 225, 'SA25', 999, 'I am usually the one who organises team activities', NULL, NULL, NULL),
(1217, 225, 'SA27', 999, 'I find it easy to learn different computer programs', NULL, NULL, NULL),
(1218, 225, 'SA27', 999, 'I find it easy to learn different computer programs', NULL, NULL, NULL),
(1219, 225, 'SA27', 999, 'I find it easy to learn different computer programs', NULL, NULL, NULL),
(1220, 226, 'SA29', 999, 'I can still cope well with my work when under pressure', NULL, NULL, NULL),
(1221, 226, 'SA29', 999, 'I can still cope well with my work when under pressure', NULL, NULL, NULL),
(1222, 226, 'SA29', 999, 'I can still cope well with my work when under pressure', NULL, NULL, NULL),
(1223, 226, 'SA28', 999, 'I do not avoid situations where I have to be responsible', NULL, NULL, NULL),
(1224, 226, 'SA28', 999, 'I do not avoid situations where I have to be responsible', NULL, NULL, NULL),
(1225, 226, 'SA28', 999, 'I do not avoid situations where I have to be responsible', NULL, NULL, NULL),
(1226, 226, 'SA30', 999, 'I offer to help and support many tasks', NULL, NULL, NULL),
(1227, 226, 'SA30', 999, 'I offer to help and support many tasks', NULL, NULL, NULL),
(1228, 226, 'SA30', 999, 'I offer to help and support many tasks', NULL, NULL, NULL),
(1229, 227, 'SA31', 999, 'Others say that I am a good listener', NULL, NULL, NULL),
(1230, 227, 'SA31', 999, 'Others say that I am a good listener', NULL, NULL, NULL),
(1231, 227, 'SA31', 999, 'Others say that I am a good listener', NULL, NULL, NULL),
(1232, 227, 'SA33', 999, 'I push myself to be the best in my team', NULL, NULL, NULL),
(1233, 227, 'SA33', 999, 'I push myself to be the best in my team', NULL, NULL, NULL),
(1234, 227, 'SA33', 999, 'I push myself to be the best in my team', NULL, NULL, NULL),
(1235, 227, 'SA32', 999, 'I do not think about my own, or other people''s feelings and emotions when making decisions', NULL, NULL, NULL),
(1236, 227, 'SA32', 999, 'I do not think about my own, or other people''s feelings and emotions when making decisions', NULL, NULL, NULL),
(1237, 227, 'SA32', 999, 'I do not think about my own, or other people''s feelings and emotions when making decisions', NULL, NULL, NULL),
(1238, 228, 'SA35', 999, 'I often surprise others with new and creative ideas', NULL, NULL, NULL),
(1239, 228, 'SA35', 999, 'I often surprise others with new and creative ideas', NULL, NULL, NULL),
(1240, 228, 'SA35', 999, 'I often surprise others with new and creative ideas', NULL, NULL, NULL),
(1241, 228, 'SA34', 999, 'I always share and teach others skills that I am good at', NULL, NULL, NULL),
(1242, 228, 'SA34', 999, 'I always share and teach others skills that I am good at', NULL, NULL, NULL),
(1243, 228, 'SA34', 999, 'I always share and teach others skills that I am good at', NULL, NULL, NULL),
(1244, 228, 'SA36', 999, 'I make plans using lots of detail', NULL, NULL, NULL),
(1245, 228, 'SA36', 999, 'I make plans using lots of detail', NULL, NULL, NULL),
(1246, 228, 'SA36', 999, 'I make plans using lots of detail', NULL, NULL, NULL),
(1247, 229, 'SA37', 999, 'I am not afraid of telling others what to do', NULL, NULL, NULL),
(1248, 229, 'SA37', 999, 'I am not afraid of telling others what to do', NULL, NULL, NULL),
(1249, 229, 'SA37', 999, 'I am not afraid of telling others what to do', NULL, NULL, NULL),
(1250, 229, 'SA38', 999, 'I like to work with new ideas', NULL, NULL, NULL),
(1251, 229, 'SA38', 999, 'I like to work with new ideas', NULL, NULL, NULL),
(1252, 229, 'SA38', 999, 'I like to work with new ideas', NULL, NULL, NULL),
(1253, 229, 'SA39', 999, 'Even under pressure I try to do difficult tasks calmly', NULL, NULL, NULL),
(1254, 229, 'SA39', 999, 'Even under pressure I try to do difficult tasks calmly', NULL, NULL, NULL),
(1255, 229, 'SA39', 999, 'Even under pressure I try to do difficult tasks calmly', NULL, NULL, NULL),
(1256, 230, 'SA41', 999, 'I am able to present confidently even on topics where I am less experienced', NULL, NULL, NULL),
(1257, 230, 'SA41', 999, 'I am able to present confidently even on topics where I am less experienced', NULL, NULL, NULL),
(1258, 230, 'SA41', 999, 'I am able to present confidently even on topics where I am less experienced', NULL, NULL, NULL),
(1259, 230, 'SA40', 999, 'I am very good at comforting others', NULL, NULL, NULL),
(1260, 230, 'SA40', 999, 'I am very good at comforting others', NULL, NULL, NULL),
(1261, 230, 'SA40', 999, 'I am very good at comforting others', NULL, NULL, NULL),
(1262, 230, 'SA42', 999, 'When looking into a problem, I always start thinking of solutions', NULL, NULL, NULL),
(1263, 230, 'SA42', 999, 'When looking into a problem, I always start thinking of solutions', NULL, NULL, NULL),
(1264, 230, 'SA42', 999, 'When looking into a problem, I always start thinking of solutions', NULL, NULL, NULL),
(1265, 231, 'SA45', 999, 'I find it very easy to use skills that I have learnt recently', NULL, NULL, NULL),
(1266, 231, 'SA45', 999, 'I find it very easy to use skills that I have learnt recently', NULL, NULL, NULL),
(1267, 231, 'SA45', 999, 'I find it very easy to use skills that I have learnt recently', NULL, NULL, NULL),
(1268, 231, 'SA44', 999, 'I am always looking for something to keep me busy', NULL, NULL, NULL),
(1269, 231, 'SA44', 999, 'I am always looking for something to keep me busy', NULL, NULL, NULL),
(1270, 231, 'SA44', 999, 'I am always looking for something to keep me busy', NULL, NULL, NULL),
(1271, 231, 'SA43', 999, 'I always plan ahead when doing anything', NULL, NULL, NULL),
(1272, 231, 'SA43', 999, 'I always plan ahead when doing anything', NULL, NULL, NULL),
(1273, 231, 'SA43', 999, 'I always plan ahead when doing anything', NULL, NULL, NULL),
(1274, 232, 'SA46', 999, 'I quickly spot the weaknesses in the way things are done', NULL, NULL, NULL),
(1275, 232, 'SA46', 999, 'I quickly spot the weaknesses in the way things are done', NULL, NULL, NULL),
(1276, 232, 'SA46', 999, 'I quickly spot the weaknesses in the way things are done', NULL, NULL, NULL),
(1277, 232, 'SA47', 999, 'I am usually able to come up with lots of possible solutions to a problem', NULL, NULL, NULL),
(1278, 232, 'SA47', 999, 'I am usually able to come up with lots of possible solutions to a problem', NULL, NULL, NULL),
(1279, 232, 'SA47', 999, 'I am usually able to come up with lots of possible solutions to a problem', NULL, NULL, NULL),
(1280, 232, 'SA48', 999, 'I would rather work for longer than to do poor quality work', NULL, NULL, NULL),
(1281, 232, 'SA48', 999, 'I would rather work for longer than to do poor quality work', NULL, NULL, NULL),
(1282, 232, 'SA48', 999, 'I would rather work for longer than to do poor quality work', NULL, NULL, NULL),
(1283, 233, 'SA49', 999, 'I am usually the one to take charge of others', NULL, NULL, NULL),
(1284, 233, 'SA49', 999, 'I am usually the one to take charge of others', NULL, NULL, NULL),
(1285, 233, 'SA49', 999, 'I am usually the one to take charge of others', NULL, NULL, NULL),
(1286, 233, 'SA50', 999, 'I always give others an opportunity to get to know me', NULL, NULL, NULL),
(1287, 233, 'SA50', 999, 'I always give others an opportunity to get to know me', NULL, NULL, NULL),
(1288, 233, 'SA50', 999, 'I always give others an opportunity to get to know me', NULL, NULL, NULL),
(1289, 233, 'SA51', 999, 'I am very logical when making decisions', NULL, NULL, NULL),
(1290, 233, 'SA51', 999, 'I am very logical when making decisions', NULL, NULL, NULL),
(1291, 233, 'SA51', 999, 'I am very logical when making decisions', NULL, NULL, NULL),
(1292, 234, 'SA54', 999, 'I usually find something positive in a situation, however bad it is', NULL, NULL, NULL),
(1293, 234, 'SA54', 999, 'I usually find something positive in a situation, however bad it is', NULL, NULL, NULL),
(1294, 234, 'SA54', 999, 'I usually find something positive in a situation, however bad it is', NULL, NULL, NULL),
(1295, 234, 'SA53', 999, 'I regularly check my work progress against deadlines', NULL, NULL, NULL),
(1296, 234, 'SA53', 999, 'I regularly check my work progress against deadlines', NULL, NULL, NULL),
(1297, 234, 'SA53', 999, 'I regularly check my work progress against deadlines', NULL, NULL, NULL),
(1298, 234, 'SA52', 999, 'I go out of my way to help my team', NULL, NULL, NULL),
(1299, 234, 'SA52', 999, 'I go out of my way to help my team', NULL, NULL, NULL),
(1300, 234, 'SA52', 999, 'I go out of my way to help my team', NULL, NULL, NULL),
(1301, 235, 'SA57', 999, 'I learn new information quickly', NULL, NULL, NULL),
(1302, 235, 'SA57', 999, 'I learn new information quickly', NULL, NULL, NULL),
(1303, 235, 'SA57', 999, 'I learn new information quickly', NULL, NULL, NULL),
(1304, 235, 'SA56', 999, 'I am usually in a positive mood', NULL, NULL, NULL),
(1305, 235, 'SA56', 999, 'I am usually in a positive mood', NULL, NULL, NULL),
(1306, 235, 'SA56', 999, 'I am usually in a positive mood', NULL, NULL, NULL),
(1307, 235, 'SA55', 999, 'When faced with new problems, I can always provide solutions quickly', NULL, NULL, NULL),
(1308, 235, 'SA55', 999, 'When faced with new problems, I can always provide solutions quickly', NULL, NULL, NULL),
(1309, 235, 'SA55', 999, 'When faced with new problems, I can always provide solutions quickly', NULL, NULL, NULL),
(1310, 236, 'SA59', 999, 'I am able to explain information in an easy to understand way', NULL, NULL, NULL),
(1311, 236, 'SA59', 999, 'I am able to explain information in an easy to understand way', NULL, NULL, NULL),
(1312, 236, 'SA59', 999, 'I am able to explain information in an easy to understand way', NULL, NULL, NULL),
(1313, 236, 'SA58', 999, 'I guide the activities of others', NULL, NULL, NULL),
(1314, 236, 'SA58', 999, 'I guide the activities of others', NULL, NULL, NULL),
(1315, 236, 'SA58', 999, 'I guide the activities of others', NULL, NULL, NULL),
(1316, 236, 'SA60', 999, 'I always have a can-do attitude to work', NULL, NULL, NULL),
(1317, 236, 'SA60', 999, 'I always have a can-do attitude to work', NULL, NULL, NULL),
(1318, 236, 'SA60', 999, 'I always have a can-do attitude to work', NULL, NULL, NULL),
(1319, 237, 'SA64', 999, 'I often organise what other people are doing', NULL, NULL, NULL),
(1320, 237, 'SA64', 999, 'I often organise what other people are doing', NULL, NULL, NULL),
(1321, 237, 'SA64', 999, 'I often organise what other people are doing', NULL, NULL, NULL),
(1322, 237, 'SA65', 999, 'My suggestions for problem-solving are always practical', NULL, NULL, NULL),
(1323, 237, 'SA65', 999, 'My suggestions for problem-solving are always practical', NULL, NULL, NULL),
(1324, 237, 'SA65', 999, 'My suggestions for problem-solving are always practical', NULL, NULL, NULL),
(1325, 237, 'SA66', 999, 'I always work to a plan', NULL, NULL, NULL),
(1326, 237, 'SA66', 999, 'I always work to a plan', NULL, NULL, NULL),
(1327, 237, 'SA66', 999, 'I always work to a plan', NULL, NULL, NULL),
(1328, 238, 'SA61', 999, 'I get on well with most people', NULL, NULL, NULL),
(1329, 238, 'SA61', 999, 'I get on well with most people', NULL, NULL, NULL),
(1330, 238, 'SA61', 999, 'I get on well with most people', NULL, NULL, NULL),
(1331, 238, 'SA62', 999, 'I base my decisions on real evidence', NULL, NULL, NULL),
(1332, 238, 'SA62', 999, 'I base my decisions on real evidence', NULL, NULL, NULL),
(1333, 238, 'SA62', 999, 'I base my decisions on real evidence', NULL, NULL, NULL),
(1334, 238, 'SA63', 999, 'I quickly remember new information', NULL, NULL, NULL),
(1335, 238, 'SA63', 999, 'I quickly remember new information', NULL, NULL, NULL),
(1336, 238, 'SA63', 999, 'I quickly remember new information', NULL, NULL, NULL),
(1337, 239, 'SA67', 999, 'I often try to understand how other people would think in a situation', NULL, NULL, NULL),
(1338, 239, 'SA67', 999, 'I often try to understand how other people would think in a situation', NULL, NULL, NULL),
(1339, 239, 'SA67', 999, 'I often try to understand how other people would think in a situation', NULL, NULL, NULL),
(1340, 239, 'SA68', 999, 'Persuading people always makes me feel good', NULL, NULL, NULL),
(1341, 239, 'SA68', 999, 'Persuading people always makes me feel good', NULL, NULL, NULL),
(1342, 239, 'SA68', 999, 'Persuading people always makes me feel good', NULL, NULL, NULL),
(1343, 239, 'SA69', 999, 'I only feel satisfied when I am busy working', NULL, NULL, NULL),
(1344, 239, 'SA69', 999, 'I only feel satisfied when I am busy working', NULL, NULL, NULL),
(1345, 239, 'SA69', 999, 'I only feel satisfied when I am busy working', NULL, NULL, NULL),
(1346, 240, 'SA72', 999, 'I usually enjoy changes at work', NULL, NULL, NULL),
(1347, 240, 'SA72', 999, 'I usually enjoy changes at work', NULL, NULL, NULL),
(1348, 240, 'SA72', 999, 'I usually enjoy changes at work', NULL, NULL, NULL),
(1349, 240, 'SA70', 999, 'I do not relax until the work is done', NULL, NULL, NULL),
(1350, 240, 'SA70', 999, 'I do not relax until the work is done', NULL, NULL, NULL),
(1351, 240, 'SA70', 999, 'I do not relax until the work is done', NULL, NULL, NULL),
(1352, 240, 'SA71', 999, 'I often find new and creative ways of doing things', NULL, NULL, NULL),
(1353, 240, 'SA71', 999, 'I often find new and creative ways of doing things', NULL, NULL, NULL),
(1354, 240, 'SA71', 999, 'I often find new and creative ways of doing things', NULL, NULL, NULL),
(1355, 241, 'SA73', 999, 'I know how to give my ideas and listen to other ideas when coming to agreements', NULL, NULL, NULL),
(1356, 241, 'SA73', 999, 'I know how to give my ideas and listen to other ideas when coming to agreements', NULL, NULL, NULL),
(1357, 241, 'SA73', 999, 'I know how to give my ideas and listen to other ideas when coming to agreements', NULL, NULL, NULL),
(1358, 241, 'SA75', 999, 'I quickly identify important information when using the internet', NULL, NULL, NULL),
(1359, 241, 'SA75', 999, 'I quickly identify important information when using the internet', NULL, NULL, NULL),
(1360, 241, 'SA75', 999, 'I quickly identify important information when using the internet', NULL, NULL, NULL),
(1361, 241, 'SA74', 999, 'I do more work when I have a plan to work from', NULL, NULL, NULL),
(1362, 241, 'SA74', 999, 'I do more work when I have a plan to work from', NULL, NULL, NULL),
(1363, 241, 'SA74', 999, 'I do more work when I have a plan to work from', NULL, NULL, NULL),
(1364, 242, 'SA78', 999, 'Before making important decisions I look into things in detail', NULL, NULL, NULL),
(1365, 242, 'SA78', 999, 'Before making important decisions I look into things in detail', NULL, NULL, NULL),
(1366, 242, 'SA78', 999, 'Before making important decisions I look into things in detail', NULL, NULL, NULL),
(1367, 242, 'SA76', 999, 'I put a lot of energy into my work', NULL, NULL, NULL),
(1368, 242, 'SA76', 999, 'I put a lot of energy into my work', NULL, NULL, NULL),
(1369, 242, 'SA76', 999, 'I put a lot of energy into my work', NULL, NULL, NULL),
(1370, 242, 'SA77', 999, 'I very quickly know how I should behave with different people', NULL, NULL, NULL),
(1371, 242, 'SA77', 999, 'I very quickly know how I should behave with different people', NULL, NULL, NULL),
(1372, 242, 'SA77', 999, 'I very quickly know how I should behave with different people', NULL, NULL, NULL),
(1373, 243, 'SA80', 999, 'I am good at tasks that need attention to the details', NULL, NULL, NULL),
(1374, 243, 'SA80', 999, 'I am good at tasks that need attention to the details', NULL, NULL, NULL),
(1375, 243, 'SA80', 999, 'I am good at tasks that need attention to the details', NULL, NULL, NULL),
(1376, 243, 'SA79', 999, 'I find it easy to understand the thoughts and feelings of others', NULL, NULL, NULL),
(1377, 243, 'SA79', 999, 'I find it easy to understand the thoughts and feelings of others', NULL, NULL, NULL),
(1378, 243, 'SA79', 999, 'I find it easy to understand the thoughts and feelings of others', NULL, NULL, NULL),
(1379, 243, 'SA81', 999, 'I remain calm when work pressure increases', NULL, NULL, NULL),
(1380, 243, 'SA81', 999, 'I remain calm when work pressure increases', NULL, NULL, NULL),
(1381, 243, 'SA81', 999, 'I remain calm when work pressure increases', NULL, NULL, NULL),
(1382, 244, 'SA82', 999, 'I feel comfortable taking a leading role in a group', NULL, NULL, NULL),
(1383, 244, 'SA82', 999, 'I feel comfortable taking a leading role in a group', NULL, NULL, NULL),
(1384, 244, 'SA82', 999, 'I feel comfortable taking a leading role in a group', NULL, NULL, NULL),
(1385, 244, 'SA83', 999, 'I am confident when speaking in front of an audience', NULL, NULL, NULL),
(1386, 244, 'SA83', 999, 'I am confident when speaking in front of an audience', NULL, NULL, NULL),
(1387, 244, 'SA83', 999, 'I am confident when speaking in front of an audience', NULL, NULL, NULL),
(1388, 244, 'SA84', 999, 'I learn new computer skills quickly', NULL, NULL, NULL),
(1389, 244, 'SA84', 999, 'I learn new computer skills quickly', NULL, NULL, NULL),
(1390, 244, 'SA84', 999, 'I learn new computer skills quickly', NULL, NULL, NULL),
(1391, 245, 'A', 999, 'A', NULL, NULL, 'S01-A.gif'),
(1392, 245, 'B', 999, 'B', NULL, NULL, 'S01-B.gif'),
(1393, 245, 'C', 999, 'C', NULL, NULL, 'S01-C.gif'),
(1394, 245, 'D', 999, 'D', NULL, NULL, 'S01-D.gif'),
(1395, 245, 'E', 999, 'E', NULL, NULL, 'S01-E.gif'),
(1396, 246, 'A', 999, 'A', NULL, NULL, 'S02-A.gif'),
(1397, 246, 'B', 999, 'B', NULL, NULL, 'S02-B.gif'),
(1398, 246, 'C', 999, 'C', NULL, NULL, 'S02-C.gif'),
(1399, 246, 'D', 999, 'D', NULL, NULL, 'S02-D.gif'),
(1400, 246, 'E', 999, 'E', NULL, NULL, 'S02-E.gif'),
(1401, 247, 'A', 999, 'A', NULL, NULL, 'S03-A.gif'),
(1402, 247, 'B', 999, 'B', NULL, NULL, 'S03-B.gif'),
(1403, 247, 'C', 999, 'C', NULL, NULL, 'S03-C.gif'),
(1404, 247, 'D', 999, 'D', NULL, NULL, 'S03-D.gif'),
(1405, 247, 'E', 999, 'E', NULL, NULL, 'S03-E.gif'),
(1406, 248, 'A', 999, 'A', NULL, NULL, 'S04-A.gif'),
(1407, 248, 'B', 999, 'B', NULL, NULL, 'S04-B.gif'),
(1408, 248, 'C', 999, 'C', NULL, NULL, 'S04-C.gif'),
(1409, 248, 'D', 999, 'D', NULL, NULL, 'S04-D.gif'),
(1410, 248, 'E', 999, 'E', NULL, NULL, 'S04-E.gif'),
(1411, 249, 'A', 999, 'A', NULL, NULL, 'S05-A.gif'),
(1412, 249, 'B', 999, 'B', NULL, NULL, 'S05-B.gif'),
(1413, 249, 'C', 999, 'C', NULL, NULL, 'S05-C.gif'),
(1414, 249, 'D', 999, 'D', NULL, NULL, 'S05-D.gif'),
(1415, 249, 'E', 999, 'E', NULL, NULL, 'S05-E.gif'),
(1416, 250, 'A', 999, 'A', NULL, NULL, 'S06-A.gif'),
(1417, 250, 'B', 999, 'B', NULL, NULL, 'S06-B.gif'),
(1418, 250, 'C', 999, 'C', NULL, NULL, 'S06-C.gif'),
(1419, 250, 'D', 999, 'D', NULL, NULL, 'S06-D.gif'),
(1420, 250, 'E', 999, 'E', NULL, NULL, 'S06-E.gif'),
(1421, 251, 'A', 999, 'A', NULL, NULL, 'S07-A.gif'),
(1422, 251, 'B', 999, 'B', NULL, NULL, 'S07-B.gif'),
(1423, 251, 'C', 999, 'C', NULL, NULL, 'S07-C.gif'),
(1424, 251, 'D', 999, 'D', NULL, NULL, 'S07-D.gif'),
(1425, 251, 'E', 999, 'E', NULL, NULL, 'S07-E.gif'),
(1426, 252, 'A', 999, 'A', NULL, NULL, 'S08-A.gif'),
(1427, 252, 'B', 999, 'B', NULL, NULL, 'S08-B.gif'),
(1428, 252, 'C', 999, 'C', NULL, NULL, 'S08-C.gif'),
(1429, 252, 'D', 999, 'D', NULL, NULL, 'S08-D.gif'),
(1430, 252, 'E', 999, 'E', NULL, NULL, 'S08-E.gif'),
(1431, 253, 'A', 999, 'A', NULL, NULL, 'S09-A.gif'),
(1432, 253, 'B', 999, 'B', NULL, NULL, 'S09-B.gif'),
(1433, 253, 'C', 999, 'C', NULL, NULL, 'S09-C.gif'),
(1434, 253, 'D', 999, 'D', NULL, NULL, 'S09-D.gif'),
(1435, 253, 'E', 999, 'E', NULL, NULL, 'S09-E.gif'),
(1436, 254, 'A', 999, 'A', NULL, NULL, 'S10-A.gif'),
(1437, 254, 'B', 999, 'B', NULL, NULL, 'S10-B.gif'),
(1438, 254, 'C', 999, 'C', NULL, NULL, 'S10-C.gif'),
(1439, 254, 'D', 999, 'D', NULL, NULL, 'S10-D.gif'),
(1440, 254, 'E', 999, 'E', NULL, NULL, 'S10-E.gif'),
(1441, 255, 'A', 999, 'A', NULL, NULL, 'S11-A.gif'),
(1442, 255, 'B', 999, 'B', NULL, NULL, 'S11-B.gif'),
(1443, 255, 'C', 999, 'C', NULL, NULL, 'S11-C.gif'),
(1444, 255, 'D', 999, 'D', NULL, NULL, 'S11-D.gif'),
(1445, 255, 'E', 999, 'E', NULL, NULL, 'S11-E.gif'),
(1446, 256, 'A', 999, 'A', NULL, NULL, 'S12-A.gif'),
(1447, 256, 'B', 999, 'B', NULL, NULL, 'S12-B.gif'),
(1448, 256, 'C', 999, 'C', NULL, NULL, 'S12-C.gif'),
(1449, 256, 'D', 999, 'D', NULL, NULL, 'S12-D.gif'),
(1450, 256, 'E', 999, 'E', NULL, NULL, 'S12-E.gif'),
(1451, 257, 'A', 999, 'A', NULL, NULL, 'S13-A.gif'),
(1452, 257, 'B', 999, 'B', NULL, NULL, 'S13-B.gif'),
(1453, 257, 'C', 999, 'C', NULL, NULL, 'S13-C.gif'),
(1454, 257, 'D', 999, 'D', NULL, NULL, 'S13-D.gif'),
(1455, 257, 'E', 999, 'E', NULL, NULL, 'S13-E.gif'),
(1456, 258, 'A', 999, 'A', NULL, NULL, 'S14-A.gif'),
(1457, 258, 'B', 999, 'B', NULL, NULL, 'S14-B.gif'),
(1458, 258, 'C', 999, 'C', NULL, NULL, 'S14-C.gif'),
(1459, 258, 'D', 999, 'D', NULL, NULL, 'S14-D.gif'),
(1460, 258, 'E', 999, 'E', NULL, NULL, 'S14-E.gif'),
(1461, 259, 'A', 999, 'True', NULL, NULL, NULL),
(1462, 259, 'B', 999, 'False', NULL, NULL, NULL),
(1463, 259, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1464, 260, 'A', 999, 'True', NULL, NULL, NULL),
(1465, 260, 'B', 999, 'False', NULL, NULL, NULL),
(1466, 260, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1467, 261, 'A', 999, 'True', NULL, NULL, NULL),
(1468, 261, 'B', 999, 'False', NULL, NULL, NULL),
(1469, 261, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1470, 262, 'A', 999, 'True', NULL, NULL, NULL),
(1471, 262, 'B', 999, 'False', NULL, NULL, NULL),
(1472, 262, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1473, 263, 'A', 999, 'True', NULL, NULL, NULL),
(1474, 263, 'B', 999, 'False', NULL, NULL, NULL),
(1475, 263, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1476, 264, 'A', 999, 'True', NULL, NULL, NULL),
(1477, 264, 'B', 999, 'False', NULL, NULL, NULL),
(1478, 264, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1479, 265, 'A', 999, 'True', NULL, NULL, NULL),
(1480, 265, 'B', 999, 'False', NULL, NULL, NULL),
(1481, 265, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1482, 266, 'A', 999, 'True', NULL, NULL, NULL),
(1483, 266, 'B', 999, 'False', NULL, NULL, NULL),
(1484, 266, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1485, 267, 'A', 999, 'True', NULL, NULL, NULL),
(1486, 267, 'B', 999, 'False', NULL, NULL, NULL),
(1487, 267, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1488, 268, 'A', 999, 'True', NULL, NULL, NULL),
(1489, 268, 'B', 999, 'False', NULL, NULL, NULL),
(1490, 268, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1491, 269, 'A', 999, 'True', NULL, NULL, NULL),
(1492, 269, 'B', 999, 'False', NULL, NULL, NULL),
(1493, 269, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1494, 270, 'A', 999, 'True', NULL, NULL, NULL),
(1495, 270, 'B', 999, 'False', NULL, NULL, NULL),
(1496, 270, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1497, 271, 'A', 999, 'True', NULL, NULL, NULL),
(1498, 271, 'B', 999, 'False', NULL, NULL, NULL),
(1499, 271, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1500, 272, 'A', 999, 'True', NULL, NULL, NULL),
(1501, 272, 'B', 999, 'False', NULL, NULL, NULL),
(1502, 272, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1503, 273, 'A', 999, 'True', NULL, NULL, NULL),
(1504, 273, 'B', 999, 'False', NULL, NULL, NULL),
(1505, 273, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1506, 274, 'A', 999, 'True', NULL, NULL, NULL),
(1507, 274, 'B', 999, 'False', NULL, NULL, NULL),
(1508, 274, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1509, 275, 'A', 999, 'True', NULL, NULL, NULL),
(1510, 275, 'B', 999, 'False', NULL, NULL, NULL),
(1511, 275, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1512, 276, 'A', 999, 'True', NULL, NULL, NULL),
(1513, 276, 'B', 999, 'False', NULL, NULL, NULL),
(1514, 276, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1515, 277, 'A', 999, 'True', NULL, NULL, NULL),
(1516, 277, 'B', 999, 'False', NULL, NULL, NULL),
(1517, 277, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1518, 278, 'A', 999, 'True', NULL, NULL, NULL),
(1519, 278, 'B', 999, 'False', NULL, NULL, NULL),
(1520, 278, 'C', 999, 'Can''t say', NULL, NULL, NULL),
(1521, 279, 'A', 999, 'A', NULL, NULL, 'AB01-A.gif'),
(1522, 279, 'B', 999, 'B', NULL, NULL, 'AB01-B.gif'),
(1523, 279, 'C', 999, 'C', NULL, NULL, 'AB01-C.gif'),
(1524, 279, 'D', 999, 'D', NULL, NULL, 'AB01-D.gif'),
(1525, 279, 'E', 999, 'E', NULL, NULL, 'AB01-E.gif'),
(1526, 280, 'A', 999, 'A', NULL, NULL, 'AB02-A.gif'),
(1527, 280, 'B', 999, 'B', NULL, NULL, 'AB02-B.gif'),
(1528, 280, 'C', 999, 'C', NULL, NULL, 'AB02-C.gif'),
(1529, 280, 'D', 999, 'D', NULL, NULL, 'AB02-D.gif'),
(1530, 280, 'E', 999, 'E', NULL, NULL, 'AB02-E.gif'),
(1531, 281, 'A', 999, 'A', NULL, NULL, 'AB03-A.gif'),
(1532, 281, 'B', 999, 'B', NULL, NULL, 'AB03-B.gif'),
(1533, 281, 'C', 999, 'C', NULL, NULL, 'AB03-C.gif'),
(1534, 281, 'D', 999, 'D', NULL, NULL, 'AB03-D.gif'),
(1535, 281, 'E', 999, 'E', NULL, NULL, 'AB03-E.gif'),
(1536, 282, 'A', 999, 'A', NULL, NULL, 'AB04-A.gif'),
(1537, 282, 'B', 999, 'B', NULL, NULL, 'AB04-B.gif'),
(1538, 282, 'C', 999, 'C', NULL, NULL, 'AB04-C.gif'),
(1539, 282, 'D', 999, 'D', NULL, NULL, 'AB04-D.gif'),
(1540, 282, 'E', 999, 'E', NULL, NULL, 'AB04-E.gif'),
(1541, 283, 'A', 999, 'A', NULL, NULL, 'AB05-A.gif'),
(1542, 283, 'B', 999, 'B', NULL, NULL, 'AB05-B.gif'),
(1543, 283, 'C', 999, 'C', NULL, NULL, 'AB05-C.gif'),
(1544, 283, 'D', 999, 'D', NULL, NULL, 'AB05-D.gif'),
(1545, 283, 'E', 999, 'E', NULL, NULL, 'AB05-E.gif'),
(1546, 284, 'A', 999, 'A', NULL, NULL, 'AB06-A.gif'),
(1547, 284, 'B', 999, 'B', NULL, NULL, 'AB06-B.gif'),
(1548, 284, 'C', 999, 'C', NULL, NULL, 'AB06-C.gif'),
(1549, 284, 'D', 999, 'D', NULL, NULL, 'AB06-D.gif'),
(1550, 284, 'E', 999, 'E', NULL, NULL, 'AB06-E.gif'),
(1551, 285, 'A', 999, 'A', NULL, NULL, 'AB07-A.gif'),
(1552, 285, 'B', 999, 'B', NULL, NULL, 'AB07-B.gif'),
(1553, 285, 'C', 999, 'C', NULL, NULL, 'AB07-C.gif'),
(1554, 285, 'D', 999, 'D', NULL, NULL, 'AB07-D.gif'),
(1555, 285, 'E', 999, 'E', NULL, NULL, 'AB07-E.gif'),
(1556, 286, 'A', 999, 'A', NULL, NULL, 'AB08-A.gif'),
(1557, 286, 'B', 999, 'B', NULL, NULL, 'AB08-B.gif'),
(1558, 286, 'C', 999, 'C', NULL, NULL, 'AB08-C.gif'),
(1559, 286, 'D', 999, 'D', NULL, NULL, 'AB08-D.gif'),
(1560, 286, 'E', 999, 'E', NULL, NULL, 'AB08-E.gif'),
(1561, 287, 'A', 999, 'A', NULL, NULL, 'AB09-A.gif'),
(1562, 287, 'B', 999, 'B', NULL, NULL, 'AB09-B.gif'),
(1563, 287, 'C', 999, 'C', NULL, NULL, 'AB09-C.gif'),
(1564, 287, 'D', 999, 'D', NULL, NULL, 'AB09-D.gif'),
(1565, 287, 'E', 999, 'E', NULL, NULL, 'AB09-E.gif'),
(1566, 288, 'A', 999, 'A', NULL, NULL, 'AB10-A.gif'),
(1567, 288, 'B', 999, 'B', NULL, NULL, 'AB10-B.gif'),
(1568, 288, 'C', 999, 'C', NULL, NULL, 'AB10-C.gif'),
(1569, 288, 'D', 999, 'D', NULL, NULL, 'AB10-D.gif'),
(1570, 288, 'E', 999, 'E', NULL, NULL, 'AB10-E.gif'),
(1571, 289, 'A', 999, 'A', NULL, NULL, 'AB11-A.gif'),
(1572, 289, 'B', 999, 'B', NULL, NULL, 'AB11-B.gif'),
(1573, 289, 'C', 999, 'C', NULL, NULL, 'AB11-C.gif'),
(1574, 289, 'D', 999, 'D', NULL, NULL, 'AB11-D.gif'),
(1575, 289, 'E', 999, 'E', NULL, NULL, 'AB11-E.gif'),
(1576, 290, 'A', 999, 'A', NULL, NULL, 'AB12-A.gif'),
(1577, 290, 'B', 999, 'B', NULL, NULL, 'AB12-B.gif'),
(1578, 290, 'C', 999, 'C', NULL, NULL, 'AB12-C.gif'),
(1579, 290, 'D', 999, 'D', NULL, NULL, 'AB12-D.gif'),
(1580, 290, 'E', 999, 'E', NULL, NULL, 'AB12-E.gif'),
(1581, 291, 'A', 999, 'A', NULL, NULL, 'AB13-A.gif'),
(1582, 291, 'B', 999, 'B', NULL, NULL, 'AB13-B.gif'),
(1583, 291, 'C', 999, 'C', NULL, NULL, 'AB13-C.gif'),
(1584, 291, 'D', 999, 'D', NULL, NULL, 'AB13-D.gif'),
(1585, 291, 'E', 999, 'E', NULL, NULL, 'AB13-E.gif'),
(1586, 292, 'A', 999, 'A', NULL, NULL, 'AB14-A.gif'),
(1587, 292, 'B', 999, 'B', NULL, NULL, 'AB14-B.gif'),
(1588, 292, 'C', 999, 'C', NULL, NULL, 'AB14-C.gif'),
(1589, 292, 'D', 999, 'D', NULL, NULL, 'AB14-D.gif'),
(1590, 292, 'E', 999, 'E', NULL, NULL, 'AB14-E.gif'),
(1591, 293, 'A', 999, 'A', NULL, NULL, 'AB15-A.gif'),
(1592, 293, 'B', 999, 'B', NULL, NULL, 'AB15-B.gif'),
(1593, 293, 'C', 999, 'C', NULL, NULL, 'AB15-C.gif'),
(1594, 293, 'D', 999, 'D', NULL, NULL, 'AB15-D.gif'),
(1595, 293, 'E', 999, 'E', NULL, NULL, 'AB15-E.gif'),
(1596, 294, 'A', 999, 'A', NULL, NULL, 'AB16-A.gif'),
(1597, 294, 'B', 999, 'B', NULL, NULL, 'AB16-B.gif'),
(1598, 294, 'C', 999, 'C', NULL, NULL, 'AB16-C.gif'),
(1599, 294, 'D', 999, 'D', NULL, NULL, 'AB16-D.gif'),
(1600, 294, 'E', 999, 'E', NULL, NULL, 'AB16-E.gif'),
(1601, 295, 'A', 999, '3,800,000', NULL, NULL, NULL),
(1602, 295, 'B', 999, '4,200,000', NULL, NULL, NULL),
(1603, 295, 'C', 999, '38,000,000', NULL, NULL, NULL),
(1604, 295, 'D', 999, '40,000,000', NULL, NULL, NULL),
(1605, 295, 'E', 999, '42,000,000', NULL, NULL, NULL),
(1606, 296, 'A', 999, 'Manufacturing', NULL, NULL, NULL),
(1607, 296, 'B', 999, 'Electricity, Gas and Water', NULL, NULL, NULL),
(1608, 296, 'C', 999, 'Transport and Communication', NULL, NULL, NULL),
(1609, 296, 'D', 999, 'Domestic', NULL, NULL, NULL),
(1610, 296, 'E', 999, 'Mining and Quarrying', NULL, NULL, NULL),
(1611, 297, 'A', 999, 'January', NULL, NULL, NULL),
(1612, 297, 'B', 999, 'February', NULL, NULL, NULL),
(1613, 297, 'C', 999, 'March', NULL, NULL, NULL),
(1614, 297, 'D', 999, 'April', NULL, NULL, NULL),
(1615, 297, 'E', 999, 'May', NULL, NULL, NULL),
(1616, 298, 'A', 999, '£516', NULL, NULL, NULL),
(1617, 298, 'B', 999, '£580', NULL, NULL, NULL),
(1618, 298, 'C', 999, '£1,096', NULL, NULL, NULL),
(1619, 298, 'D', 999, '£1,256', NULL, NULL, NULL),
(1620, 298, 'E', 999, '£1,894', NULL, NULL, NULL),
(1621, 299, 'A', 999, 'Geneva', NULL, NULL, NULL),
(1622, 299, 'B', 999, 'London', NULL, NULL, NULL),
(1623, 299, 'C', 999, 'Munich', NULL, NULL, NULL),
(1624, 299, 'D', 999, 'Paris', NULL, NULL, NULL),
(1625, 299, 'E', 999, 'Rome', NULL, NULL, NULL),
(1626, 300, 'A', 999, '4,255', NULL, NULL, NULL),
(1627, 300, 'B', 999, '4,350', NULL, NULL, NULL),
(1628, 300, 'C', 999, '5,250', NULL, NULL, NULL),
(1629, 300, 'D', 999, '5,550', NULL, NULL, NULL),
(1630, 300, 'E', 999, '9,030', NULL, NULL, NULL),
(1631, 301, 'A', 999, '11.2%', NULL, NULL, NULL),
(1632, 301, 'B', 999, '11.8%', NULL, NULL, NULL),
(1633, 301, 'C', 999, '12.6%', NULL, NULL, NULL),
(1634, 301, 'D', 999, '13.2%', NULL, NULL, NULL),
(1635, 301, 'E', 999, '13.4%', NULL, NULL, NULL),
(1636, 302, 'A', 999, '6:32', NULL, NULL, NULL),
(1637, 302, 'B', 999, '7:13', NULL, NULL, NULL),
(1638, 302, 'C', 999, '13:7', NULL, NULL, NULL),
(1639, 302, 'D', 999, '13:6', NULL, NULL, NULL),
(1640, 302, 'E', 999, '32:6', NULL, NULL, NULL),
(1641, 303, 'A', 999, 'Factory A', NULL, NULL, NULL),
(1642, 303, 'B', 999, 'Factory B', NULL, NULL, NULL),
(1643, 303, 'C', 999, 'Factory C', NULL, NULL, NULL),
(1644, 303, 'D', 999, 'Factory D', NULL, NULL, NULL),
(1645, 303, 'E', 999, 'Factory E', NULL, NULL, NULL),
(1646, 304, 'A', 999, '4%', NULL, NULL, NULL),
(1647, 304, 'B', 999, '5%', NULL, NULL, NULL),
(1648, 304, 'C', 999, '6%', NULL, NULL, NULL),
(1649, 304, 'D', 999, '7%', NULL, NULL, NULL),
(1650, 304, 'E', 999, '8%', NULL, NULL, NULL)

) AS AnswersTemp(
	Id,
	QuestionId,
	Value,
	IsCorrect,
	Text,
	ImageTitle,
	ImageCaption,
	ImageURL
))


MERGE Answers AS target
USING (
	SELECT *
	FROM AnswersCte
) AS source
ON target.Id = source.Id
WHEN MATCHED
	THEN UPDATE SET
		target.QuestionId = source.QuestionId,
		target.Value = source.Value,
		target.IsCorrect = source.IsCorrect,
		target.Text = source.Text,
		target.ImageTitle = source.ImageTitle,
		target.ImageCaption = source.ImageCaption,
		target.ImageURL = source.ImageURL
WHEN NOT MATCHED
	THEN INSERT (
		Id,
	    QuestionId,
	    Value,
	    IsCorrect,
	    Text,
	    ImageTitle,
	    ImageCaption,
	    ImageURL
	) VALUES (
		source.Id,
		source.QuestionId,
		source.Value,
		source.IsCorrect,
		source.Text,
		source.ImageTitle,
		source.ImageCaption,
		source.ImageURL
	)
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;


