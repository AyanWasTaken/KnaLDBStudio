interface RandomData {
    id: number;
    name: string;
    isActive: boolean;
    metadata?: {
        createdAt: Date;
        updatedAt?: Date;
    };
}

function generateRandomData(): RandomData {
    const randomId = Math.floor(Math.random() * 1000);
    const randomName = `User_${randomId}`;
    const isActive = Math.random() > 0.5;

    const data: RandomData = {
        id: randomId,
        name: randomName,
        isActive,
        metadata: {
            createdAt: new Date(),
        },
    };

    if (isActive) {
        data.metadata!.updatedAt = new Date();
    }

    return data;
} // hi

function logRandomData(data: RandomData): void {
    console.log(`ID: ${data.id}`);
    console.log(`Name: ${data.name}`);
    console.log(`Active: ${data.isActive ? "Yes" : "No"}`);
    console.log(`Created At: ${data.metadata?.createdAt.toISOString()}`);
    if (data.metadata?.updatedAt) {
        console.log(`Updated At: ${data.metadata.updatedAt.toISOString()}`);
    }
    console.log("-------------------");
}

const randomData = generateRandomData();
logRandomData(randomData);