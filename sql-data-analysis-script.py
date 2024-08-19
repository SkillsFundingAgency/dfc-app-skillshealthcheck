
def count_responses(item, assessment_type):
      
    import json
    data = json.loads(item)
    given_answers = data.get(assessment_type + ".Answers", "")
    if given_answers is not None:
      answer_count = len(given_answers.split(','))
      return answer_count

def parse_assessment(assessment_type):

  print ("processing assessment: " + assessment_type)

  assessments_not_attempted = 0
  assessments_started_incomplete = 0
  assessments_complete = 0
  assessment_answersprovided = []

  for item in data_keys:
    if (assessment_type + ".Complete\": \"\"") in item:
      assessments_not_attempted += 1
    if (assessment_type + ".Complete\": \"True\"") in item:
      assessments_complete += 1
      assessment_answersprovided.append(count_responses(item, assessment_type))
    if (assessment_type + ".Complete\": \"False\"") in item:
      assessments_started_incomplete += 1
      assessment_answersprovided.append(count_responses(item, assessment_type))
  
  denominator = float(assessments_complete + assessments_started_incomplete)
  if (denominator != 0):
    completion_rate = ((assessments_complete/denominator) * 100)

  print (assessment_type + " not attempted:   " + str(assessments_not_attempted))
  print (assessment_type + " incomplete:      " + str(assessments_started_incomplete))
  print (assessment_type + " completed:       " + str(assessments_complete))
  print (assessment_type + " completion rate: " + str(completion_rate) + "%")
  print

  with open("assessments-analysis.csv", 'a') as csv_file_assessments:
    csv_file_assessments.write(str(assessment_type) + ',' + str(assessments_not_attempted) + ',' + str(assessments_started_incomplete) + ',' + str(assessments_complete) + ',' + str(completion_rate) + '\n')

  with open(assessment_type + "-progression-analysis.csv", 'w') as csv_file_progression:
    for value in assessment_answersprovided:
      csv_file_progression.write(str(value) + '\n')

def parse_user_data():

  for item in data_keys:
    not_attempted_assessments = item.count(".Complete\": \"\"")
    assessments_not_attempted_list.append(not_attempted_assessments)
    complete_assessments = item.count(".Complete\": \"True\"")
    assessments_complete_list.append(complete_assessments)
    started_incomplete_assessments = item.count(".Complete\": \"False\"")
    assessments_started_incomplete_list.append(started_incomplete_assessments)

  with open("not-attempted-user-data-analysis.csv", 'w') as csv_file_user_data:
    for value in assessments_not_attempted_list:
      csv_file_user_data.write(str(value) + '\n')

  with open("complete-assessments-user-data-analysis.csv", 'w') as csv_file_user_data:
    for value in assessments_complete_list:
      csv_file_user_data.write(str(value) + '\n')

  with open("started-incomplete-user-data-analysis.csv", 'w') as csv_file_user_data:
    for value in assessments_started_incomplete_list:
      csv_file_user_data.write(str(value) + '\n')

print
print ("Beginning script...")
print

from pandas import *

data = read_csv('dataexport.csv')

data_keys = data['DataValueKeys'].tolist()
data_keys.pop(0)

import csv
with open("assessments-analysis.csv", 'w') as csv_file_assessments:
  csv_file_assessments.write('name, not attempted, incomplete, completed, completion rate\n')

assessments_not_attempted_list = []
assessments_started_incomplete_list = []
assessments_complete_list = []

parse_assessment("SkillAreas")
parse_assessment("Interests")
parse_assessment("Personal")
parse_assessment("Motivation")

parse_assessment("Numerical")
parse_assessment("Verbal")
parse_assessment("Checking")
parse_assessment("Mechanical")
parse_assessment("Spatial")
parse_assessment("Abstract")

parse_user_data()

print ("For full analysis, see:")
print ("1. assessments-analysis.csv")
print ("2. *-progression-analysis.csv (multiple)")
print ("3. *-user-data-analysis.csv (multiple)")
print