import {Card, CardBody, Text} from "@chakra-ui/react"

export default function CardSlot ({rank = "", suit = ""}) {
    return (
        <>
            <Card>
                <CardBody className='w-32 flex justify-center items-center'> 
                    <Text>{rank}</Text>
                    <Text>{suit}</Text>
                </CardBody>
            </Card>
        </>
    )
}