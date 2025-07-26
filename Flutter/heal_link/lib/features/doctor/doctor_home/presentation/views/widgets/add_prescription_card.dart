import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/trapezium_painter.dart';
import '../../../data/models/add_prescription_model.dart';
import 'add_prescription_body.dart';

class AddPrescriptionCard extends StatelessWidget {
  const AddPrescriptionCard({
    super.key,
    required this.isConfirm,
    required this.patientNameController,
    required this.ageController,
    required this.dateController,
    required this.prescriptionList,
  });

  final bool isConfirm;
  final List<AddPrescriptionModel> prescriptionList;
  final TextEditingController patientNameController;
  final TextEditingController ageController;
  final TextEditingController dateController;

  @override
  Widget build(BuildContext context) {
    return Stack(
      alignment: AlignmentDirectional.topCenter,
      children: [
        Container(
          height:
              !isConfirm
                  ? 557
                  : max(557, 557 + (prescriptionList.length - 5) * 54),
          width: double.infinity,
          decoration: BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.centerRight,
              end: Alignment.centerLeft,
              colors: [const Color(0xFFDBEBFF), Colors.white],
              // stops: [-0.7154, 0.8229],
            ),
            borderRadius: BorderRadius.circular(28),
          ),
        ),
        CustomPaint(size: const Size(194, 32), painter: TrapeziumPainter()),
        SizedBox(
          width: 170,
          child: Text(
            'Dr. Yousry Ahmed',
            textAlign: TextAlign.center,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
            style: AppTextStyles.popins500style16PrimaryColor,
          ),
        ),
        Positioned(
          top: 102,
          left: 16,
          child: SvgPicture.asset(AppImages.addPrescriptions),
        ),
        AddPrescriptionBody(
          isConfirm: isConfirm,
          ageController: ageController,
          dateController: dateController,
          patientNameController: patientNameController,
          prescriptionList: prescriptionList,
        ),
      ],
    );
  }
}
