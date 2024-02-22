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
(6, 2, 1, 'How interested are you in this work activity?', 'Working in the armed forces', NULL, NULL, NULL),
(7, 2, 1, 'How interested are you in this work activity?', 'Drawing and painting', NULL, NULL, NULL),
(8, 2, 1, 'How interested are you in this work activity?', 'Looking at money and bank account information', NULL, NULL, NULL),
(9, 2, 1, 'How interested are you in this work activity?', 'Overseeing a control room of an industrial plant', NULL, NULL, NULL),
(10, 2, 2, 'How interested are you in this work activity?', 'Selling products or services to people', NULL, NULL, NULL),
(11, 2, 2, 'How interested are you in this work activity?', 'Working in a kitchen', NULL, NULL, NULL),
(12, 2, 2, 'How interested are you in this work activity?', 'Organising files and paperwork', NULL, NULL, NULL),
(13, 2, 2, 'How interested are you in this work activity?', 'Studying how animals live', NULL, NULL, NULL),
(14, 2, 2, 'How interested are you in this work activity?', 'Moving products between different areas', NULL, NULL, NULL),
(15, 2, 3, 'How interested are you in this work activity?', 'Grouping old documents together into files', NULL, NULL, NULL),
(16, 2, 3, 'How interested are you in this work activity?', 'Providing advice to people who have problems', NULL, NULL, NULL),
(17, 2, 3, 'How interested are you in this work activity?', 'Stopping criminals from breaking the law', NULL, NULL, NULL),
(18, 2, 3, 'How interested are you in this work activity?', 'Choosing patterns or colours', NULL, NULL, NULL),
(19, 2, 3, 'How interested are you in this work activity?', 'Adding up information about money', NULL, NULL, NULL),
(20, 2, 4, 'How interested are you in this work activity?', 'Making improvements to a house', NULL, NULL, NULL),
(21, 2, 4, 'How interested are you in this work activity?', 'Working out how to advertise a product', NULL, NULL, NULL),
(22, 2, 4, 'How interested are you in this work activity?', 'Packaging food for sale', NULL, NULL, NULL),
(23, 2, 4, 'How interested are you in this work activity?', 'Completing administrative tasks in a set order', NULL, NULL, NULL),
(24, 2, 4, 'How interested are you in this work activity?', 'Studying oceans and sea life', NULL, NULL, NULL),
(25, 2, 5, 'How interested are you in this work activity?', 'Organising things in a warehouse', NULL, NULL, NULL),
(26, 2, 5, 'How interested are you in this work activity?', 'Writing a speech', NULL, NULL, NULL),
(27, 2, 5, 'How interested are you in this work activity?', 'Teaching new skills to others', NULL, NULL, NULL),
(28, 2, 5, 'How interested are you in this work activity?', 'Protecting people or property', NULL, NULL, NULL),
(29, 2, 5, 'How interested are you in this work activity?', 'Developing photographs', NULL, NULL, NULL),
(30, 2, 6, 'How interested are you in this work activity?', 'Checking information about money for mistakes', NULL, NULL, NULL),
(31, 2, 6, 'How interested are you in this work activity?', 'Building homes or offices', NULL, NULL, NULL),
(32, 2, 6, 'How interested are you in this work activity?', 'Planning how to advertise a product', NULL, NULL, NULL),
(33, 2, 6, 'How interested are you in this work activity?', 'Working in a bar', NULL, NULL, NULL),
(34, 2, 6, 'How interested are you in this work activity?', 'Typing information into a computer', NULL, NULL, NULL),
(35, 2, 7, 'How interested are you in this work activity?', 'Predicting earthquakes', NULL, NULL, NULL),
(36, 2, 7, 'How interested are you in this work activity?', 'Loading and unloading ships', NULL, NULL, NULL),
(37, 2, 7, 'How interested are you in this work activity?', 'Writing a play', NULL, NULL, NULL),
(38, 2, 7, 'How interested are you in this work activity?', 'Helping people with special needs to learn', NULL, NULL, NULL),
(39, 2, 7, 'How interested are you in this work activity?', 'Working in the emergency services', NULL, NULL, NULL),
(40, 2, 8, 'How interested are you in this work activity?', 'Designing products for use in people''s homes', NULL, NULL, NULL),
(41, 2, 8, 'How interested are you in this work activity?', 'Making sense of information about money', NULL, NULL, NULL),
(42, 2, 8, 'How interested are you in this work activity?', 'Making improvements to roads', NULL, NULL, NULL),
(43, 2, 8, 'How interested are you in this work activity?', 'Organising a large social event', NULL, NULL, NULL),
(44, 2, 8, 'How interested are you in this work activity?', 'Working in a restaurant', NULL, NULL, NULL),
(45, 2, 9, 'How interested are you in this work activity?', 'Doing administrative tasks for a solicitor or lawyer', NULL, NULL, NULL),
(46, 2, 9, 'How interested are you in this work activity?', 'Looking at samples of rock', NULL, NULL, NULL),
(47, 2, 9, 'How interested are you in this work activity?', 'Organising printing for a magazine', NULL, NULL, NULL),
(48, 2, 9, 'How interested are you in this work activity?', 'Researching information for a TV programme', NULL, NULL, NULL),
(49, 2, 9, 'How interested are you in this work activity?', 'Caring for people who are unwell', NULL, NULL, NULL),
(50, 2, 10, 'How interested are you in this work activity?', 'Working on a film or TV set', NULL, NULL, NULL),
(51, 2, 10, 'How interested are you in this work activity?', 'Designing a computer program', NULL, NULL, NULL),
(52, 2, 10, 'How interested are you in this work activity?', 'Fixing computer equipment', NULL, NULL, NULL),
(53, 2, 10, 'How interested are you in this work activity?', 'Helping people buy things in a shop', NULL, NULL, NULL),
(54, 2, 10, 'How interested are you in this work activity?', 'Teaching people how to swim', NULL, NULL, NULL),
(55, 2, 11, 'How interested are you in this work activity?', 'Checking legal documents for errors', NULL, NULL, NULL),
(56, 2, 11, 'How interested are you in this work activity?', 'Examining genetic samples', NULL, NULL, NULL),
(57, 2, 11, 'How interested are you in this work activity?', 'Organising trips on public transport', NULL, NULL, NULL),
(58, 2, 11, 'How interested are you in this work activity?', 'Looking through old files for information', NULL, NULL, NULL),
(59, 2, 11, 'How interested are you in this work activity?', 'Checking how healthy someone is', NULL, NULL, NULL),
(60, 2, 12, 'How interested are you in this work activity?', 'Working with music or sound', NULL, NULL, NULL),
(61, 2, 12, 'How interested are you in this work activity?', 'Designing a computer database', NULL, NULL, NULL),
(62, 2, 12, 'How interested are you in this work activity?', 'Fixing air conditioning', NULL, NULL, NULL),
(63, 2, 12, 'How interested are you in this work activity?', 'Giving people information over the phone', NULL, NULL, NULL),
(64, 2, 12, 'How interested are you in this work activity?', 'Working in a hotel', NULL, NULL, NULL),
(65, 2, 13, 'How interested are you in this work activity?', 'Helping people fill in application forms correctly', NULL, NULL, NULL),
(66, 2, 13, 'How interested are you in this work activity?', 'Working as an assistant in a science lab', NULL, NULL, NULL),
(67, 2, 13, 'How interested are you in this work activity?', 'Using transport to move people from place to place', NULL, NULL, NULL),
(68, 2, 13, 'How interested are you in this work activity?', 'Writing a report for a newspaper or magazine', NULL, NULL, NULL),
(69, 2, 13, 'How interested are you in this work activity?', 'Caring for children who might be at risk', NULL, NULL, NULL),
(70, 2, 14, 'How interested are you in this work activity?', 'Taking photographs', NULL, NULL, NULL),
(71, 2, 14, 'How interested are you in this work activity?', 'Setting up a group of connected computers', NULL, NULL, NULL),
(72, 2, 14, 'How interested are you in this work activity?', 'Making tools that save people time', NULL, NULL, NULL),
(73, 2, 14, 'How interested are you in this work activity?', 'Managing a business', NULL, NULL, NULL),
(74, 2, 14, 'How interested are you in this work activity?', 'Guiding people around tourist attractions', NULL, NULL, NULL),
(75, 2, 15, 'How interested are you in this work activity?', 'Making detailed plans for activities', NULL, NULL, NULL),
(76, 2, 15, 'How interested are you in this work activity?', 'Looking at things under a microscope', NULL, NULL, NULL),
(77, 2, 15, 'How interested are you in this work activity?', 'Delivering products from a vehicle', NULL, NULL, NULL),
(78, 2, 15, 'How interested are you in this work activity?', 'Checking written information for mistakes', NULL, NULL, NULL),
(79, 2, 15, 'How interested are you in this work activity?', 'Working with people who are in need', NULL, NULL, NULL),
(80, 2, 16, 'How interested are you in this work activity?', 'Creating computer games', NULL, NULL, NULL),
(81, 2, 16, 'How interested are you in this work activity?', 'Making sense of information about numbers', NULL, NULL, NULL),
(82, 2, 16, 'How interested are you in this work activity?', 'Operating heavy machinery', NULL, NULL, NULL),
(83, 2, 16, 'How interested are you in this work activity?', 'Helping people use products or services they have bought', NULL, NULL, NULL),
(84, 2, 16, 'How interested are you in this work activity?', 'Organising an event', NULL, NULL, NULL),
(85, 2, 17, 'How interested are you in this work activity?', 'Checking that plans are on track', NULL, NULL, NULL),
(86, 2, 17, 'How interested are you in this work activity?', 'Studying different types of chemicals', NULL, NULL, NULL),
(87, 2, 17, 'How interested are you in this work activity?', 'Planning transport routes for a business', NULL, NULL, NULL),
(88, 2, 17, 'How interested are you in this work activity?', 'Using different ways to find information', NULL, NULL, NULL),
(186, 8, 1, 'Which is the stronger arch?', NULL, 'this is a test', 'Image A is of a round arch and image B is a flat arch.', 'M01.gif'),
(187, 8, 2, 'Which pair of magnets will attract each other?', NULL, NULL, 'Images of two U-shaped magnets facing one another.', 'M02.gif'),
(188, 8, 3, 'Which trolley will turn in the smallest circle?', NULL, NULL, 'Images of three trolleys with fixed back wheels and pivoting front wheels.', 'M03.gif'),
(189, 8, 4, 'Which chain is likely to break first when the lever is pulled as shown?', NULL, NULL, 'Image of a lever held in position by two chains.', 'M04.gif'),
(190, 8, 5, 'How would the iron bar hang when suspended in a rope sling?', NULL, NULL, 'Image of an iron bar suspended in a rope sling.', 'M05.gif'),
(191, 8, 6, 'Which is the hottest part of the oven?', NULL, NULL, 'Image of an oven with a top and middle shelf.', 'M06.gif'),
(192, 8, 7, 'Which screw is least likely to pull out of the wall?', NULL, NULL, 'Image of two shelves each attached with two screws in a different configuration.', 'M07.gif'),
(193, 8, 8, 'Which liquid is the densest?', NULL, NULL, 'Image of three beakers each holding a cork bobbing at a different height in liquid.', 'M08.gif'),
(194, 8, 9, 'Which mirror will reflect light rays as shown?', NULL, NULL, 'Image of three mirrors each curved differently. The mirrors are all reflecting the same parallel light rays.', 'M09.gif'),
(195, 8, 10, 'Which apparatus requires least effort to lift the weight?', NULL, NULL, 'Image of three diagrams showing 100kg weights attached to different lifting apparatus.', 'M10.gif'),
(196, 8, 11, 'Which arrangement would support the heavier load?', NULL, NULL, 'Image of two structures made from 3 blocks each holding 100kg weights.', 'M11.gif'),
(89, 4, 1, NULL, 'How important is having activities where I can work with other people?', NULL, NULL, NULL),
(90, 4, 1, NULL, 'How important is having a safe environment without surprises?', NULL, NULL, NULL),
(91, 4, 1, NULL, 'How important is having lots of things to do?', NULL, NULL, NULL),
(92, 4, 1, NULL, 'How important is it that doing things on your own is seen as a good thing?', NULL, NULL, NULL),
(93, 4, 1, NULL, 'How important is doing a variety of tasks?', NULL, NULL, NULL),
(94, 4, 2, NULL, 'How important is it that research skills are valued?', NULL, NULL, NULL),
(95, 4, 2, NULL, 'How important is it that the environment is focused on making money?', NULL, NULL, NULL),
(96, 4, 2, NULL, 'How important is working in a friendly environment?', NULL, NULL, NULL),
(97, 4, 2, NULL, 'How important is doing well organised tasks?', NULL, NULL, NULL),
(98, 4, 2, NULL, 'How important is working in a lively environment?', NULL, NULL, NULL),
(99, 4, 3, NULL, 'How important is work where you can be creative?', NULL, NULL, NULL),
(100, 4, 3, NULL, 'How important is it that you can use practical skills?', NULL, NULL, NULL),
(101, 4, 3, NULL, 'How important is it that making sense of information is seen as a good thing?', NULL, NULL, NULL),
(102, 4, 3, NULL, 'How important is it that what you earn is linked to how well you do?', NULL, NULL, NULL),
(103, 4, 3, NULL, 'How important is doing things in a team?', NULL, NULL, NULL),
(104, 4, 4, NULL, 'How important is it that there is an organised way of doing things?', NULL, NULL, NULL),
(105, 4, 4, NULL, 'How important is having competition against others?', NULL, NULL, NULL),
(106, 4, 4, NULL, 'How important is it to do what you believe in?', NULL, NULL, NULL),
(107, 4, 4, NULL, 'How important is being able to focus on doing a good job?', NULL, NULL, NULL),
(108, 4, 4, NULL, 'How important is using information to solve problems?', NULL, NULL, NULL),
(109, 4, 5, NULL, 'How important is being given money as a reward for doing well?', NULL, NULL, NULL),
(110, 4, 5, NULL, 'How important is working in a caring environment?', NULL, NULL, NULL),
(111, 4, 5, NULL, 'How important is it that having clear instructions is seen as a good thing?', NULL, NULL, NULL),
(112, 4, 5, NULL, 'How important is being encouraged to do your best?', NULL, NULL, NULL),
(113, 4, 5, NULL, 'How important is it that having different opinions is seen as a good thing?', NULL, NULL, NULL),
(114, 4, 6, NULL, 'How important is it that the focus is on getting things done?', NULL, NULL, NULL),
(115, 4, 6, NULL, 'How important is it that you do activities that need special skills?', NULL, NULL, NULL),
(116, 4, 6, NULL, 'How important is it that the work helps you get into better jobs?', NULL, NULL, NULL),
(117, 4, 6, NULL, 'How important is it that helping others is seen as a good thing?', NULL, NULL, NULL),
(118, 4, 6, NULL, 'How important is having a set way of doing things?', NULL, NULL, NULL),
(119, 4, 7, NULL, 'How important is working in a busy environment?', NULL, NULL, NULL),
(120, 4, 7, NULL, 'How important is it that having ideas is seen as a good thing?', NULL, NULL, NULL),
(121, 4, 7, NULL, 'How important is working in a relaxed environment?', NULL, NULL, NULL),
(122, 4, 7, NULL, 'How important is doing activities where understanding a set topic is important?', NULL, NULL, NULL),
(123, 4, 7, NULL, 'How important is it that good relationships with others are important for doing well?', NULL, NULL, NULL),
(124, 4, 8, NULL, 'How important is it that the focus is on giving a good service to others?', NULL, NULL, NULL),
(125, 4, 8, NULL, 'How important is it that tidying and organising things is valued?', NULL, NULL, NULL),
(126, 4, 8, NULL, 'How important is it that you can do things that you''ve not done before?', NULL, NULL, NULL),
(127, 4, 8, NULL, 'How important is it that doing things differently is seen as a good thing?', NULL, NULL, NULL),
(128, 4, 8, NULL, 'How important is it that the focus is on getting a lot done?', NULL, NULL, NULL),
(129, 4, 9, NULL, 'How important is it that activities are focused on one specialist area?', NULL, NULL, NULL),
(130, 4, 9, NULL, 'How important is it that you''ll get the chance to make money?', NULL, NULL, NULL),
(131, 3, 1, 'Think about how often you do the following things.', 'Changing what other people think', NULL, NULL, NULL),
(132, 3, 1, 'Think about how often you do the following things.', 'Fixing things', NULL, NULL, NULL),
(133, 3, 1, 'Think about how often you do the following things.', 'Thinking through complicated ideas', NULL, NULL, NULL),
(134, 3, 1, 'Think about how often you do the following things.', 'Feeling stressed under pressure', NULL, NULL, NULL),
(135, 3, 1, 'Think about how often you do the following things.', 'Talking with people', NULL, NULL, NULL),
(136, 3, 2, 'Think about how often you do the following things.', 'Working with numbers', NULL, NULL, NULL),
(137, 3, 2, 'Think about how often you do the following things.', 'Keeping things tidy', NULL, NULL, NULL),
(138, 3, 2, 'Think about how often you do the following things.', 'Keeping your emotions under control when others make negative comments', NULL, NULL, NULL),
(139, 3, 2, 'Think about how often you do the following things.', 'Being caring and supportive to other people', NULL, NULL, NULL),
(140, 3, 2, 'Think about how often you do the following things.', 'Taking on more work than most people', NULL, NULL, NULL),
(141, 3, 3, 'Think about how often you do the following things.', 'Thinking about why people act the way they do', NULL, NULL, NULL),
(142, 3, 3, 'Think about how often you do the following things.', 'Making things', NULL, NULL, NULL),
(143, 3, 3, 'Think about how often you do the following things.', 'Doing things in new ways', NULL, NULL, NULL),
(144, 3, 3, 'Think about how often you do the following things.', 'Organising people', NULL, NULL, NULL),
(145, 3, 3, 'Think about how often you do the following things.', 'Reviewing information', NULL, NULL, NULL),
(146, 3, 4, 'Think about how often you do the following things.', 'Feeling worried', NULL, NULL, NULL),
(147, 3, 4, 'Think about how often you do the following things.', 'Spending time with other people', NULL, NULL, NULL),
(148, 3, 4, 'Think about how often you do the following things.', 'Holding back from showing how you really feel', NULL, NULL, NULL),
(149, 3, 4, 'Think about how often you do the following things.', 'Thinking about what other people have achieved, rather than what you have achieved', NULL, NULL, NULL),
(150, 3, 4, 'Think about how often you do the following things.', 'Preparing for things early', NULL, NULL, NULL),
(151, 3, 5, 'Think about how often you do the following things.', 'Trying to understand why people act the way they do', NULL, NULL, NULL),
(152, 3, 5, 'Think about how often you do the following things.', 'Pushing yourself hard to do well', NULL, NULL, NULL),
(153, 3, 5, 'Think about how often you do the following things.', 'Doing things differently rather than the same way as before', NULL, NULL, NULL),
(154, 3, 5, 'Think about how often you do the following things.', 'Mending things', NULL, NULL, NULL),
(155, 3, 5, 'Think about how often you do the following things.', 'Getting other people to do something', NULL, NULL, NULL),
(156, 3, 6, 'Think about how often you do the following things.', 'Using facts to understand something', NULL, NULL, NULL),
(157, 3, 6, 'Think about how often you do the following things.', 'Turning up to social events', NULL, NULL, NULL),
(158, 3, 6, 'Think about how often you do the following things.', 'Worrying when things go wrong', NULL, NULL, NULL),
(159, 3, 6, 'Think about how often you do the following things.', 'Talking about others rather than yourself', NULL, NULL, NULL),
(160, 3, 6, 'Think about how often you do the following things.', 'Showing how you feel without getting upset', NULL, NULL, NULL),
(161, 3, 7, 'Think about how often you do the following things.', 'Planning what you are going to do', NULL, NULL, NULL),
(162, 3, 7, 'Think about how often you do the following things.', 'Exploring why people think in a particular way', NULL, NULL, NULL),
(163, 3, 7, 'Think about how often you do the following things.', 'Getting involved in more activities than most people', NULL, NULL, NULL),
(164, 3, 7, 'Think about how often you do the following things.', 'Taking the lead in activities', NULL, NULL, NULL),
(165, 3, 7, 'Think about how often you do the following things.', 'Wanting to understand things', NULL, NULL, NULL),
(166, 3, 8, 'Think about how often you do the following things.', 'Enjoying hands-on work', NULL, NULL, NULL),
(167, 3, 8, 'Think about how often you do the following things.', 'Making friends', NULL, NULL, NULL),
(168, 3, 8, 'Think about how often you do the following things.', 'Feeling nervous before an important event', NULL, NULL, NULL),
(169, 3, 8, 'Think about how often you do the following things.', 'Looking at numbers to help you understand something', NULL, NULL, NULL),
(170, 3, 8, 'Think about how often you do the following things.', 'Doing things in an organised way', NULL, NULL, NULL),
(171, 3, 9, 'Think about how often you do the following things.', 'Finding out what other people think', NULL, NULL, NULL),
(172, 3, 9, 'Think about how often you do the following things.', 'Not showing other people how you feel', NULL, NULL, NULL),
(173, 3, 9, 'Think about how often you do the following things.', 'Working hard to do a good job', NULL, NULL, NULL),
(174, 3, 9, 'Think about how often you do the following things.', 'Thinking about what makes other people tick', NULL, NULL, NULL),
(175, 3, 9, 'Think about how often you do the following things.', 'Telling other people what to do', NULL, NULL, NULL),
(176, 3, 10, 'Think about how often you do the following things.', 'Fixing things when they break', NULL, NULL, NULL),
(177, 3, 10, 'Think about how often you do the following things.', 'Thinking up clever ideas', NULL, NULL, NULL),
(178, 3, 10, 'Think about how often you do the following things.', 'Getting worried about doing well', NULL, NULL, NULL),
(179, 3, 10, 'Think about how often you do the following things.', 'Spending time with people', NULL, NULL, NULL),
(180, 3, 10, 'Think about how often you do the following things.', 'Enjoying looking up information', NULL, NULL, NULL),
(181, 3, 11, 'Think about how often you do the following things.', 'Being on time for things', NULL, NULL, NULL),
(182, 3, 11, 'Think about how often you do the following things.', 'Hiding how you feel when you are upset', NULL, NULL, NULL),
(183, 3, 11, 'Think about how often you do the following things.', 'Listening and talking about things with others', NULL, NULL, NULL),
(184, 3, 11, 'Think about how often you do the following things.', 'Doing high quality work', NULL, NULL, NULL),
(185, 3, 11, 'Think about how often you do the following things.', 'Enjoying activities that need you to have an understanding of others', NULL, NULL, NULL),
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
(1, 1, 'A', 1, 'A', NULL, NULL, NULL),
(2, 1, 'B', 0, 'B', NULL, NULL, NULL),
(3, 1, 'C', 1, 'C', NULL, NULL, NULL),
(4, 1, 'D', 0, 'D', NULL, NULL, NULL),
(5, 1, 'E', 0, 'E', NULL, NULL, NULL),
(6, 1, 'A', 0, 'A', NULL, NULL, NULL),
(7, 1, 'B', 0, 'B', NULL, NULL, NULL),
(8, 1, 'C', 0, 'C', NULL, NULL, NULL),
(9, 1, 'D', 0, 'D', NULL, NULL, NULL),
(10, 1, 'E', 1, 'E', NULL, NULL, NULL),
(11, 1, 'A', 1, 'A', NULL, NULL, NULL),
(12, 1, 'B', 1, 'B', NULL, NULL, NULL),
(13, 1, 'C', 1, 'C', NULL, NULL, NULL),
(14, 1, 'D', 0, 'D', NULL, NULL, NULL),
(15, 1, 'E', 0, 'E', NULL, NULL, NULL),
(16, 1, 'A', 1, 'A', NULL, NULL, NULL),
(17, 1, 'B', 0, 'B', NULL, NULL, NULL),
(18, 1, 'C', 0, 'C', NULL, NULL, NULL),
(19, 1, 'D', 0, 'D', NULL, NULL, NULL),
(20, 1, 'E', 0, 'E', NULL, NULL, NULL),
(21, 1, 'A', 0, 'A', NULL, NULL, NULL),
(22, 1, 'B', 0, 'B', NULL, NULL, NULL),
(23, 1, 'C', 0, 'C', NULL, NULL, NULL),
(24, 1, 'D', 0, 'D', NULL, NULL, NULL),
(25, 1, 'E', 1, 'E', NULL, NULL, NULL),
(26, 1, 'A', 1, 'A', NULL, NULL, NULL),
(27, 1, 'B', 0, 'B', NULL, NULL, NULL),
(28, 1, 'C', 0, 'C', NULL, NULL, NULL),
(29, 1, 'D', 0, 'D', NULL, NULL, NULL),
(30, 1, 'E', 0, 'E', NULL, NULL, NULL),
(31, 1, 'A', 0, 'A', NULL, NULL, NULL),
(32, 1, 'B', 1, 'B', NULL, NULL, NULL),
(33, 1, 'C', 1, 'C', NULL, NULL, NULL),
(34, 1, 'D', 0, 'D', NULL, NULL, NULL),
(35, 1, 'E', 0, 'E', NULL, NULL, NULL),
(36, 1, 'A', 0, 'A', NULL, NULL, NULL),
(37, 1, 'B', 0, 'B', NULL, NULL, NULL),
(38, 1, 'C', 1, 'C', NULL, NULL, NULL),
(39, 1, 'D', 1, 'D', NULL, NULL, NULL),
(40, 1, 'E', 0, 'E', NULL, NULL, NULL),
(41, 1, 'A', 1, 'A', NULL, NULL, NULL),
(42, 1, 'B', 1, 'B', NULL, NULL, NULL),
(43, 1, 'C', 1, 'C', NULL, NULL, NULL),
(44, 1, 'D', 1, 'D', NULL, NULL, NULL),
(45, 1, 'E', 0, 'E', NULL, NULL, NULL),
(46, 1, 'A', 0, 'A', NULL, NULL, NULL),
(47, 1, 'B', 0, 'B', NULL, NULL, NULL),
(48, 1, 'C', 0, 'C', NULL, NULL, NULL),
(49, 1, 'D', 0, 'D', NULL, NULL, NULL),
(50, 1, 'E', 1, 'E', NULL, NULL, NULL),
(51, 2, 'A', 0, 'A', NULL, NULL, NULL),
(52, 2, 'B', 1, 'B', NULL, NULL, NULL),
(53, 2, 'C', 0, 'C', NULL, NULL, NULL),
(54, 2, 'D', 0, 'D', NULL, NULL, NULL),
(55, 2, 'E', 0, 'E', NULL, NULL, NULL),
(56, 2, 'A', 0, 'A', NULL, NULL, NULL),
(57, 2, 'B', 0, 'B', NULL, NULL, NULL),
(58, 2, 'C', 0, 'C', NULL, NULL, NULL),
(59, 2, 'D', 0, 'D', NULL, NULL, NULL),
(60, 2, 'E', 1, 'E', NULL, NULL, NULL),
(61, 2, 'A', 0, 'A', NULL, NULL, NULL),
(62, 2, 'B', 1, 'B', NULL, NULL, NULL),
(63, 2, 'C', 0, 'C', NULL, NULL, NULL),
(64, 2, 'D', 0, 'D', NULL, NULL, NULL),
(65, 2, 'E', 0, 'E', NULL, NULL, NULL),
(66, 2, 'A', 0, 'A', NULL, NULL, NULL),
(67, 2, 'B', 1, 'B', NULL, NULL, NULL),
(68, 2, 'C', 0, 'C', NULL, NULL, NULL),
(69, 2, 'D', 1, 'D', NULL, NULL, NULL),
(70, 2, 'E', 0, 'E', NULL, NULL, NULL),
(71, 2, 'A', 0, 'A', NULL, NULL, NULL),
(72, 2, 'B', 0, 'B', NULL, NULL, NULL),
(73, 2, 'C', 0, 'C', NULL, NULL, NULL),
(74, 2, 'D', 0, 'D', NULL, NULL, NULL),
(75, 2, 'E', 1, 'E', NULL, NULL, NULL),
(76, 2, 'A', 1, 'A', NULL, NULL, NULL),
(77, 2, 'B', 0, 'B', NULL, NULL, NULL),
(78, 2, 'C', 0, 'C', NULL, NULL, NULL),
(79, 2, 'D', 0, 'D', NULL, NULL, NULL),
(80, 2, 'E', 0, 'E', NULL, NULL, NULL),
(81, 2, 'A', 1, 'A', NULL, NULL, NULL),
(82, 2, 'B', 0, 'B', NULL, NULL, NULL),
(83, 2, 'C', 0, 'C', NULL, NULL, NULL),
(84, 2, 'D', 0, 'D', NULL, NULL, NULL),
(85, 2, 'E', 0, 'E', NULL, NULL, NULL),
(86, 2, 'A', 0, 'A', NULL, NULL, NULL),
(87, 2, 'B', 0, 'B', NULL, NULL, NULL),
(88, 2, 'C', 0, 'C', NULL, NULL, NULL),
(89, 2, 'D', 0, 'D', NULL, NULL, NULL),
(90, 2, 'E', 1, 'E', NULL, NULL, NULL),
(91, 2, 'A', 1, 'A', NULL, NULL, NULL),
(92, 2, 'B', 0, 'B', NULL, NULL, NULL),
(93, 2, 'C', 0, 'C', NULL, NULL, NULL),
(94, 2, 'D', 1, 'D', NULL, NULL, NULL),
(95, 2, 'E', 0, 'E', NULL, NULL, NULL),
(96, 2, 'A', 0, 'A', NULL, NULL, NULL),
(97, 2, 'B', 1, 'B', NULL, NULL, NULL),
(98, 2, 'C', 0, 'C', NULL, NULL, NULL),
(99, 2, 'D', 0, 'D', NULL, NULL, NULL),
(100, 2, 'E', 0, 'E', NULL, NULL, NULL),
(101, 3, 'A', 1, 'A', NULL, NULL, NULL),
(102, 3, 'B', 0, 'B', NULL, NULL, NULL),
(103, 3, 'C', 1, 'C', NULL, NULL, NULL),
(104, 3, 'D', 0, 'D', NULL, NULL, NULL),
(105, 3, 'E', 0, 'E', NULL, NULL, NULL),
(106, 3, 'A', 0, 'A', NULL, NULL, NULL),
(107, 3, 'B', 0, 'B', NULL, NULL, NULL),
(108, 3, 'C', 1, 'C', NULL, NULL, NULL),
(109, 3, 'D', 1, 'D', NULL, NULL, NULL),
(110, 3, 'E', 0, 'E', NULL, NULL, NULL),
(111, 3, 'A', 0, 'A', NULL, NULL, NULL),
(112, 3, 'B', 0, 'B', NULL, NULL, NULL),
(113, 3, 'C', 0, 'C', NULL, NULL, NULL),
(114, 3, 'D', 0, 'D', NULL, NULL, NULL),
(115, 3, 'E', 1, 'E', NULL, NULL, NULL),
(116, 3, 'A', 0, 'A', NULL, NULL, NULL),
(117, 3, 'B', 0, 'B', NULL, NULL, NULL),
(118, 3, 'C', 1, 'C', NULL, NULL, NULL),
(119, 3, 'D', 0, 'D', NULL, NULL, NULL),
(120, 3, 'E', 0, 'E', NULL, NULL, NULL),
(121, 3, 'A', 0, 'A', NULL, NULL, NULL),
(122, 3, 'B', 0, 'B', NULL, NULL, NULL),
(123, 3, 'C', 0, 'C', NULL, NULL, NULL),
(124, 3, 'D', 0, 'D', NULL, NULL, NULL),
(125, 3, 'E', 1, 'E', NULL, NULL, NULL),
(126, 3, 'A', 0, 'A', NULL, NULL, NULL),
(127, 3, 'B', 1, 'B', NULL, NULL, NULL),
(128, 3, 'C', 1, 'C', NULL, NULL, NULL),
(129, 3, 'D', 0, 'D', NULL, NULL, NULL),
(130, 3, 'E', 0, 'E', NULL, NULL, NULL),
(131, 3, 'A', 0, 'A', NULL, NULL, NULL),
(132, 3, 'B', 0, 'B', NULL, NULL, NULL),
(133, 3, 'C', 0, 'C', NULL, NULL, NULL),
(134, 3, 'D', 0, 'D', NULL, NULL, NULL),
(135, 3, 'E', 1, 'E', NULL, NULL, NULL),
(136, 3, 'A', 1, 'A', NULL, NULL, NULL),
(137, 3, 'B', 0, 'B', NULL, NULL, NULL),
(138, 3, 'C', 0, 'C', NULL, NULL, NULL),
(139, 3, 'D', 0, 'D', NULL, NULL, NULL),
(140, 3, 'E', 0, 'E', NULL, NULL, NULL),
(141, 3, 'A', 0, 'A', NULL, NULL, NULL),
(142, 3, 'B', 1, 'B', NULL, NULL, NULL),
(143, 3, 'C', 0, 'C', NULL, NULL, NULL),
(144, 3, 'D', 1, 'D', NULL, NULL, NULL),
(145, 3, 'E', 0, 'E', NULL, NULL, NULL),
(146, 3, 'A', 0, 'A', NULL, NULL, NULL),
(147, 3, 'B', 0, 'B', NULL, NULL, NULL),
(148, 3, 'C', 0, 'C', NULL, NULL, NULL),
(149, 3, 'D', 0, 'D', NULL, NULL, NULL),
(150, 3, 'E', 1, 'E', NULL, NULL, NULL),
(151, 4, 'A', 0, 'A', NULL, NULL, NULL),
(152, 4, 'B', 0, 'B', NULL, NULL, NULL),
(153, 4, 'C', 0, 'C', NULL, NULL, NULL),
(154, 4, 'D', 0, 'D', NULL, NULL, NULL),
(155, 4, 'E', 1, 'E', NULL, NULL, NULL),
(156, 4, 'A', 1, 'A', NULL, NULL, NULL),
(157, 4, 'B', 0, 'B', NULL, NULL, NULL),
(158, 4, 'C', 0, 'C', NULL, NULL, NULL),
(159, 4, 'D', 0, 'D', NULL, NULL, NULL),
(160, 4, 'E', 0, 'E', NULL, NULL, NULL),
(161, 4, 'A', 0, 'A', NULL, NULL, NULL),
(162, 4, 'B', 0, 'B', NULL, NULL, NULL),
(163, 4, 'C', 0, 'C', NULL, NULL, NULL),
(164, 4, 'D', 0, 'D', NULL, NULL, NULL),
(165, 4, 'E', 1, 'E', NULL, NULL, NULL),
(166, 4, 'A', 1, 'A', NULL, NULL, NULL),
(167, 4, 'B', 0, 'B', NULL, NULL, NULL),
(168, 4, 'C', 1, 'C', NULL, NULL, NULL),
(169, 4, 'D', 0, 'D', NULL, NULL, NULL),
(170, 4, 'E', 0, 'E', NULL, NULL, NULL),
(171, 4, 'A', 0, 'A', NULL, NULL, NULL),
(172, 4, 'B', 0, 'B', NULL, NULL, NULL),
(173, 4, 'C', 0, 'C', NULL, NULL, NULL),
(174, 4, 'D', 0, 'D', NULL, NULL, NULL),
(175, 4, 'E', 1, 'E', NULL, NULL, NULL),
(176, 4, 'A', 0, 'A', NULL, NULL, NULL),
(177, 4, 'B', 0, 'B', NULL, NULL, NULL),
(178, 4, 'C', 1, 'C', NULL, NULL, NULL),
(179, 4, 'D', 0, 'D', NULL, NULL, NULL),
(180, 4, 'E', 0, 'E', NULL, NULL, NULL),
(181, 4, 'A', 0, 'A', NULL, NULL, NULL),
(182, 4, 'B', 1, 'B', NULL, NULL, NULL),
(183, 4, 'C', 0, 'C', NULL, NULL, NULL),
(184, 4, 'D', 1, 'D', NULL, NULL, NULL),
(185, 4, 'E', 0, 'E', NULL, NULL, NULL),
(186, 4, 'A', 1, 'A', NULL, NULL, NULL),
(187, 4, 'B', 0, 'B', NULL, NULL, NULL),
(188, 4, 'C', 1, 'C', NULL, NULL, NULL),
(189, 4, 'D', 1, 'D', NULL, NULL, NULL),
(190, 4, 'E', 0, 'E', NULL, NULL, NULL),
(191, 4, 'A', 0, 'A', NULL, NULL, NULL),
(192, 4, 'B', 0, 'B', NULL, NULL, NULL),
(193, 4, 'C', 1, 'C', NULL, NULL, NULL),
(194, 4, 'D', 1, 'D', NULL, NULL, NULL),
(195, 4, 'E', 0, 'E', NULL, NULL, NULL),
(196, 4, 'A', 0, 'A', NULL, NULL, NULL),
(197, 4, 'B', 0, 'B', NULL, NULL, NULL),
(198, 4, 'C', 0, 'C', NULL, NULL, NULL),
(199, 4, 'D', 0, 'D', NULL, NULL, NULL),
(200, 4, 'E', 1, 'E', NULL, NULL, NULL),
(201, 5, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(202, 5, '2', 0, 'A little interested', NULL, NULL, NULL),
(203, 5, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(204, 5, '4', 0, 'Very interested', NULL, NULL, NULL),
(205, 5, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(206, 6, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(207, 6, '2', 0, 'A little interested', NULL, NULL, NULL),
(208, 6, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(209, 6, '4', 0, 'Very interested', NULL, NULL, NULL),
(210, 6, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(211, 7, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(212, 7, '2', 0, 'A little interested', NULL, NULL, NULL),
(213, 7, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(214, 7, '4', 0, 'Very interested', NULL, NULL, NULL),
(215, 7, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(216, 8, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(217, 8, '2', 0, 'A little interested', NULL, NULL, NULL),
(218, 8, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(219, 8, '4', 0, 'Very interested', NULL, NULL, NULL),
(220, 8, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(221, 9, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(222, 9, '2', 0, 'A little interested', NULL, NULL, NULL),
(223, 9, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(224, 9, '4', 0, 'Very interested', NULL, NULL, NULL),
(225, 9, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(226, 10, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(227, 10, '2', 0, 'A little interested', NULL, NULL, NULL),
(228, 10, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(229, 10, '4', 0, 'Very interested', NULL, NULL, NULL),
(230, 10, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(231, 11, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(232, 11, '2', 0, 'A little interested', NULL, NULL, NULL),
(233, 11, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(234, 11, '4', 0, 'Very interested', NULL, NULL, NULL),
(235, 11, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(236, 12, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(237, 12, '2', 0, 'A little interested', NULL, NULL, NULL),
(238, 12, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(239, 12, '4', 0, 'Very interested', NULL, NULL, NULL),
(240, 12, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(241, 13, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(242, 13, '2', 0, 'A little interested', NULL, NULL, NULL),
(243, 13, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(244, 13, '4', 0, 'Very interested', NULL, NULL, NULL),
(245, 13, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(246, 14, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(247, 14, '2', 0, 'A little interested', NULL, NULL, NULL),
(248, 14, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(249, 14, '4', 0, 'Very interested', NULL, NULL, NULL),
(250, 14, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(251, 15, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(252, 15, '2', 0, 'A little interested', NULL, NULL, NULL),
(253, 15, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(254, 15, '4', 0, 'Very interested', NULL, NULL, NULL),
(255, 15, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(256, 16, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(257, 16, '2', 0, 'A little interested', NULL, NULL, NULL),
(258, 16, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(259, 16, '4', 0, 'Very interested', NULL, NULL, NULL),
(260, 16, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(261, 17, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(262, 17, '2', 0, 'A little interested', NULL, NULL, NULL),
(263, 17, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(264, 17, '4', 0, 'Very interested', NULL, NULL, NULL),
(265, 17, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(266, 18, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(267, 18, '2', 0, 'A little interested', NULL, NULL, NULL),
(268, 18, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(269, 18, '4', 0, 'Very interested', NULL, NULL, NULL),
(270, 18, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(271, 19, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(272, 19, '2', 0, 'A little interested', NULL, NULL, NULL),
(273, 19, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(274, 19, '4', 0, 'Very interested', NULL, NULL, NULL),
(275, 19, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(276, 20, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(277, 20, '2', 0, 'A little interested', NULL, NULL, NULL),
(278, 20, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(279, 20, '4', 0, 'Very interested', NULL, NULL, NULL),
(280, 20, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(281, 21, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(282, 21, '2', 0, 'A little interested', NULL, NULL, NULL),
(283, 21, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(284, 21, '4', 0, 'Very interested', NULL, NULL, NULL),
(285, 21, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(286, 22, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(287, 22, '2', 0, 'A little interested', NULL, NULL, NULL),
(288, 22, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(289, 22, '4', 0, 'Very interested', NULL, NULL, NULL),
(290, 22, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(291, 23, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(292, 23, '2', 0, 'A little interested', NULL, NULL, NULL),
(293, 23, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(294, 23, '4', 0, 'Very interested', NULL, NULL, NULL),
(295, 23, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(296, 24, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(297, 24, '2', 0, 'A little interested', NULL, NULL, NULL),
(298, 24, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(299, 24, '4', 0, 'Very interested', NULL, NULL, NULL),
(300, 24, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(301, 25, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(302, 25, '2', 0, 'A little interested', NULL, NULL, NULL),
(303, 25, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(304, 25, '4', 0, 'Very interested', NULL, NULL, NULL),
(305, 25, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(306, 26, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(307, 26, '2', 0, 'A little interested', NULL, NULL, NULL),
(308, 26, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(309, 26, '4', 0, 'Very interested', NULL, NULL, NULL),
(310, 26, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(311, 27, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(312, 27, '2', 0, 'A little interested', NULL, NULL, NULL),
(313, 27, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(314, 27, '4', 0, 'Very interested', NULL, NULL, NULL),
(315, 27, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(316, 28, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(317, 28, '2', 0, 'A little interested', NULL, NULL, NULL),
(318, 28, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(319, 28, '4', 0, 'Very interested', NULL, NULL, NULL),
(320, 28, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(321, 29, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(322, 29, '2', 0, 'A little interested', NULL, NULL, NULL),
(323, 29, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(324, 29, '4', 0, 'Very interested', NULL, NULL, NULL),
(325, 29, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(326, 30, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(327, 30, '2', 0, 'A little interested', NULL, NULL, NULL),
(328, 30, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(329, 30, '4', 0, 'Very interested', NULL, NULL, NULL),
(330, 30, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(331, 31, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(332, 31, '2', 0, 'A little interested', NULL, NULL, NULL),
(333, 31, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(334, 31, '4', 0, 'Very interested', NULL, NULL, NULL),
(335, 31, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(336, 32, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(337, 32, '2', 0, 'A little interested', NULL, NULL, NULL),
(338, 32, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(339, 32, '4', 0, 'Very interested', NULL, NULL, NULL),
(340, 32, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(341, 33, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(342, 33, '2', 0, 'A little interested', NULL, NULL, NULL),
(343, 33, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(344, 33, '4', 0, 'Very interested', NULL, NULL, NULL),
(345, 33, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(346, 34, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(347, 34, '2', 0, 'A little interested', NULL, NULL, NULL),
(348, 34, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(349, 34, '4', 0, 'Very interested', NULL, NULL, NULL),
(350, 34, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(351, 35, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(352, 35, '2', 0, 'A little interested', NULL, NULL, NULL),
(353, 35, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(354, 35, '4', 0, 'Very interested', NULL, NULL, NULL),
(355, 35, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(356, 36, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(357, 36, '2', 0, 'A little interested', NULL, NULL, NULL),
(358, 36, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(359, 36, '4', 0, 'Very interested', NULL, NULL, NULL),
(360, 36, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(361, 37, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(362, 37, '2', 0, 'A little interested', NULL, NULL, NULL),
(363, 37, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(364, 37, '4', 0, 'Very interested', NULL, NULL, NULL),
(365, 37, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(366, 38, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(367, 38, '2', 0, 'A little interested', NULL, NULL, NULL),
(368, 38, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(369, 38, '4', 0, 'Very interested', NULL, NULL, NULL),
(370, 38, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(371, 39, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(372, 39, '2', 0, 'A little interested', NULL, NULL, NULL),
(373, 39, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(374, 39, '4', 0, 'Very interested', NULL, NULL, NULL),
(375, 39, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(376, 40, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(377, 40, '2', 0, 'A little interested', NULL, NULL, NULL),
(378, 40, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(379, 40, '4', 0, 'Very interested', NULL, NULL, NULL),
(380, 40, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(381, 41, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(382, 41, '2', 0, 'A little interested', NULL, NULL, NULL),
(383, 41, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(384, 41, '4', 0, 'Very interested', NULL, NULL, NULL),
(385, 41, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(386, 42, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(387, 42, '2', 0, 'A little interested', NULL, NULL, NULL),
(388, 42, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(389, 42, '4', 0, 'Very interested', NULL, NULL, NULL),
(390, 42, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(391, 43, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(392, 43, '2', 0, 'A little interested', NULL, NULL, NULL),
(393, 43, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(394, 43, '4', 0, 'Very interested', NULL, NULL, NULL),
(395, 43, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(396, 44, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(397, 44, '2', 0, 'A little interested', NULL, NULL, NULL),
(398, 44, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(399, 44, '4', 0, 'Very interested', NULL, NULL, NULL),
(400, 44, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(401, 45, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(402, 45, '2', 0, 'A little interested', NULL, NULL, NULL),
(403, 45, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(404, 45, '4', 0, 'Very interested', NULL, NULL, NULL),
(405, 45, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(406, 46, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(407, 46, '2', 0, 'A little interested', NULL, NULL, NULL),
(408, 46, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(409, 46, '4', 0, 'Very interested', NULL, NULL, NULL),
(410, 46, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(411, 47, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(412, 47, '2', 0, 'A little interested', NULL, NULL, NULL),
(413, 47, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(414, 47, '4', 0, 'Very interested', NULL, NULL, NULL),
(415, 47, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(416, 48, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(417, 48, '2', 0, 'A little interested', NULL, NULL, NULL),
(418, 48, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(419, 48, '4', 0, 'Very interested', NULL, NULL, NULL),
(420, 48, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(421, 49, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(422, 49, '2', 0, 'A little interested', NULL, NULL, NULL),
(423, 49, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(424, 49, '4', 0, 'Very interested', NULL, NULL, NULL),
(425, 49, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(426, 50, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(427, 50, '2', 0, 'A little interested', NULL, NULL, NULL),
(428, 50, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(429, 50, '4', 0, 'Very interested', NULL, NULL, NULL),
(430, 50, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(431, 51, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(432, 51, '2', 0, 'A little interested', NULL, NULL, NULL),
(433, 51, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(434, 51, '4', 0, 'Very interested', NULL, NULL, NULL),
(435, 51, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(436, 52, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(437, 52, '2', 0, 'A little interested', NULL, NULL, NULL),
(438, 52, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(439, 52, '4', 0, 'Very interested', NULL, NULL, NULL),
(440, 52, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(441, 53, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(442, 53, '2', 0, 'A little interested', NULL, NULL, NULL),
(443, 53, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(444, 53, '4', 0, 'Very interested', NULL, NULL, NULL),
(445, 53, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(446, 54, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(447, 54, '2', 0, 'A little interested', NULL, NULL, NULL),
(448, 54, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(449, 54, '4', 0, 'Very interested', NULL, NULL, NULL),
(450, 54, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(451, 55, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(452, 55, '2', 0, 'A little interested', NULL, NULL, NULL),
(453, 55, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(454, 55, '4', 0, 'Very interested', NULL, NULL, NULL),
(455, 55, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(456, 56, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(457, 56, '2', 0, 'A little interested', NULL, NULL, NULL),
(458, 56, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(459, 56, '4', 0, 'Very interested', NULL, NULL, NULL),
(460, 56, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(461, 57, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(462, 57, '2', 0, 'A little interested', NULL, NULL, NULL),
(463, 57, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(464, 57, '4', 0, 'Very interested', NULL, NULL, NULL),
(465, 57, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(466, 58, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(467, 58, '2', 0, 'A little interested', NULL, NULL, NULL),
(468, 58, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(469, 58, '4', 0, 'Very interested', NULL, NULL, NULL),
(470, 58, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(471, 59, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(472, 59, '2', 0, 'A little interested', NULL, NULL, NULL),
(473, 59, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(474, 59, '4', 0, 'Very interested', NULL, NULL, NULL),
(475, 59, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(476, 60, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(477, 60, '2', 0, 'A little interested', NULL, NULL, NULL),
(478, 60, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(479, 60, '4', 0, 'Very interested', NULL, NULL, NULL),
(480, 60, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(481, 61, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(482, 61, '2', 0, 'A little interested', NULL, NULL, NULL),
(483, 61, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(484, 61, '4', 0, 'Very interested', NULL, NULL, NULL),
(485, 61, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(486, 62, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(487, 62, '2', 0, 'A little interested', NULL, NULL, NULL),
(488, 62, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(489, 62, '4', 0, 'Very interested', NULL, NULL, NULL),
(490, 62, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(491, 63, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(492, 63, '2', 0, 'A little interested', NULL, NULL, NULL),
(493, 63, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(494, 63, '4', 0, 'Very interested', NULL, NULL, NULL),
(495, 63, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(496, 64, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(497, 64, '2', 0, 'A little interested', NULL, NULL, NULL),
(498, 64, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(499, 64, '4', 0, 'Very interested', NULL, NULL, NULL),
(500, 64, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(501, 65, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(502, 65, '2', 0, 'A little interested', NULL, NULL, NULL),
(503, 65, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(504, 65, '4', 0, 'Very interested', NULL, NULL, NULL),
(505, 65, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(506, 66, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(507, 66, '2', 0, 'A little interested', NULL, NULL, NULL),
(508, 66, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(509, 66, '4', 0, 'Very interested', NULL, NULL, NULL),
(510, 66, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(511, 67, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(512, 67, '2', 0, 'A little interested', NULL, NULL, NULL),
(513, 67, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(514, 67, '4', 0, 'Very interested', NULL, NULL, NULL),
(515, 67, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(516, 68, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(517, 68, '2', 0, 'A little interested', NULL, NULL, NULL),
(518, 68, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(519, 68, '4', 0, 'Very interested', NULL, NULL, NULL),
(520, 68, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(521, 69, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(522, 69, '2', 0, 'A little interested', NULL, NULL, NULL),
(523, 69, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(524, 69, '4', 0, 'Very interested', NULL, NULL, NULL),
(525, 69, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(526, 70, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(527, 70, '2', 0, 'A little interested', NULL, NULL, NULL),
(528, 70, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(529, 70, '4', 0, 'Very interested', NULL, NULL, NULL),
(530, 70, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(531, 71, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(532, 71, '2', 0, 'A little interested', NULL, NULL, NULL),
(533, 71, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(534, 71, '4', 0, 'Very interested', NULL, NULL, NULL),
(535, 71, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(536, 72, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(537, 72, '2', 0, 'A little interested', NULL, NULL, NULL),
(538, 72, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(539, 72, '4', 0, 'Very interested', NULL, NULL, NULL),
(540, 72, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(541, 73, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(542, 73, '2', 0, 'A little interested', NULL, NULL, NULL),
(543, 73, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(544, 73, '4', 0, 'Very interested', NULL, NULL, NULL),
(545, 73, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(546, 74, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(547, 74, '2', 0, 'A little interested', NULL, NULL, NULL),
(548, 74, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(549, 74, '4', 0, 'Very interested', NULL, NULL, NULL),
(550, 74, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(551, 75, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(552, 75, '2', 0, 'A little interested', NULL, NULL, NULL),
(553, 75, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(554, 75, '4', 0, 'Very interested', NULL, NULL, NULL),
(555, 75, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(556, 76, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(557, 76, '2', 0, 'A little interested', NULL, NULL, NULL),
(558, 76, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(559, 76, '4', 0, 'Very interested', NULL, NULL, NULL),
(560, 76, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(561, 77, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(562, 77, '2', 0, 'A little interested', NULL, NULL, NULL),
(563, 77, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(564, 77, '4', 0, 'Very interested', NULL, NULL, NULL),
(565, 77, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(566, 78, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(567, 78, '2', 0, 'A little interested', NULL, NULL, NULL),
(568, 78, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(569, 78, '4', 0, 'Very interested', NULL, NULL, NULL),
(570, 78, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(571, 79, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(572, 79, '2', 0, 'A little interested', NULL, NULL, NULL),
(573, 79, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(574, 79, '4', 0, 'Very interested', NULL, NULL, NULL),
(575, 79, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(576, 80, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(577, 80, '2', 0, 'A little interested', NULL, NULL, NULL),
(578, 80, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(579, 80, '4', 0, 'Very interested', NULL, NULL, NULL),
(580, 80, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(581, 81, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(582, 81, '2', 0, 'A little interested', NULL, NULL, NULL),
(583, 81, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(584, 81, '4', 0, 'Very interested', NULL, NULL, NULL),
(585, 81, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(586, 82, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(587, 82, '2', 0, 'A little interested', NULL, NULL, NULL),
(588, 82, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(589, 82, '4', 0, 'Very interested', NULL, NULL, NULL),
(590, 82, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(591, 83, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(592, 83, '2', 0, 'A little interested', NULL, NULL, NULL),
(593, 83, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(594, 83, '4', 0, 'Very interested', NULL, NULL, NULL),
(595, 83, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(596, 84, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(597, 84, '2', 0, 'A little interested', NULL, NULL, NULL),
(598, 84, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(599, 84, '4', 0, 'Very interested', NULL, NULL, NULL),
(600, 84, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(601, 85, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(602, 85, '2', 0, 'A little interested', NULL, NULL, NULL),
(603, 85, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(604, 85, '4', 0, 'Very interested', NULL, NULL, NULL),
(605, 85, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(606, 86, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(607, 86, '2', 0, 'A little interested', NULL, NULL, NULL),
(608, 86, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(609, 86, '4', 0, 'Very interested', NULL, NULL, NULL),
(610, 86, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(611, 87, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(612, 87, '2', 0, 'A little interested', NULL, NULL, NULL),
(613, 87, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(614, 87, '4', 0, 'Very interested', NULL, NULL, NULL),
(615, 87, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(616, 88, '1', 0, 'Not at all interested', NULL, NULL, NULL),
(617, 88, '2', 0, 'A little interested', NULL, NULL, NULL),
(618, 88, '3', 0, 'Moderately interested', NULL, NULL, NULL),
(619, 88, '4', 0, 'Very interested', NULL, NULL, NULL),
(620, 88, '5', 0, 'Extremely interested', NULL, NULL, NULL),
(621, 186, 'A', 1, 'A', NULL, NULL, NULL),
(622, 186, 'B', 0, 'B', NULL, NULL, NULL),
(623, 186, 'Neither', 0, 'Neither', NULL, NULL, NULL),
(624, 187, 'A', 1, 'A', NULL, NULL, NULL),
(625, 187, 'B', 0, 'B', NULL, NULL, NULL),
(626, 187, 'Both', 0, 'Both', NULL, NULL, NULL),
(627, 188, 'A', 0, 'A', NULL, NULL, NULL),
(628, 188, 'B', 1, 'B', NULL, NULL, NULL),
(629, 188, 'C', 0, 'C', NULL, NULL, NULL),
(630, 189, 'A', 1, 'A', NULL, NULL, NULL),
(631, 189, 'B', 0, 'B', NULL, NULL, NULL),
(632, 189, 'Either', 0, 'Either', NULL, NULL, NULL),
(633, 190, 'A', 0, 'A', NULL, NULL, NULL),
(634, 190, 'B', 0, 'B', NULL, NULL, NULL),
(635, 190, 'C', 1, 'C', NULL, NULL, NULL),
(636, 191, 'A', 1, 'A', NULL, NULL, NULL),
(637, 191, 'B', 0, 'B', NULL, NULL, NULL),
(638, 191, 'Equal', 0, 'Equal', NULL, NULL, NULL),
(639, 192, 'A', 0, 'A', NULL, NULL, NULL),
(640, 192, 'B', 0, 'B', NULL, NULL, NULL),
(641, 192, 'C', 1, 'C', NULL, NULL, NULL),
(642, 193, 'A', 1, 'A', NULL, NULL, NULL),
(643, 193, 'B', 0, 'B', NULL, NULL, NULL),
(644, 193, 'C', 0, 'C', NULL, NULL, NULL),
(645, 194, 'A', 0, 'A', NULL, NULL, NULL),
(646, 194, 'B', 0, 'B', NULL, NULL, NULL),
(647, 194, 'C', 1, 'C', NULL, NULL, NULL),
(648, 195, 'A', 0, 'A', NULL, NULL, NULL),
(649, 195, 'B', 0, 'B', NULL, NULL, NULL),
(650, 195, 'C', 1, 'C', NULL, NULL, NULL),
(651, 196, 'A', 1, 'A', NULL, NULL, NULL),
(652, 196, 'B', 0, 'B', NULL, NULL, NULL),
(653, 196, 'Equal', 0, 'Equal', NULL, NULL, NULL),
(654, 89, '1', 0, 'Of no importance', NULL, NULL, NULL),
(655, 89, '2', 0, 'Of some importance', NULL, NULL, NULL),
(656, 89, '3', 0, 'Generally important', NULL, NULL, NULL),
(657, 89, '4', 0, 'Very important', NULL, NULL, NULL),
(658, 89, '5', 0, 'Extremely important', NULL, NULL, NULL),
(659, 90, '1', 0, 'Of no importance', NULL, NULL, NULL),
(660, 90, '2', 0, 'Of some importance', NULL, NULL, NULL),
(661, 90, '3', 0, 'Generally important', NULL, NULL, NULL),
(662, 90, '4', 0, 'Very important', NULL, NULL, NULL),
(663, 90, '5', 0, 'Extremely important', NULL, NULL, NULL),
(664, 91, '1', 0, 'Of no importance', NULL, NULL, NULL),
(665, 91, '2', 0, 'Of some importance', NULL, NULL, NULL),
(666, 91, '3', 0, 'Generally important', NULL, NULL, NULL),
(667, 91, '4', 0, 'Very important', NULL, NULL, NULL),
(668, 91, '5', 0, 'Extremely important', NULL, NULL, NULL),
(669, 92, '1', 0, 'Of no importance', NULL, NULL, NULL),
(670, 92, '2', 0, 'Of some importance', NULL, NULL, NULL),
(671, 92, '3', 0, 'Generally important', NULL, NULL, NULL),
(672, 92, '4', 0, 'Very important', NULL, NULL, NULL),
(673, 92, '5', 0, 'Extremely important', NULL, NULL, NULL),
(674, 93, '1', 0, 'Of no importance', NULL, NULL, NULL),
(675, 93, '2', 0, 'Of some importance', NULL, NULL, NULL),
(676, 93, '3', 0, 'Generally important', NULL, NULL, NULL),
(677, 93, '4', 0, 'Very important', NULL, NULL, NULL),
(678, 93, '5', 0, 'Extremely important', NULL, NULL, NULL),
(679, 94, '1', 0, 'Of no importance', NULL, NULL, NULL),
(680, 94, '2', 0, 'Of some importance', NULL, NULL, NULL),
(681, 94, '3', 0, 'Generally important', NULL, NULL, NULL),
(682, 94, '4', 0, 'Very important', NULL, NULL, NULL),
(683, 94, '5', 0, 'Extremely important', NULL, NULL, NULL),
(684, 95, '1', 0, 'Of no importance', NULL, NULL, NULL),
(685, 95, '2', 0, 'Of some importance', NULL, NULL, NULL),
(686, 95, '3', 0, 'Generally important', NULL, NULL, NULL),
(687, 95, '4', 0, 'Very important', NULL, NULL, NULL),
(688, 95, '5', 0, 'Extremely important', NULL, NULL, NULL),
(689, 96, '1', 0, 'Of no importance', NULL, NULL, NULL),
(690, 96, '2', 0, 'Of some importance', NULL, NULL, NULL),
(691, 96, '3', 0, 'Generally important', NULL, NULL, NULL),
(692, 96, '4', 0, 'Very important', NULL, NULL, NULL),
(693, 96, '5', 0, 'Extremely important', NULL, NULL, NULL),
(694, 97, '1', 0, 'Of no importance', NULL, NULL, NULL),
(695, 97, '2', 0, 'Of some importance', NULL, NULL, NULL),
(696, 97, '3', 0, 'Generally important', NULL, NULL, NULL),
(697, 97, '4', 0, 'Very important', NULL, NULL, NULL),
(698, 97, '5', 0, 'Extremely important', NULL, NULL, NULL),
(699, 98, '1', 0, 'Of no importance', NULL, NULL, NULL),
(700, 98, '2', 0, 'Of some importance', NULL, NULL, NULL),
(701, 98, '3', 0, 'Generally important', NULL, NULL, NULL),
(702, 98, '4', 0, 'Very important', NULL, NULL, NULL),
(703, 98, '5', 0, 'Extremely important', NULL, NULL, NULL),
(704, 99, '1', 0, 'Of no importance', NULL, NULL, NULL),
(705, 99, '2', 0, 'Of some importance', NULL, NULL, NULL),
(706, 99, '3', 0, 'Generally important', NULL, NULL, NULL),
(707, 99, '4', 0, 'Very important', NULL, NULL, NULL),
(708, 99, '5', 0, 'Extremely important', NULL, NULL, NULL),
(709, 100, '1', 0, 'Of no importance', NULL, NULL, NULL),
(710, 100, '2', 0, 'Of some importance', NULL, NULL, NULL),
(711, 100, '3', 0, 'Generally important', NULL, NULL, NULL),
(712, 100, '4', 0, 'Very important', NULL, NULL, NULL),
(713, 100, '5', 0, 'Extremely important', NULL, NULL, NULL),
(714, 101, '1', 0, 'Of no importance', NULL, NULL, NULL),
(715, 101, '2', 0, 'Of some importance', NULL, NULL, NULL),
(716, 101, '3', 0, 'Generally important', NULL, NULL, NULL),
(717, 101, '4', 0, 'Very important', NULL, NULL, NULL),
(718, 101, '5', 0, 'Extremely important', NULL, NULL, NULL),
(719, 102, '1', 0, 'Of no importance', NULL, NULL, NULL),
(720, 102, '2', 0, 'Of some importance', NULL, NULL, NULL),
(721, 102, '3', 0, 'Generally important', NULL, NULL, NULL),
(722, 102, '4', 0, 'Very important', NULL, NULL, NULL),
(723, 102, '5', 0, 'Extremely important', NULL, NULL, NULL),
(724, 103, '1', 0, 'Of no importance', NULL, NULL, NULL),
(725, 103, '2', 0, 'Of some importance', NULL, NULL, NULL),
(726, 103, '3', 0, 'Generally important', NULL, NULL, NULL),
(727, 103, '4', 0, 'Very important', NULL, NULL, NULL),
(728, 103, '5', 0, 'Extremely important', NULL, NULL, NULL),
(729, 104, '1', 0, 'Of no importance', NULL, NULL, NULL),
(730, 104, '2', 0, 'Of some importance', NULL, NULL, NULL),
(731, 104, '3', 0, 'Generally important', NULL, NULL, NULL),
(732, 104, '4', 0, 'Very important', NULL, NULL, NULL),
(733, 104, '5', 0, 'Extremely important', NULL, NULL, NULL),
(734, 105, '1', 0, 'Of no importance', NULL, NULL, NULL),
(735, 105, '2', 0, 'Of some importance', NULL, NULL, NULL),
(736, 105, '3', 0, 'Generally important', NULL, NULL, NULL),
(737, 105, '4', 0, 'Very important', NULL, NULL, NULL),
(738, 105, '5', 0, 'Extremely important', NULL, NULL, NULL),
(739, 106, '1', 0, 'Of no importance', NULL, NULL, NULL),
(740, 106, '2', 0, 'Of some importance', NULL, NULL, NULL),
(741, 106, '3', 0, 'Generally important', NULL, NULL, NULL),
(742, 106, '4', 0, 'Very important', NULL, NULL, NULL),
(743, 106, '5', 0, 'Extremely important', NULL, NULL, NULL),
(744, 107, '1', 0, 'Of no importance', NULL, NULL, NULL),
(745, 107, '2', 0, 'Of some importance', NULL, NULL, NULL),
(746, 107, '3', 0, 'Generally important', NULL, NULL, NULL),
(747, 107, '4', 0, 'Very important', NULL, NULL, NULL),
(748, 107, '5', 0, 'Extremely important', NULL, NULL, NULL),
(749, 108, '1', 0, 'Of no importance', NULL, NULL, NULL),
(750, 108, '2', 0, 'Of some importance', NULL, NULL, NULL),
(751, 108, '3', 0, 'Generally important', NULL, NULL, NULL),
(752, 108, '4', 0, 'Very important', NULL, NULL, NULL),
(753, 108, '5', 0, 'Extremely important', NULL, NULL, NULL),
(754, 109, '1', 0, 'Of no importance', NULL, NULL, NULL),
(755, 109, '2', 0, 'Of some importance', NULL, NULL, NULL),
(756, 109, '3', 0, 'Generally important', NULL, NULL, NULL),
(757, 109, '4', 0, 'Very important', NULL, NULL, NULL),
(758, 109, '5', 0, 'Extremely important', NULL, NULL, NULL),
(759, 110, '1', 0, 'Of no importance', NULL, NULL, NULL),
(760, 110, '2', 0, 'Of some importance', NULL, NULL, NULL),
(761, 110, '3', 0, 'Generally important', NULL, NULL, NULL),
(762, 110, '4', 0, 'Very important', NULL, NULL, NULL),
(763, 110, '5', 0, 'Extremely important', NULL, NULL, NULL),
(764, 111, '1', 0, 'Of no importance', NULL, NULL, NULL),
(765, 111, '2', 0, 'Of some importance', NULL, NULL, NULL),
(766, 111, '3', 0, 'Generally important', NULL, NULL, NULL),
(767, 111, '4', 0, 'Very important', NULL, NULL, NULL),
(768, 111, '5', 0, 'Extremely important', NULL, NULL, NULL),
(769, 112, '1', 0, 'Of no importance', NULL, NULL, NULL),
(770, 112, '2', 0, 'Of some importance', NULL, NULL, NULL),
(771, 112, '3', 0, 'Generally important', NULL, NULL, NULL),
(772, 112, '4', 0, 'Very important', NULL, NULL, NULL),
(773, 112, '5', 0, 'Extremely important', NULL, NULL, NULL),
(774, 113, '1', 0, 'Of no importance', NULL, NULL, NULL),
(775, 113, '2', 0, 'Of some importance', NULL, NULL, NULL),
(776, 113, '3', 0, 'Generally important', NULL, NULL, NULL),
(777, 113, '4', 0, 'Very important', NULL, NULL, NULL),
(778, 113, '5', 0, 'Extremely important', NULL, NULL, NULL),
(779, 114, '1', 0, 'Of no importance', NULL, NULL, NULL),
(780, 114, '2', 0, 'Of some importance', NULL, NULL, NULL),
(781, 114, '3', 0, 'Generally important', NULL, NULL, NULL),
(782, 114, '4', 0, 'Very important', NULL, NULL, NULL),
(783, 114, '5', 0, 'Extremely important', NULL, NULL, NULL),
(784, 115, '1', 0, 'Of no importance', NULL, NULL, NULL),
(785, 115, '2', 0, 'Of some importance', NULL, NULL, NULL),
(786, 115, '3', 0, 'Generally important', NULL, NULL, NULL),
(787, 115, '4', 0, 'Very important', NULL, NULL, NULL),
(788, 115, '5', 0, 'Extremely important', NULL, NULL, NULL),
(789, 116, '1', 0, 'Of no importance', NULL, NULL, NULL),
(790, 116, '2', 0, 'Of some importance', NULL, NULL, NULL),
(791, 116, '3', 0, 'Generally important', NULL, NULL, NULL),
(792, 116, '4', 0, 'Very important', NULL, NULL, NULL),
(793, 116, '5', 0, 'Extremely important', NULL, NULL, NULL),
(794, 117, '1', 0, 'Of no importance', NULL, NULL, NULL),
(795, 117, '2', 0, 'Of some importance', NULL, NULL, NULL),
(796, 117, '3', 0, 'Generally important', NULL, NULL, NULL),
(797, 117, '4', 0, 'Very important', NULL, NULL, NULL),
(798, 117, '5', 0, 'Extremely important', NULL, NULL, NULL),
(799, 118, '1', 0, 'Of no importance', NULL, NULL, NULL),
(800, 118, '2', 0, 'Of some importance', NULL, NULL, NULL),
(801, 118, '3', 0, 'Generally important', NULL, NULL, NULL),
(802, 118, '4', 0, 'Very important', NULL, NULL, NULL),
(803, 118, '5', 0, 'Extremely important', NULL, NULL, NULL),
(804, 119, '1', 0, 'Of no importance', NULL, NULL, NULL),
(805, 119, '2', 0, 'Of some importance', NULL, NULL, NULL),
(806, 119, '3', 0, 'Generally important', NULL, NULL, NULL),
(807, 119, '4', 0, 'Very important', NULL, NULL, NULL),
(808, 119, '5', 0, 'Extremely important', NULL, NULL, NULL),
(809, 120, '1', 0, 'Of no importance', NULL, NULL, NULL),
(810, 120, '2', 0, 'Of some importance', NULL, NULL, NULL),
(811, 120, '3', 0, 'Generally important', NULL, NULL, NULL),
(812, 120, '4', 0, 'Very important', NULL, NULL, NULL),
(813, 120, '5', 0, 'Extremely important', NULL, NULL, NULL),
(814, 121, '1', 0, 'Of no importance', NULL, NULL, NULL),
(815, 121, '2', 0, 'Of some importance', NULL, NULL, NULL),
(816, 121, '3', 0, 'Generally important', NULL, NULL, NULL),
(817, 121, '4', 0, 'Very important', NULL, NULL, NULL),
(818, 121, '5', 0, 'Extremely important', NULL, NULL, NULL),
(819, 122, '1', 0, 'Of no importance', NULL, NULL, NULL),
(820, 122, '2', 0, 'Of some importance', NULL, NULL, NULL),
(821, 122, '3', 0, 'Generally important', NULL, NULL, NULL),
(822, 122, '4', 0, 'Very important', NULL, NULL, NULL),
(823, 122, '5', 0, 'Extremely important', NULL, NULL, NULL),
(824, 123, '1', 0, 'Of no importance', NULL, NULL, NULL),
(825, 123, '2', 0, 'Of some importance', NULL, NULL, NULL),
(826, 123, '3', 0, 'Generally important', NULL, NULL, NULL),
(827, 123, '4', 0, 'Very important', NULL, NULL, NULL),
(828, 123, '5', 0, 'Extremely important', NULL, NULL, NULL),
(829, 124, '1', 0, 'Of no importance', NULL, NULL, NULL),
(830, 124, '2', 0, 'Of some importance', NULL, NULL, NULL),
(831, 124, '3', 0, 'Generally important', NULL, NULL, NULL),
(832, 124, '4', 0, 'Very important', NULL, NULL, NULL),
(833, 124, '5', 0, 'Extremely important', NULL, NULL, NULL),
(834, 125, '1', 0, 'Of no importance', NULL, NULL, NULL),
(835, 125, '2', 0, 'Of some importance', NULL, NULL, NULL),
(836, 125, '3', 0, 'Generally important', NULL, NULL, NULL),
(837, 125, '4', 0, 'Very important', NULL, NULL, NULL),
(838, 125, '5', 0, 'Extremely important', NULL, NULL, NULL),
(839, 126, '1', 0, 'Of no importance', NULL, NULL, NULL),
(840, 126, '2', 0, 'Of some importance', NULL, NULL, NULL),
(841, 126, '3', 0, 'Generally important', NULL, NULL, NULL),
(842, 126, '4', 0, 'Very important', NULL, NULL, NULL),
(843, 126, '5', 0, 'Extremely important', NULL, NULL, NULL),
(844, 127, '1', 0, 'Of no importance', NULL, NULL, NULL),
(845, 127, '2', 0, 'Of some importance', NULL, NULL, NULL),
(846, 127, '3', 0, 'Generally important', NULL, NULL, NULL),
(847, 127, '4', 0, 'Very important', NULL, NULL, NULL),
(848, 127, '5', 0, 'Extremely important', NULL, NULL, NULL),
(849, 128, '1', 0, 'Of no importance', NULL, NULL, NULL),
(850, 128, '2', 0, 'Of some importance', NULL, NULL, NULL),
(851, 128, '3', 0, 'Generally important', NULL, NULL, NULL),
(852, 128, '4', 0, 'Very important', NULL, NULL, NULL),
(853, 128, '5', 0, 'Extremely important', NULL, NULL, NULL),
(854, 129, '1', 0, 'Of no importance', NULL, NULL, NULL),
(855, 129, '2', 0, 'Of some importance', NULL, NULL, NULL),
(856, 129, '3', 0, 'Generally important', NULL, NULL, NULL),
(857, 129, '4', 0, 'Very important', NULL, NULL, NULL),
(858, 129, '5', 0, 'Extremely important', NULL, NULL, NULL),
(859, 130, '1', 0, 'Of no importance', NULL, NULL, NULL),
(860, 130, '2', 0, 'Of some importance', NULL, NULL, NULL),
(861, 130, '3', 0, 'Generally important', NULL, NULL, NULL),
(862, 130, '4', 0, 'Very important', NULL, NULL, NULL),
(863, 130, '5', 0, 'Extremely important', NULL, NULL, NULL),
(864, 131, '1', 0, 'Not at all', NULL, NULL, NULL),
(865, 131, '2', 0, 'A little', NULL, NULL, NULL),
(866, 131, '3', 0, 'Moderately', NULL, NULL, NULL),
(867, 131, '4', 0, 'Quite often', NULL, NULL, NULL),
(868, 131, '5', 0, 'Very often', NULL, NULL, NULL),
(869, 132, '1', 0, 'Not at all', NULL, NULL, NULL),
(870, 132, '2', 0, 'A little', NULL, NULL, NULL),
(871, 132, '3', 0, 'Moderately', NULL, NULL, NULL),
(872, 132, '4', 0, 'Quite often', NULL, NULL, NULL),
(873, 132, '5', 0, 'Very often', NULL, NULL, NULL),
(874, 133, '1', 0, 'Not at all', NULL, NULL, NULL),
(875, 133, '2', 0, 'A little', NULL, NULL, NULL),
(876, 133, '3', 0, 'Moderately', NULL, NULL, NULL),
(877, 133, '4', 0, 'Quite often', NULL, NULL, NULL),
(878, 133, '5', 0, 'Very often', NULL, NULL, NULL),
(879, 134, '1', 0, 'Not at all', NULL, NULL, NULL),
(880, 134, '2', 0, 'A little', NULL, NULL, NULL),
(881, 134, '3', 0, 'Moderately', NULL, NULL, NULL),
(882, 134, '4', 0, 'Quite often', NULL, NULL, NULL),
(883, 134, '5', 0, 'Very often', NULL, NULL, NULL),
(884, 135, '1', 0, 'Not at all', NULL, NULL, NULL),
(885, 135, '2', 0, 'A little', NULL, NULL, NULL),
(886, 135, '3', 0, 'Moderately', NULL, NULL, NULL),
(887, 135, '4', 0, 'Quite often', NULL, NULL, NULL),
(888, 135, '5', 0, 'Very often', NULL, NULL, NULL),
(889, 136, '1', 0, 'Not at all', NULL, NULL, NULL),
(890, 136, '2', 0, 'A little', NULL, NULL, NULL),
(891, 136, '3', 0, 'Moderately', NULL, NULL, NULL),
(892, 136, '4', 0, 'Quite often', NULL, NULL, NULL),
(893, 136, '5', 0, 'Very often', NULL, NULL, NULL),
(894, 137, '1', 0, 'Not at all', NULL, NULL, NULL),
(895, 137, '2', 0, 'A little', NULL, NULL, NULL),
(896, 137, '3', 0, 'Moderately', NULL, NULL, NULL),
(897, 137, '4', 0, 'Quite often', NULL, NULL, NULL),
(898, 137, '5', 0, 'Very often', NULL, NULL, NULL),
(899, 138, '1', 0, 'Not at all', NULL, NULL, NULL),
(900, 138, '2', 0, 'A little', NULL, NULL, NULL),
(901, 138, '3', 0, 'Moderately', NULL, NULL, NULL),
(902, 138, '4', 0, 'Quite often', NULL, NULL, NULL),
(903, 138, '5', 0, 'Very often', NULL, NULL, NULL),
(904, 139, '1', 0, 'Not at all', NULL, NULL, NULL),
(905, 139, '2', 0, 'A little', NULL, NULL, NULL),
(906, 139, '3', 0, 'Moderately', NULL, NULL, NULL),
(907, 139, '4', 0, 'Quite often', NULL, NULL, NULL),
(908, 139, '5', 0, 'Very often', NULL, NULL, NULL),
(909, 140, '1', 0, 'Not at all', NULL, NULL, NULL),
(910, 140, '2', 0, 'A little', NULL, NULL, NULL),
(911, 140, '3', 0, 'Moderately', NULL, NULL, NULL),
(912, 140, '4', 0, 'Quite often', NULL, NULL, NULL),
(913, 140, '5', 0, 'Very often', NULL, NULL, NULL),
(914, 141, '1', 0, 'Not at all', NULL, NULL, NULL),
(915, 141, '2', 0, 'A little', NULL, NULL, NULL),
(916, 141, '3', 0, 'Moderately', NULL, NULL, NULL),
(917, 141, '4', 0, 'Quite often', NULL, NULL, NULL),
(918, 141, '5', 0, 'Very often', NULL, NULL, NULL),
(919, 142, '1', 0, 'Not at all', NULL, NULL, NULL),
(920, 142, '2', 0, 'A little', NULL, NULL, NULL),
(921, 142, '3', 0, 'Moderately', NULL, NULL, NULL),
(922, 142, '4', 0, 'Quite often', NULL, NULL, NULL),
(923, 142, '5', 0, 'Very often', NULL, NULL, NULL),
(924, 143, '1', 0, 'Not at all', NULL, NULL, NULL),
(925, 143, '2', 0, 'A little', NULL, NULL, NULL),
(926, 143, '3', 0, 'Moderately', NULL, NULL, NULL),
(927, 143, '4', 0, 'Quite often', NULL, NULL, NULL),
(928, 143, '5', 0, 'Very often', NULL, NULL, NULL),
(929, 144, '1', 0, 'Not at all', NULL, NULL, NULL),
(930, 144, '2', 0, 'A little', NULL, NULL, NULL),
(931, 144, '3', 0, 'Moderately', NULL, NULL, NULL),
(932, 144, '4', 0, 'Quite often', NULL, NULL, NULL),
(933, 144, '5', 0, 'Very often', NULL, NULL, NULL),
(934, 145, '1', 0, 'Not at all', NULL, NULL, NULL),
(935, 145, '2', 0, 'A little', NULL, NULL, NULL),
(936, 145, '3', 0, 'Moderately', NULL, NULL, NULL),
(937, 145, '4', 0, 'Quite often', NULL, NULL, NULL),
(938, 145, '5', 0, 'Very often', NULL, NULL, NULL),
(939, 146, '1', 0, 'Not at all', NULL, NULL, NULL),
(940, 146, '2', 0, 'A little', NULL, NULL, NULL),
(941, 146, '3', 0, 'Moderately', NULL, NULL, NULL),
(942, 146, '4', 0, 'Quite often', NULL, NULL, NULL),
(943, 146, '5', 0, 'Very often', NULL, NULL, NULL),
(944, 147, '1', 0, 'Not at all', NULL, NULL, NULL),
(945, 147, '2', 0, 'A little', NULL, NULL, NULL),
(946, 147, '3', 0, 'Moderately', NULL, NULL, NULL),
(947, 147, '4', 0, 'Quite often', NULL, NULL, NULL),
(948, 147, '5', 0, 'Very often', NULL, NULL, NULL),
(949, 148, '1', 0, 'Not at all', NULL, NULL, NULL),
(950, 148, '2', 0, 'A little', NULL, NULL, NULL),
(951, 148, '3', 0, 'Moderately', NULL, NULL, NULL),
(952, 148, '4', 0, 'Quite often', NULL, NULL, NULL),
(953, 148, '5', 0, 'Very often', NULL, NULL, NULL),
(954, 149, '1', 0, 'Not at all', NULL, NULL, NULL),
(955, 149, '2', 0, 'A little', NULL, NULL, NULL),
(956, 149, '3', 0, 'Moderately', NULL, NULL, NULL),
(957, 149, '4', 0, 'Quite often', NULL, NULL, NULL),
(958, 149, '5', 0, 'Very often', NULL, NULL, NULL),
(959, 150, '1', 0, 'Not at all', NULL, NULL, NULL),
(960, 150, '2', 0, 'A little', NULL, NULL, NULL),
(961, 150, '3', 0, 'Moderately', NULL, NULL, NULL),
(962, 150, '4', 0, 'Quite often', NULL, NULL, NULL),
(963, 150, '5', 0, 'Very often', NULL, NULL, NULL),
(964, 151, '1', 0, 'Not at all', NULL, NULL, NULL),
(965, 151, '2', 0, 'A little', NULL, NULL, NULL),
(966, 151, '3', 0, 'Moderately', NULL, NULL, NULL),
(967, 151, '4', 0, 'Quite often', NULL, NULL, NULL),
(968, 151, '5', 0, 'Very often', NULL, NULL, NULL),
(969, 152, '1', 0, 'Not at all', NULL, NULL, NULL),
(970, 152, '2', 0, 'A little', NULL, NULL, NULL),
(971, 152, '3', 0, 'Moderately', NULL, NULL, NULL),
(972, 152, '4', 0, 'Quite often', NULL, NULL, NULL),
(973, 152, '5', 0, 'Very often', NULL, NULL, NULL),
(974, 153, '1', 0, 'Not at all', NULL, NULL, NULL),
(975, 153, '2', 0, 'A little', NULL, NULL, NULL),
(976, 153, '3', 0, 'Moderately', NULL, NULL, NULL),
(977, 153, '4', 0, 'Quite often', NULL, NULL, NULL),
(978, 153, '5', 0, 'Very often', NULL, NULL, NULL),
(979, 154, '1', 0, 'Not at all', NULL, NULL, NULL),
(980, 154, '2', 0, 'A little', NULL, NULL, NULL),
(981, 154, '3', 0, 'Moderately', NULL, NULL, NULL),
(982, 154, '4', 0, 'Quite often', NULL, NULL, NULL),
(983, 154, '5', 0, 'Very often', NULL, NULL, NULL),
(984, 155, '1', 0, 'Not at all', NULL, NULL, NULL),
(985, 155, '2', 0, 'A little', NULL, NULL, NULL),
(986, 155, '3', 0, 'Moderately', NULL, NULL, NULL),
(987, 155, '4', 0, 'Quite often', NULL, NULL, NULL),
(988, 155, '5', 0, 'Very often', NULL, NULL, NULL),
(989, 156, '1', 0, 'Not at all', NULL, NULL, NULL),
(990, 156, '2', 0, 'A little', NULL, NULL, NULL),
(991, 156, '3', 0, 'Moderately', NULL, NULL, NULL),
(992, 156, '4', 0, 'Quite often', NULL, NULL, NULL),
(993, 156, '5', 0, 'Very often', NULL, NULL, NULL),
(994, 157, '1', 0, 'Not at all', NULL, NULL, NULL),
(995, 157, '2', 0, 'A little', NULL, NULL, NULL),
(996, 157, '3', 0, 'Moderately', NULL, NULL, NULL),
(997, 157, '4', 0, 'Quite often', NULL, NULL, NULL),
(998, 157, '5', 0, 'Very often', NULL, NULL, NULL),
(999, 158, '1', 0, 'Not at all', NULL, NULL, NULL),
(1000, 158, '2', 0, 'A little', NULL, NULL, NULL),
(1001, 158, '3', 0, 'Moderately', NULL, NULL, NULL),
(1002, 158, '4', 0, 'Quite often', NULL, NULL, NULL),
(1003, 158, '5', 0, 'Very often', NULL, NULL, NULL),
(1004, 159, '1', 0, 'Not at all', NULL, NULL, NULL),
(1005, 159, '2', 0, 'A little', NULL, NULL, NULL),
(1006, 159, '3', 0, 'Moderately', NULL, NULL, NULL),
(1007, 159, '4', 0, 'Quite often', NULL, NULL, NULL),
(1008, 159, '5', 0, 'Very often', NULL, NULL, NULL),
(1009, 160, '1', 0, 'Not at all', NULL, NULL, NULL),
(1010, 160, '2', 0, 'A little', NULL, NULL, NULL),
(1011, 160, '3', 0, 'Moderately', NULL, NULL, NULL),
(1012, 160, '4', 0, 'Quite often', NULL, NULL, NULL),
(1013, 160, '5', 0, 'Very often', NULL, NULL, NULL),
(1014, 161, '1', 0, 'Not at all', NULL, NULL, NULL),
(1015, 161, '2', 0, 'A little', NULL, NULL, NULL),
(1016, 161, '3', 0, 'Moderately', NULL, NULL, NULL),
(1017, 161, '4', 0, 'Quite often', NULL, NULL, NULL),
(1018, 161, '5', 0, 'Very often', NULL, NULL, NULL),
(1019, 162, '1', 0, 'Not at all', NULL, NULL, NULL),
(1020, 162, '2', 0, 'A little', NULL, NULL, NULL),
(1021, 162, '3', 0, 'Moderately', NULL, NULL, NULL),
(1022, 162, '4', 0, 'Quite often', NULL, NULL, NULL),
(1023, 162, '5', 0, 'Very often', NULL, NULL, NULL),
(1024, 163, '1', 0, 'Not at all', NULL, NULL, NULL),
(1025, 163, '2', 0, 'A little', NULL, NULL, NULL),
(1026, 163, '3', 0, 'Moderately', NULL, NULL, NULL),
(1027, 163, '4', 0, 'Quite often', NULL, NULL, NULL),
(1028, 163, '5', 0, 'Very often', NULL, NULL, NULL),
(1029, 164, '1', 0, 'Not at all', NULL, NULL, NULL),
(1030, 164, '2', 0, 'A little', NULL, NULL, NULL),
(1031, 164, '3', 0, 'Moderately', NULL, NULL, NULL),
(1032, 164, '4', 0, 'Quite often', NULL, NULL, NULL),
(1033, 164, '5', 0, 'Very often', NULL, NULL, NULL),
(1034, 165, '1', 0, 'Not at all', NULL, NULL, NULL),
(1035, 165, '2', 0, 'A little', NULL, NULL, NULL),
(1036, 165, '3', 0, 'Moderately', NULL, NULL, NULL),
(1037, 165, '4', 0, 'Quite often', NULL, NULL, NULL),
(1038, 165, '5', 0, 'Very often', NULL, NULL, NULL),
(1039, 166, '1', 0, 'Not at all', NULL, NULL, NULL),
(1040, 166, '2', 0, 'A little', NULL, NULL, NULL),
(1041, 166, '3', 0, 'Moderately', NULL, NULL, NULL),
(1042, 166, '4', 0, 'Quite often', NULL, NULL, NULL),
(1043, 166, '5', 0, 'Very often', NULL, NULL, NULL),
(1044, 167, '1', 0, 'Not at all', NULL, NULL, NULL),
(1045, 167, '2', 0, 'A little', NULL, NULL, NULL),
(1046, 167, '3', 0, 'Moderately', NULL, NULL, NULL),
(1047, 167, '4', 0, 'Quite often', NULL, NULL, NULL),
(1048, 167, '5', 0, 'Very often', NULL, NULL, NULL),
(1049, 168, '1', 0, 'Not at all', NULL, NULL, NULL),
(1050, 168, '2', 0, 'A little', NULL, NULL, NULL),
(1051, 168, '3', 0, 'Moderately', NULL, NULL, NULL),
(1052, 168, '4', 0, 'Quite often', NULL, NULL, NULL),
(1053, 168, '5', 0, 'Very often', NULL, NULL, NULL),
(1054, 169, '1', 0, 'Not at all', NULL, NULL, NULL),
(1055, 169, '2', 0, 'A little', NULL, NULL, NULL),
(1056, 169, '3', 0, 'Moderately', NULL, NULL, NULL),
(1057, 169, '4', 0, 'Quite often', NULL, NULL, NULL),
(1058, 169, '5', 0, 'Very often', NULL, NULL, NULL),
(1059, 170, '1', 0, 'Not at all', NULL, NULL, NULL),
(1060, 170, '2', 0, 'A little', NULL, NULL, NULL),
(1061, 170, '3', 0, 'Moderately', NULL, NULL, NULL),
(1062, 170, '4', 0, 'Quite often', NULL, NULL, NULL),
(1063, 170, '5', 0, 'Very often', NULL, NULL, NULL),
(1064, 171, '1', 0, 'Not at all', NULL, NULL, NULL),
(1065, 171, '2', 0, 'A little', NULL, NULL, NULL),
(1066, 171, '3', 0, 'Moderately', NULL, NULL, NULL),
(1067, 171, '4', 0, 'Quite often', NULL, NULL, NULL),
(1068, 171, '5', 0, 'Very often', NULL, NULL, NULL),
(1069, 172, '1', 0, 'Not at all', NULL, NULL, NULL),
(1070, 172, '2', 0, 'A little', NULL, NULL, NULL),
(1071, 172, '3', 0, 'Moderately', NULL, NULL, NULL),
(1072, 172, '4', 0, 'Quite often', NULL, NULL, NULL),
(1073, 172, '5', 0, 'Very often', NULL, NULL, NULL),
(1074, 173, '1', 0, 'Not at all', NULL, NULL, NULL),
(1075, 173, '2', 0, 'A little', NULL, NULL, NULL),
(1076, 173, '3', 0, 'Moderately', NULL, NULL, NULL),
(1077, 173, '4', 0, 'Quite often', NULL, NULL, NULL),
(1078, 173, '5', 0, 'Very often', NULL, NULL, NULL),
(1079, 174, '1', 0, 'Not at all', NULL, NULL, NULL),
(1080, 174, '2', 0, 'A little', NULL, NULL, NULL),
(1081, 174, '3', 0, 'Moderately', NULL, NULL, NULL),
(1082, 174, '4', 0, 'Quite often', NULL, NULL, NULL),
(1083, 174, '5', 0, 'Very often', NULL, NULL, NULL),
(1084, 175, '1', 0, 'Not at all', NULL, NULL, NULL),
(1085, 175, '2', 0, 'A little', NULL, NULL, NULL),
(1086, 175, '3', 0, 'Moderately', NULL, NULL, NULL),
(1087, 175, '4', 0, 'Quite often', NULL, NULL, NULL),
(1088, 175, '5', 0, 'Very often', NULL, NULL, NULL),
(1089, 176, '1', 0, 'Not at all', NULL, NULL, NULL),
(1090, 176, '2', 0, 'A little', NULL, NULL, NULL),
(1091, 176, '3', 0, 'Moderately', NULL, NULL, NULL),
(1092, 176, '4', 0, 'Quite often', NULL, NULL, NULL),
(1093, 176, '5', 0, 'Very often', NULL, NULL, NULL),
(1094, 177, '1', 0, 'Not at all', NULL, NULL, NULL),
(1095, 177, '2', 0, 'A little', NULL, NULL, NULL),
(1096, 177, '3', 0, 'Moderately', NULL, NULL, NULL),
(1097, 177, '4', 0, 'Quite often', NULL, NULL, NULL),
(1098, 177, '5', 0, 'Very often', NULL, NULL, NULL),
(1099, 178, '1', 0, 'Not at all', NULL, NULL, NULL),
(1100, 178, '2', 0, 'A little', NULL, NULL, NULL),
(1101, 178, '3', 0, 'Moderately', NULL, NULL, NULL),
(1102, 178, '4', 0, 'Quite often', NULL, NULL, NULL),
(1103, 178, '5', 0, 'Very often', NULL, NULL, NULL),
(1104, 179, '1', 0, 'Not at all', NULL, NULL, NULL),
(1105, 179, '2', 0, 'A little', NULL, NULL, NULL),
(1106, 179, '3', 0, 'Moderately', NULL, NULL, NULL),
(1107, 179, '4', 0, 'Quite often', NULL, NULL, NULL),
(1108, 179, '5', 0, 'Very often', NULL, NULL, NULL),
(1109, 180, '1', 0, 'Not at all', NULL, NULL, NULL),
(1110, 180, '2', 0, 'A little', NULL, NULL, NULL),
(1111, 180, '3', 0, 'Moderately', NULL, NULL, NULL),
(1112, 180, '4', 0, 'Quite often', NULL, NULL, NULL),
(1113, 180, '5', 0, 'Very often', NULL, NULL, NULL),
(1114, 181, '1', 0, 'Not at all', NULL, NULL, NULL),
(1115, 181, '2', 0, 'A little', NULL, NULL, NULL),
(1116, 181, '3', 0, 'Moderately', NULL, NULL, NULL),
(1117, 181, '4', 0, 'Quite often', NULL, NULL, NULL),
(1118, 181, '5', 0, 'Very often', NULL, NULL, NULL),
(1119, 182, '1', 0, 'Not at all', NULL, NULL, NULL),
(1120, 182, '2', 0, 'A little', NULL, NULL, NULL),
(1121, 182, '3', 0, 'Moderately', NULL, NULL, NULL),
(1122, 182, '4', 0, 'Quite often', NULL, NULL, NULL),
(1123, 182, '5', 0, 'Very often', NULL, NULL, NULL),
(1124, 183, '1', 0, 'Not at all', NULL, NULL, NULL),
(1125, 183, '2', 0, 'A little', NULL, NULL, NULL),
(1126, 183, '3', 0, 'Moderately', NULL, NULL, NULL),
(1127, 183, '4', 0, 'Quite often', NULL, NULL, NULL),
(1128, 183, '5', 0, 'Very often', NULL, NULL, NULL),
(1129, 184, '1', 0, 'Not at all', NULL, NULL, NULL),
(1130, 184, '2', 0, 'A little', NULL, NULL, NULL),
(1131, 184, '3', 0, 'Moderately', NULL, NULL, NULL),
(1132, 184, '4', 0, 'Quite often', NULL, NULL, NULL),
(1133, 184, '5', 0, 'Very often', NULL, NULL, NULL),
(1134, 185, '1', 0, 'Not at all', NULL, NULL, NULL),
(1135, 185, '2', 0, 'A little', NULL, NULL, NULL),
(1136, 185, '3', 0, 'Moderately', NULL, NULL, NULL),
(1137, 185, '4', 0, 'Quite often', NULL, NULL, NULL),
(1138, 185, '5', 0, 'Very often', NULL, NULL, NULL),
(1139, 217, '1', 0, 'I always spend a lot of time making plans', NULL, NULL, NULL),
(1140, 217, '2', 0, 'I find change exciting', NULL, NULL, NULL),
(1141, 217, '3', 0, 'I look for more work when there is little to do', NULL, NULL, NULL),
(1142, 218, '1', 0, 'I am comfortable in situations where I have to make a decision', NULL, NULL, NULL),
(1143, 218, '2', 0, 'I am quick to see when I need to help team members', NULL, NULL, NULL),
(1144, 218, '3', 0, 'I get on well with all types of people', NULL, NULL, NULL),
(1145, 219, '1', 0, 'I am usually one of the first people to find a problem', NULL, NULL, NULL),
(1146, 219, '2', 0, 'I learn new computer programs quickly', NULL, NULL, NULL),
(1147, 219, '3', 0, 'I often come up with new ways to do work', NULL, NULL, NULL),
(1148, 220, '1', 0, 'When working on my tasks I take a lot of care with the details', NULL, NULL, NULL),
(1149, 220, '2', 0, 'Before making decisions I try to get as much information as possible', NULL, NULL, NULL),
(1150, 220, '3', 0, 'I always own up when I have made a mistake', NULL, NULL, NULL),
(1151, 221, '1', 0, 'I always keep up-to-date with new technology', NULL, NULL, NULL),
(1152, 221, '2', 0, 'I can easily see things from other people''s points of view', NULL, NULL, NULL),
(1153, 221, '3', 0, 'I can usually stay positive in difficult situations', NULL, NULL, NULL),
(1154, 222, '1', 0, 'I always finish what I start', NULL, NULL, NULL),
(1155, 222, '2', 0, 'I am quick to think of ways to solve problems', NULL, NULL, NULL),
(1156, 222, '3', 0, 'I am able to persuade people to do things my way', NULL, NULL, NULL),
(1157, 223, '1', 0, 'I get used to new processes easily', NULL, NULL, NULL),
(1158, 223, '2', 0, 'I often find problems that other people have missed', NULL, NULL, NULL),
(1159, 223, '3', 0, 'I help others to find different ways to solve their disagreements', NULL, NULL, NULL),
(1160, 224, '1', 0, 'I always feel for people who are in difficulty', NULL, NULL, NULL),
(1161, 224, '2', 0, 'I am the kind of person who will not stop until I reach my goal', NULL, NULL, NULL),
(1162, 224, '3', 0, 'I know exactly which tasks to do first when I am short of time', NULL, NULL, NULL),
(1163, 225, '1', 0, 'It is easy for me to write things that others will find interesting to read', NULL, NULL, NULL),
(1164, 225, '2', 0, 'I am usually the one who organises team activities', NULL, NULL, NULL),
(1165, 225, '3', 0, 'I find it easy to learn different computer programs', NULL, NULL, NULL),
(1166, 226, '1', 0, 'I can still cope well with my work when under pressure', NULL, NULL, NULL),
(1167, 226, '2', 0, 'I do not avoid situations where I have to be responsible', NULL, NULL, NULL),
(1168, 226, '3', 0, 'I offer to help and support many tasks', NULL, NULL, NULL),
(1169, 227, '1', 0, 'Others say that I am a good listener', NULL, NULL, NULL),
(1170, 227, '2', 0, 'I push myself to be the best in my team', NULL, NULL, NULL),
(1171, 227, '3', 0, 'I do not think about my own, or other people''s feelings and emotions when making decisions', NULL, NULL, NULL),
(1172, 228, '1', 0, 'I often surprise others with new and creative ideas', NULL, NULL, NULL),
(1173, 228, '2', 0, 'I always share and teach others skills that I am good at', NULL, NULL, NULL),
(1174, 228, '3', 0, 'I make plans using lots of detail', NULL, NULL, NULL),
(1175, 229, '1', 0, 'I am not afraid of telling others what to do', NULL, NULL, NULL),
(1176, 229, '2', 0, 'I like to work with new ideas', NULL, NULL, NULL),
(1177, 229, '3', 0, 'Even under pressure I try to do difficult tasks calmly', NULL, NULL, NULL),
(1178, 230, '1', 0, 'I am able to present confidently even on topics where I am less experienced', NULL, NULL, NULL),
(1179, 230, '2', 0, 'I am very good at comforting others', NULL, NULL, NULL),
(1180, 230, '3', 0, 'When looking into a problem, I always start thinking of solutions', NULL, NULL, NULL),
(1181, 231, '1', 0, 'I find it very easy to use skills that I have learnt recently', NULL, NULL, NULL),
(1182, 231, '2', 0, 'I am always looking for something to keep me busy', NULL, NULL, NULL),
(1183, 231, '3', 0, 'I always plan ahead when doing anything', NULL, NULL, NULL),
(1184, 232, '1', 0, 'I quickly spot the weaknesses in the way things are done', NULL, NULL, NULL),
(1185, 232, '2', 0, 'I am usually able to come up with lots of possible solutions to a problem', NULL, NULL, NULL),
(1186, 232, '3', 0, 'I would rather work for longer than to do poor quality work', NULL, NULL, NULL),
(1187, 233, '1', 0, 'I am usually the one to take charge of others', NULL, NULL, NULL),
(1188, 233, '2', 0, 'I always give others an opportunity to get to know me', NULL, NULL, NULL),
(1189, 233, '3', 0, 'I am very logical when making decisions', NULL, NULL, NULL),
(1190, 234, '1', 0, 'I usually find something positive in a situation, however bad it is', NULL, NULL, NULL),
(1191, 234, '2', 0, 'I regularly check my work progress against deadlines', NULL, NULL, NULL),
(1192, 234, '3', 0, 'I go out of my way to help my team', NULL, NULL, NULL),
(1193, 235, '1', 0, 'I learn new information quickly', NULL, NULL, NULL),
(1194, 235, '2', 0, 'I am usually in a positive mood', NULL, NULL, NULL),
(1195, 235, '3', 0, 'When faced with new problems, I can always provide solutions quickly', NULL, NULL, NULL),
(1196, 236, '1', 0, 'I am able to explain information in an easy to understand way', NULL, NULL, NULL),
(1197, 236, '2', 0, 'I guide the activities of others', NULL, NULL, NULL),
(1198, 236, '3', 0, 'I always have a can-do attitude to work', NULL, NULL, NULL),
(1199, 237, '1', 0, 'I often organise what other people are doing', NULL, NULL, NULL),
(1200, 237, '2', 0, 'My suggestions for problem-solving are always practical', NULL, NULL, NULL),
(1201, 237, '3', 0, 'I always work to a plan', NULL, NULL, NULL),
(1202, 238, '1', 0, 'I get on well with most people', NULL, NULL, NULL),
(1203, 238, '2', 0, 'I base my decisions on real evidence', NULL, NULL, NULL),
(1204, 238, '3', 0, 'I quickly remember new information', NULL, NULL, NULL),
(1205, 239, '1', 0, 'I often try to understand how other people would think in a situation', NULL, NULL, NULL),
(1206, 239, '2', 0, 'Persuading people always makes me feel good', NULL, NULL, NULL),
(1207, 239, '3', 0, 'I only feel satisfied when I am busy working', NULL, NULL, NULL),
(1208, 240, '1', 0, 'I usually enjoy changes at work', NULL, NULL, NULL),
(1209, 240, '2', 0, 'I do not relax until the work is done', NULL, NULL, NULL),
(1210, 240, '3', 0, 'I often find new and creative ways of doing things', NULL, NULL, NULL),
(1211, 241, '1', 0, 'I know how to give my ideas and listen to other ideas when coming to agreements', NULL, NULL, NULL),
(1212, 241, '2', 0, 'I quickly identify important information when using the internet', NULL, NULL, NULL),
(1213, 241, '3', 0, 'I do more work when I have a plan to work from', NULL, NULL, NULL),
(1214, 242, '1', 0, 'Before making important decisions I look into things in detail', NULL, NULL, NULL),
(1215, 242, '2', 0, 'I put a lot of energy into my work', NULL, NULL, NULL),
(1216, 242, '3', 0, 'I very quickly know how I should behave with different people', NULL, NULL, NULL),
(1217, 243, '1', 0, 'I am good at tasks that need attention to the details', NULL, NULL, NULL),
(1218, 243, '2', 0, 'I find it easy to understand the thoughts and feelings of others', NULL, NULL, NULL),
(1219, 243, '3', 0, 'I remain calm when work pressure increases', NULL, NULL, NULL),
(1220, 244, '1', 0, 'I feel comfortable taking a leading role in a group', NULL, NULL, NULL),
(1221, 244, '2', 0, 'I am confident when speaking in front of an audience', NULL, NULL, NULL),
(1222, 244, '3', 0, 'I learn new computer skills quickly', NULL, NULL, NULL),
(1223, 245, 'A', 1, 'A', NULL, NULL, 'S01-A.gif'),
(1224, 245, 'B', 0, 'B', NULL, NULL, 'S01-B.gif'),
(1225, 245, 'C', 0, 'C', NULL, NULL, 'S01-C.gif'),
(1226, 245, 'D', 0, 'D', NULL, NULL, 'S01-D.gif'),
(1227, 245, 'E', 0, 'E', NULL, NULL, 'S01-E.gif'),
(1228, 246, 'A', 0, 'A', NULL, NULL, 'S02-A.gif'),
(1229, 246, 'B', 1, 'B', NULL, NULL, 'S02-B.gif'),
(1230, 246, 'C', 0, 'C', NULL, NULL, 'S02-C.gif'),
(1231, 246, 'D', 0, 'D', NULL, NULL, 'S02-D.gif'),
(1232, 246, 'E', 0, 'E', NULL, NULL, 'S02-E.gif'),
(1233, 247, 'A', 1, 'A', NULL, NULL, 'S03-A.gif'),
(1234, 247, 'B', 0, 'B', NULL, NULL, 'S03-B.gif'),
(1235, 247, 'C', 0, 'C', NULL, NULL, 'S03-C.gif'),
(1236, 247, 'D', 0, 'D', NULL, NULL, 'S03-D.gif'),
(1237, 247, 'E', 0, 'E', NULL, NULL, 'S03-E.gif'),
(1238, 248, 'A', 0, 'A', NULL, NULL, 'S04-A.gif'),
(1239, 248, 'B', 0, 'B', NULL, NULL, 'S04-B.gif'),
(1240, 248, 'C', 1, 'C', NULL, NULL, 'S04-C.gif'),
(1241, 248, 'D', 0, 'D', NULL, NULL, 'S04-D.gif'),
(1242, 248, 'E', 0, 'E', NULL, NULL, 'S04-E.gif'),
(1243, 249, 'A', 0, 'A', NULL, NULL, 'S05-A.gif'),
(1244, 249, 'B', 1, 'B', NULL, NULL, 'S05-B.gif'),
(1245, 249, 'C', 0, 'C', NULL, NULL, 'S05-C.gif'),
(1246, 249, 'D', 0, 'D', NULL, NULL, 'S05-D.gif'),
(1247, 249, 'E', 0, 'E', NULL, NULL, 'S05-E.gif'),
(1248, 250, 'A', 1, 'A', NULL, NULL, 'S06-A.gif'),
(1249, 250, 'B', 0, 'B', NULL, NULL, 'S06-B.gif'),
(1250, 250, 'C', 0, 'C', NULL, NULL, 'S06-C.gif'),
(1251, 250, 'D', 0, 'D', NULL, NULL, 'S06-D.gif'),
(1252, 250, 'E', 0, 'E', NULL, NULL, 'S06-E.gif'),
(1253, 251, 'A', 0, 'A', NULL, NULL, 'S07-A.gif'),
(1254, 251, 'B', 0, 'B', NULL, NULL, 'S07-B.gif'),
(1255, 251, 'C', 1, 'C', NULL, NULL, 'S07-C.gif'),
(1256, 251, 'D', 0, 'D', NULL, NULL, 'S07-D.gif'),
(1257, 251, 'E', 0, 'E', NULL, NULL, 'S07-E.gif'),
(1258, 252, 'A', 0, 'A', NULL, NULL, 'S08-A.gif'),
(1259, 252, 'B', 1, 'B', NULL, NULL, 'S08-B.gif'),
(1260, 252, 'C', 0, 'C', NULL, NULL, 'S08-C.gif'),
(1261, 252, 'D', 0, 'D', NULL, NULL, 'S08-D.gif'),
(1262, 252, 'E', 0, 'E', NULL, NULL, 'S08-E.gif'),
(1263, 253, 'A', 0, 'A', NULL, NULL, 'S09-A.gif'),
(1264, 253, 'B', 0, 'B', NULL, NULL, 'S09-B.gif'),
(1265, 253, 'C', 0, 'C', NULL, NULL, 'S09-C.gif'),
(1266, 253, 'D', 1, 'D', NULL, NULL, 'S09-D.gif'),
(1267, 253, 'E', 0, 'E', NULL, NULL, 'S09-E.gif'),
(1268, 254, 'A', 0, 'A', NULL, NULL, 'S10-A.gif'),
(1269, 254, 'B', 0, 'B', NULL, NULL, 'S10-B.gif'),
(1270, 254, 'C', 0, 'C', NULL, NULL, 'S10-C.gif'),
(1271, 254, 'D', 0, 'D', NULL, NULL, 'S10-D.gif'),
(1272, 254, 'E', 1, 'E', NULL, NULL, 'S10-E.gif'),
(1273, 255, 'A', 0, 'A', NULL, NULL, 'S11-A.gif'),
(1274, 255, 'B', 0, 'B', NULL, NULL, 'S11-B.gif'),
(1275, 255, 'C', 1, 'C', NULL, NULL, 'S11-C.gif'),
(1276, 255, 'D', 0, 'D', NULL, NULL, 'S11-D.gif'),
(1277, 255, 'E', 0, 'E', NULL, NULL, 'S11-E.gif'),
(1278, 256, 'A', 0, 'A', NULL, NULL, 'S12-A.gif'),
(1279, 256, 'B', 0, 'B', NULL, NULL, 'S12-B.gif'),
(1280, 256, 'C', 0, 'C', NULL, NULL, 'S12-C.gif'),
(1281, 256, 'D', 1, 'D', NULL, NULL, 'S12-D.gif'),
(1282, 256, 'E', 0, 'E', NULL, NULL, 'S12-E.gif'),
(1283, 257, 'A', 0, 'A', NULL, NULL, 'S13-A.gif'),
(1284, 257, 'B', 0, 'B', NULL, NULL, 'S13-B.gif'),
(1285, 257, 'C', 0, 'C', NULL, NULL, 'S13-C.gif'),
(1286, 257, 'D', 0, 'D', NULL, NULL, 'S13-D.gif'),
(1287, 257, 'E', 1, 'E', NULL, NULL, 'S13-E.gif'),
(1288, 258, 'A', 0, 'A', NULL, NULL, 'S14-A.gif'),
(1289, 258, 'B', 0, 'B', NULL, NULL, 'S14-B.gif'),
(1290, 258, 'C', 0, 'C', NULL, NULL, 'S14-C.gif'),
(1291, 258, 'D', 1, 'D', NULL, NULL, 'S14-D.gif'),
(1292, 258, 'E', 0, 'E', NULL, NULL, 'S14-E.gif'),
(1293, 259, 'A', 1, 'True', NULL, NULL, NULL),
(1294, 259, 'B', 0, 'False', NULL, NULL, NULL),
(1295, 259, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1296, 260, 'A', 1, 'True', NULL, NULL, NULL),
(1297, 260, 'B', 0, 'False', NULL, NULL, NULL),
(1298, 260, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1299, 261, 'A', 0, 'True', NULL, NULL, NULL),
(1300, 261, 'B', 1, 'False', NULL, NULL, NULL),
(1301, 261, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1302, 262, 'A', 1, 'True', NULL, NULL, NULL),
(1303, 262, 'B', 0, 'False', NULL, NULL, NULL),
(1304, 262, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1305, 263, 'A', 0, 'True', NULL, NULL, NULL),
(1306, 263, 'B', 0, 'False', NULL, NULL, NULL),
(1307, 263, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1308, 264, 'A', 1, 'True', NULL, NULL, NULL),
(1309, 264, 'B', 0, 'False', NULL, NULL, NULL),
(1310, 264, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1311, 265, 'A', 0, 'True', NULL, NULL, NULL),
(1312, 265, 'B', 1, 'False', NULL, NULL, NULL),
(1313, 265, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1314, 266, 'A', 0, 'True', NULL, NULL, NULL),
(1315, 266, 'B', 0, 'False', NULL, NULL, NULL),
(1316, 266, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1317, 267, 'A', 0, 'True', NULL, NULL, NULL),
(1318, 267, 'B', 1, 'False', NULL, NULL, NULL),
(1319, 267, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1320, 268, 'A', 0, 'True', NULL, NULL, NULL),
(1321, 268, 'B', 1, 'False', NULL, NULL, NULL),
(1322, 268, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1323, 269, 'A', 0, 'True', NULL, NULL, NULL),
(1324, 269, 'B', 0, 'False', NULL, NULL, NULL),
(1325, 269, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1326, 270, 'A', 0, 'True', NULL, NULL, NULL),
(1327, 270, 'B', 1, 'False', NULL, NULL, NULL),
(1328, 270, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1329, 271, 'A', 0, 'True', NULL, NULL, NULL),
(1330, 271, 'B', 0, 'False', NULL, NULL, NULL),
(1331, 271, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1332, 272, 'A', 0, 'True', NULL, NULL, NULL),
(1333, 272, 'B', 0, 'False', NULL, NULL, NULL),
(1334, 272, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1335, 273, 'A', 1, 'True', NULL, NULL, NULL),
(1336, 273, 'B', 0, 'False', NULL, NULL, NULL),
(1337, 273, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1338, 274, 'A', 1, 'True', NULL, NULL, NULL),
(1339, 274, 'B', 0, 'False', NULL, NULL, NULL),
(1340, 274, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1341, 275, 'A', 1, 'True', NULL, NULL, NULL),
(1342, 275, 'B', 0, 'False', NULL, NULL, NULL),
(1343, 275, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1344, 276, 'A', 1, 'True', NULL, NULL, NULL),
(1345, 276, 'B', 0, 'False', NULL, NULL, NULL),
(1346, 276, 'C', 0, 'Can''t say', NULL, NULL, NULL),
(1347, 277, 'A', 0, 'True', NULL, NULL, NULL),
(1348, 277, 'B', 0, 'False', NULL, NULL, NULL),
(1349, 277, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1350, 278, 'A', 0, 'True', NULL, NULL, NULL),
(1351, 278, 'B', 0, 'False', NULL, NULL, NULL),
(1352, 278, 'C', 1, 'Can''t say', NULL, NULL, NULL),
(1353, 279, 'A', 0, 'A', NULL, NULL, 'AB01-A.gif'),
(1354, 279, 'B', 1, 'B', NULL, NULL, 'AB01-B.gif'),
(1355, 279, 'C', 0, 'C', NULL, NULL, 'AB01-C.gif'),
(1356, 279, 'D', 0, 'D', NULL, NULL, 'AB01-D.gif'),
(1357, 279, 'E', 0, 'E', NULL, NULL, 'AB01-E.gif'),
(1358, 280, 'A', 0, 'A', NULL, NULL, 'AB02-A.gif'),
(1359, 280, 'B', 0, 'B', NULL, NULL, 'AB02-B.gif'),
(1360, 280, 'C', 1, 'C', NULL, NULL, 'AB02-C.gif'),
(1361, 280, 'D', 0, 'D', NULL, NULL, 'AB02-D.gif'),
(1362, 280, 'E', 0, 'E', NULL, NULL, 'AB02-E.gif'),
(1363, 281, 'A', 0, 'A', NULL, NULL, 'AB03-A.gif'),
(1364, 281, 'B', 0, 'B', NULL, NULL, 'AB03-B.gif'),
(1365, 281, 'C', 0, 'C', NULL, NULL, 'AB03-C.gif'),
(1366, 281, 'D', 0, 'D', NULL, NULL, 'AB03-D.gif'),
(1367, 281, 'E', 1, 'E', NULL, NULL, 'AB03-E.gif'),
(1368, 282, 'A', 0, 'A', NULL, NULL, 'AB04-A.gif'),
(1369, 282, 'B', 0, 'B', NULL, NULL, 'AB04-B.gif'),
(1370, 282, 'C', 1, 'C', NULL, NULL, 'AB04-C.gif'),
(1371, 282, 'D', 0, 'D', NULL, NULL, 'AB04-D.gif'),
(1372, 282, 'E', 0, 'E', NULL, NULL, 'AB04-E.gif'),
(1373, 283, 'A', 0, 'A', NULL, NULL, 'AB05-A.gif'),
(1374, 283, 'B', 0, 'B', NULL, NULL, 'AB05-B.gif'),
(1375, 283, 'C', 1, 'C', NULL, NULL, 'AB05-C.gif'),
(1376, 283, 'D', 0, 'D', NULL, NULL, 'AB05-D.gif'),
(1377, 283, 'E', 0, 'E', NULL, NULL, 'AB05-E.gif'),
(1378, 284, 'A', 0, 'A', NULL, NULL, 'AB06-A.gif'),
(1379, 284, 'B', 0, 'B', NULL, NULL, 'AB06-B.gif'),
(1380, 284, 'C', 0, 'C', NULL, NULL, 'AB06-C.gif'),
(1381, 284, 'D', 1, 'D', NULL, NULL, 'AB06-D.gif'),
(1382, 284, 'E', 0, 'E', NULL, NULL, 'AB06-E.gif'),
(1383, 285, 'A', 1, 'A', NULL, NULL, 'AB07-A.gif'),
(1384, 285, 'B', 0, 'B', NULL, NULL, 'AB07-B.gif'),
(1385, 285, 'C', 0, 'C', NULL, NULL, 'AB07-C.gif'),
(1386, 285, 'D', 0, 'D', NULL, NULL, 'AB07-D.gif'),
(1387, 285, 'E', 0, 'E', NULL, NULL, 'AB07-E.gif'),
(1388, 286, 'A', 0, 'A', NULL, NULL, 'AB08-A.gif'),
(1389, 286, 'B', 0, 'B', NULL, NULL, 'AB08-B.gif'),
(1390, 286, 'C', 0, 'C', NULL, NULL, 'AB08-C.gif'),
(1391, 286, 'D', 1, 'D', NULL, NULL, 'AB08-D.gif'),
(1392, 286, 'E', 0, 'E', NULL, NULL, 'AB08-E.gif'),
(1393, 287, 'A', 0, 'A', NULL, NULL, 'AB09-A.gif'),
(1394, 287, 'B', 1, 'B', NULL, NULL, 'AB09-B.gif'),
(1395, 287, 'C', 0, 'C', NULL, NULL, 'AB09-C.gif'),
(1396, 287, 'D', 0, 'D', NULL, NULL, 'AB09-D.gif'),
(1397, 287, 'E', 0, 'E', NULL, NULL, 'AB09-E.gif'),
(1398, 288, 'A', 1, 'A', NULL, NULL, 'AB10-A.gif'),
(1399, 288, 'B', 0, 'B', NULL, NULL, 'AB10-B.gif'),
(1400, 288, 'C', 0, 'C', NULL, NULL, 'AB10-C.gif'),
(1401, 288, 'D', 0, 'D', NULL, NULL, 'AB10-D.gif'),
(1402, 288, 'E', 0, 'E', NULL, NULL, 'AB10-E.gif'),
(1403, 289, 'A', 0, 'A', NULL, NULL, 'AB11-A.gif'),
(1404, 289, 'B', 1, 'B', NULL, NULL, 'AB11-B.gif'),
(1405, 289, 'C', 0, 'C', NULL, NULL, 'AB11-C.gif'),
(1406, 289, 'D', 0, 'D', NULL, NULL, 'AB11-D.gif'),
(1407, 289, 'E', 0, 'E', NULL, NULL, 'AB11-E.gif'),
(1408, 290, 'A', 0, 'A', NULL, NULL, 'AB12-A.gif'),
(1409, 290, 'B', 0, 'B', NULL, NULL, 'AB12-B.gif'),
(1410, 290, 'C', 0, 'C', NULL, NULL, 'AB12-C.gif'),
(1411, 290, 'D', 0, 'D', NULL, NULL, 'AB12-D.gif'),
(1412, 290, 'E', 1, 'E', NULL, NULL, 'AB12-E.gif'),
(1413, 291, 'A', 1, 'A', NULL, NULL, 'AB13-A.gif'),
(1414, 291, 'B', 0, 'B', NULL, NULL, 'AB13-B.gif'),
(1415, 291, 'C', 0, 'C', NULL, NULL, 'AB13-C.gif'),
(1416, 291, 'D', 0, 'D', NULL, NULL, 'AB13-D.gif'),
(1417, 291, 'E', 0, 'E', NULL, NULL, 'AB13-E.gif'),
(1418, 292, 'A', 1, 'A', NULL, NULL, 'AB14-A.gif'),
(1419, 292, 'B', 0, 'B', NULL, NULL, 'AB14-B.gif'),
(1420, 292, 'C', 0, 'C', NULL, NULL, 'AB14-C.gif'),
(1421, 292, 'D', 0, 'D', NULL, NULL, 'AB14-D.gif'),
(1422, 292, 'E', 0, 'E', NULL, NULL, 'AB14-E.gif'),
(1423, 293, 'A', 0, 'A', NULL, NULL, 'AB15-A.gif'),
(1424, 293, 'B', 0, 'B', NULL, NULL, 'AB15-B.gif'),
(1425, 293, 'C', 0, 'C', NULL, NULL, 'AB15-C.gif'),
(1426, 293, 'D', 0, 'D', NULL, NULL, 'AB15-D.gif'),
(1427, 293, 'E', 1, 'E', NULL, NULL, 'AB15-E.gif'),
(1428, 294, 'A', 0, 'A', NULL, NULL, 'AB16-A.gif'),
(1429, 294, 'B', 0, 'B', NULL, NULL, 'AB16-B.gif'),
(1430, 294, 'C', 0, 'C', NULL, NULL, 'AB16-C.gif'),
(1431, 294, 'D', 1, 'D', NULL, NULL, 'AB16-D.gif'),
(1432, 294, 'E', 0, 'E', NULL, NULL, 'AB16-E.gif'),
(1433, 295, 'A', 0, '3,800,000', NULL, NULL, NULL),
(1434, 295, 'B', 0, '4,200,000', NULL, NULL, NULL),
(1435, 295, 'C', 1, '38,000,000', NULL, NULL, NULL),
(1436, 295, 'D', 0, '40,000,000', NULL, NULL, NULL),
(1437, 295, 'E', 0, '42,000,000', NULL, NULL, NULL),
(1438, 296, 'A', 0, 'Manufacturing', NULL, NULL, NULL),
(1439, 296, 'B', 1, 'Electricity, Gas and Water', NULL, NULL, NULL),
(1440, 296, 'C', 0, 'Transport and Communication', NULL, NULL, NULL),
(1441, 296, 'D', 0, 'Domestic', NULL, NULL, NULL),
(1442, 296, 'E', 0, 'Mining and Quarrying', NULL, NULL, NULL),
(1443, 297, 'A', 0, 'January', NULL, NULL, NULL),
(1444, 297, 'B', 0, 'February', NULL, NULL, NULL),
(1445, 297, 'C', 0, 'March', NULL, NULL, NULL),
(1446, 297, 'D', 0, 'April', NULL, NULL, NULL),
(1447, 297, 'E', 1, 'May', NULL, NULL, NULL),
(1448, 298, 'A', 0, '£516', NULL, NULL, NULL),
(1449, 298, 'B', 0, '£580', NULL, NULL, NULL),
(1450, 298, 'C', 1, '£1,096', NULL, NULL, NULL),
(1451, 298, 'D', 0, '£1,256', NULL, NULL, NULL),
(1452, 298, 'E', 0, '£1,894', NULL, NULL, NULL),
(1453, 299, 'A', 0, 'Geneva', NULL, NULL, NULL),
(1454, 299, 'B', 0, 'London', NULL, NULL, NULL),
(1455, 299, 'C', 0, 'Munich', NULL, NULL, NULL),
(1456, 299, 'D', 0, 'Paris', NULL, NULL, NULL),
(1457, 299, 'E', 1, 'Rome', NULL, NULL, NULL),
(1458, 300, 'A', 1, '4,255', NULL, NULL, NULL),
(1459, 300, 'B', 0, '4,350', NULL, NULL, NULL),
(1460, 300, 'C', 0, '5,250', NULL, NULL, NULL),
(1461, 300, 'D', 0, '5,550', NULL, NULL, NULL),
(1462, 300, 'E', 0, '9,030', NULL, NULL, NULL),
(1463, 301, 'A', 1, '11.2%', NULL, NULL, NULL),
(1464, 301, 'B', 0, '11.8%', NULL, NULL, NULL),
(1465, 301, 'C', 0, '12.6%', NULL, NULL, NULL),
(1466, 301, 'D', 0, '13.2%', NULL, NULL, NULL),
(1467, 301, 'E', 0, '13.4%', NULL, NULL, NULL),
(1468, 302, 'A', 0, '6:32', NULL, NULL, NULL),
(1469, 302, 'B', 0, '7:13', NULL, NULL, NULL),
(1470, 302, 'C', 1, '13:7', NULL, NULL, NULL),
(1471, 302, 'D', 0, '13:6', NULL, NULL, NULL),
(1472, 302, 'E', 0, '32:6', NULL, NULL, NULL),
(1473, 303, 'A', 0, 'Factory A', NULL, NULL, NULL),
(1474, 303, 'B', 0, 'Factory B', NULL, NULL, NULL),
(1475, 303, 'C', 0, 'Factory C', NULL, NULL, NULL),
(1476, 303, 'D', 1, 'Factory D', NULL, NULL, NULL),
(1477, 303, 'E', 0, 'Factory E', NULL, NULL, NULL),
(1478, 304, 'A', 0, '4%', NULL, NULL, NULL),
(1479, 304, 'B', 1, '5%', NULL, NULL, NULL),
(1480, 304, 'C', 0, '6%', NULL, NULL, NULL),
(1481, 304, 'D', 0, '7%', NULL, NULL, NULL),
(1482, 304, 'E', 0, '8%', NULL, NULL, NULL)


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


