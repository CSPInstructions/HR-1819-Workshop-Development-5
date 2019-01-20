// Since the React part of are application is seperate from the C# part, we need to redefine to models
// These version of the models will be used by react
export type StudyYear = {
    studyYearId: number,
    name: string
}

export type Student = {
    studentId: number,
    firstName: string,
    lastName: string,
    studyYearId: number,
    studyYear: StudyYear,
    grades: null
}

export type Assignment = {
    assignmentId: number,
    name: string,
    studyYearId: number,
    deliveries: null
}

export type Grade = {
    studentId: number,
    assignmentId: number,
    points: number
}