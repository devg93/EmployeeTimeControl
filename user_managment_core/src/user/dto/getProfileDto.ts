import { ApiProperty } from "@nestjs/swagger";
import { IsEmail, IsNotEmpty, MinLength } from "class-validator";

export class getProfileDto{
 @ApiProperty({ example: 'johndoe@example.com', description: 'User email' })
  @IsEmail()
  email: string;

  @ApiProperty({ example: 'SecurePassword123', description: 'User password' })
  @IsNotEmpty()
  @MinLength(6)
  password: string;

}