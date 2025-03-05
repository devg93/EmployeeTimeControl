

import { ApiTags, ApiOperation } from '@nestjs/swagger';
import { CreateAuthDto } from '../dto/create-auth.dto';
import { JwtAuthGuard } from '../libs/jwt-auth.guard';
import { GetUser } from '../libs/decorators/getUser';
import { IuserReadInterface, IuserWriteInterface } from '../user.repository/contracts/user.repository.Interface';
import { Body, Controller, Get, Inject, Param, Post, UnauthorizedException, UseGuards } from '@nestjs/common';

@ApiTags('Auth')
@Controller('auth')
export class AuthController {
 constructor(@Inject("IuserReadInterface") private readonly UseReadService: IuserReadInterface) { }


  @Post('login')
  @ApiOperation({ summary: 'Login and receive a JWT token' })
  async login(@Body() body: CreateAuthDto) {
    const user = await this.UseReadService.validateUser(body.email, body.password);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    return await this.UseReadService.login(body.email, body.password);
  }



  @Post('getProfileByemail')
  @UseGuards(JwtAuthGuard)
  async getProfileByemail(@Body() body: any, @GetUser() user: any): Promise<any> {
    if (!user) {
      throw new UnauthorizedException('User not found');
    }
    return await this.UseReadService.getProfileByEmail(user.email);

  }

    @Get('findAll')
    async findAll() {
  
      return await this.UseReadService.findAll();
    }
  
  
    @Get('findOne/:id')
    async findOne(@Param('id') id: string) {
      return await this.UseReadService.getProfileById(id);
    }




}





