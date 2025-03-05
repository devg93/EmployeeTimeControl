import { Controller, Get, Post, Body, Patch, Param, Delete, Inject } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { ApiOperation } from '@nestjs/swagger';
import {  IuserWriteInterface } from '../user.repository/contracts/user.repository.Interface';





@Controller('registracion')
export class RegistracionController {
  constructor(@Inject("IuserWriteInterface") private readonly UserWriteService: IuserWriteInterface) { }



  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {

    return await this.UserWriteService.register(body);

  }


  @Patch('update/:id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    return await this.UserWriteService.updateUser(id, updateRegistracionDto);
  }


  @Delete('delete/:email')
  async remove(@Param('email') email: string) {
    return await this.UserWriteService.deleteByemail(email);
  }
  
}