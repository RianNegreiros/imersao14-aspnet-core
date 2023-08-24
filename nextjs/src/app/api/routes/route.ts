import { revalidateTag } from "next/cache";
import { NextResponse } from "next/server";

export async function GET() {
  const response = await fetch(`${process.env.DOTNET_URL}/routes`, {
    next: {
      revalidate: 1,
      tags: ["routes"],
    },
  });
  return NextResponse.json(await response.json());
}

export async function POST(request: Request) {
  const response = await fetch(`${process.env.DOTNET_URL}/routes`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(await request.json()),
  });
  revalidateTag("routes");
  return NextResponse.json(await response.json());
}
